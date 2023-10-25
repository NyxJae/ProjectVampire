using System;
using System.Linq;
using EGO.Framework;
using EGO.Logic;
using EGO.System;
using EGO.System.Logic;
using EGO.V1;
using UnityEngine;

namespace EGO.ViewController
{
    public class TodoView : HorizontalLayout
    {
        public V1.Todo Data { get; }

        private ImageButtonView mBtnStart    = null;
        private ImageButtonView mBtnFinish   = null;
        private IView           mFinishedBox = null;


        private BoxView           mBoxView           = null;
        private CategoryComponent mCategoryComponent = null;

        private int mIndent = 0;
        
        private VerticalLayout mChildrenParent = new VerticalLayout();

        public TodoView(V1.Todo data, Action needRefresh, Action onDelete, bool removeWhenFinished = true,
            int indent = 0)
        {
            mIndent = indent;

            Data = data;

            mBtnStart = new ImageButtonView("play", () =>
            {
                data.State.Value = TodoState.Started;
                data.StartTime = DateTime.Now;
                Model.Save();
                Refresh();
            }).Width(25).Height(25).Color(Color.green);

            mBtnFinish = new ImageButtonView("finish", () =>
            {
                Data.State.Value = TodoState.Done;
                Data.FinishedAt = DateTime.Now;
                Model.Save();

                if (removeWhenFinished)
                {
                    RemoveFromParent();
                }
                else
                {
                    RefreshNextFrame();
                }
            }).Width(25).Height(25).Color(Color.green);

            mFinishedBox = new BoxView("已完成")
                .FontColor(Color.white)
                .FontBold()
                .FontSize(12)
                .Width(50)
                .Height(25)
                .Color(Color.green);



            mBoxView = new BoxView("b")
                .FontColor(Color.white)
                .FontBold()
                .FontSize(12)
                .Width(20);


            mCategoryComponent = new CategoryComponent(Data.Category);

            var btnEdit =
                new ImageButtonView("edit", () => { this.PushCommand(() => OpenTodoEditor(needRefresh, Data)); })
                    .Width(25)
                    .Height(25)
                    .Color(Color.black);

            var btnDelete = new ImageButtonView("delete", () =>
            {
                Data.State.UnBindAll();

                onDelete?.Invoke();
                Model.Save();

                RemoveFromParent();
            }).Width(25).Height(25).Color(Color.red);

            new TreeNode(false, Data.Content, indent * 15)
                .FontSize(15)
                .Add2FirstLine(mBtnStart)
                .Add2FirstLine(mBtnFinish)
                .Add2FirstLine(mFinishedBox)
                .Add2FirstLine(mBoxView)
                .Add2FirstLine(mCategoryComponent)
                .Add2FirstLine(
                    new ImageButtonView("process", () => { this.PushCommand(() => { OpenProcessWindow(Data); }); })
                        .Width(25).Height(25).Color(Color.blue))
                .Add2FirstLine(
                    new ImageButtonView("add", () => { this.PushCommand(() => { OpenTodoEditor(() => { }); }); })
                        .Width(25).Height(25).Color(Color.yellow))
                .Add2FirstLine(btnEdit)
                .Add2FirstLine(btnDelete)
                .Add2Spread(mChildrenParent)
                .AddTo(this);
        }

        protected override void OnBeforeDraw()
        {
            Refresh();
        }

        protected override void OnRefresh()
        {
            mChildrenParent.Clear();
            
            Data.Children.ForEach(todo =>
            {
                new TodoView(todo, () => { }, () =>
                    {
                        Data.Children.Remove(todo);
                    }, false, mIndent + 1).AddTo(mChildrenParent);
                new SpaceView(4).AddTo(mChildrenParent);
            });
            
            switch (Data.State.Value)
            {
                case TodoState.NotStart:
                    mBtnStart.Show();
                    mBtnFinish.Hide();
                    mFinishedBox.Hide();
                    break;
                case TodoState.Started:
                    mBtnStart.Hide();
                    mBtnFinish.Show();
                    mFinishedBox.Hide();
                    break;
                case TodoState.Done:
                    mBtnStart.Hide();
                    mBtnFinish.Hide();
                    mFinishedBox.Show();
                    break;
            }

            switch (Data.Priority.Value)
            {
                case TodoPriority.None:
                    mBoxView.Text = "无";
                    mBoxView.Color(Color.clear);
                    break;
                case TodoPriority.A:
                    mBoxView.Text = "A";
                    mBoxView.Color(Color.red);
                    break;
                case TodoPriority.B:
                    mBoxView.Text = "B";
                    mBoxView.Color(Color.yellow);
                    break;
                case TodoPriority.C:
                    mBoxView.Text = "C";
                    mBoxView.Color(Color.cyan);
                    break;
                case TodoPriority.D:
                    mBoxView.Text = "D";
                    mBoxView.Color(Color.blue);
                    break;
            }

//            mContentView.Content = Data.Content;

        }

        /// <summary>
        /// todo 为空 则为创建新的
        /// todo 不为空 则为编辑当前的
        /// </summary>
        /// <param name="needRefresh"></param>
        /// <param name="todo"></param>
        void OpenTodoEditor(Action needRefresh, V1.Todo todo = null)
        {
            var todoEditor = Window.CreateSubWindow("Todo 编辑器");

            var verticalLayout = new VerticalLayout("box")
                .AddTo(todoEditor);

            var content = todo != null ? todo.Content : string.Empty;

            var priority = todo != null ? todo.Priority.Value : TodoPriority.None;

            new LabelView("描述").FontSize(15).FontBold().AddTo(verticalLayout);
            new TextAreaView(content).Height(30)
                .ExpandHeight()
                .FontSize(15)
                .AddTo(verticalLayout)
                .Content.Bind(newContent => content = newContent);

            var categoryIndex = 0;

            var hide = new Property<bool>(todo != null ? todo.Hide : false);

            if (todo != null && todo.Category != null)
            {
                categoryIndex = Model.IndexOfCategory(todo.Category);
            }

            new PopupView(categoryIndex, Model.Categories
                    .Select(category => category.Name)
                    .ToArray())
                .AddTo(verticalLayout)
                .FontBold()
                .IndexProperty.Bind(index => categoryIndex = index);

            
            var priorityView = new EnumPopupView<TodoPriority>(Data.Priority.Value)
                .FontBold()
                .AddTo(verticalLayout);
                
            priorityView.ValueProperty.Bind(newPriority => { priority = newPriority; });

            var showButton = new ButtonView("显示", () => { hide.Value = false; })
                .FontSize(EGOTheme.TEXT_BUTTON_SIZE)
                .FontBold()
                .AddTo(verticalLayout);
            var hideButton = new ButtonView("隐藏", () => { hide.Value = true; })
                .FontSize(EGOTheme.TEXT_BUTTON_SIZE)
                .FontBold()
                .AddTo(verticalLayout);



            hide.Bind(value =>
            {
                Data.Hide = value;
                Model.Save();
                todoEditor.Close();
                RefreshNextFrame();
                needRefresh.Invoke();
            });

            showButton.Visible = hide.Value;
            hideButton.Visible = !hide.Value;

            new ButtonView("保存", () =>
                {
                    if (todo != null)
                    {
                        todo.Content = content;
                        todo.Hide = hide.Value;
                        todo.Priority.Value = priority;
                        Model.Save();

                        try
                        {
                            todo.Category = Model.GetCategoryByIndex(categoryIndex);
                        }
                        catch (Exception e)
                        {
                            // ignored
                        }

                        mCategoryComponent.Data = Data.Category;
                    }
                    else
                    {
                        var newTodo = new V1.Todo()
                        {
                            Content = content,
                            Hide = hide.Value,
                            Priority = {Value = priority}
                        };


                        Data.Children.Add(newTodo);

                        Model.Save();
                    }

                    todoEditor.Close();

                    RefreshNextFrame();

                })
                .FontSize(EGOTheme.TEXT_BUTTON_SIZE)
                .FontBold()
                .AddTo(verticalLayout);
        }

        void OpenProcessWindow(V1.Todo todo)
        {
            var processWindow = Window.CreateSubWindow("处理");

            var hide = false;

            var note = new Note() {Content = todo.Content};

            ProcessSystem.CreateQuestions(note)
                // 拆分逻辑
                .TodoSplitQuestion(todo.Children)
                
                .AddTo(processWindow)
                .StartProcess(() =>
                {
                    processWindow.Close();
                    RefreshNextFrame();
                });

            processWindow.Show();
        }

        void Convert2Codo(Note note,bool hide)
        {
            Model.RemoveNote(note);
            Model.CreateTodo(note.Content, hide);
            Model.Save();
        }
    }
}