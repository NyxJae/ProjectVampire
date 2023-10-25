using EGO.Constant;
using EGO.Framework;
using EGO.Logic;
using EGO.System;
using EGO.System.Logic;
using EGO.V1;

namespace EGO.ViewController
{
    public class NoteListView : VerticalLayout
    {
        public const string Key = nameof(NoteListView);

        public NoteListView()
        {
            VerticalStyle = "box";

            new ButtonView("创建笔记", () =>
                {
                    this.PushCommand(() =>
                    {
                        new NoteEditorView()
                            .AddTo(Parent);
                    });

                    RemoveFromParent();
                })
                .FontBold()
                .FontSize(EGOTheme.TEXT_BUTTON_SIZE)
                .AddTo(this);

            mNotesParent.AddTo(this);

            RegisterEvent(GlobalEvents.OnTopBarMenuClicked, key =>
            {
                if (key == Key)
                {
                    Show();
                }
                else
                {
                    Hide();
                }
            });

            Refresh();
        }

        protected override void OnRefresh()
        {
            mNotesParent.Clear();

            foreach (var note in Model.Notes)
            {
                new NoteView(note, OpenProcessWindow, () =>
                {
                    this.PushCommand(() =>
                    {
                        new NoteEditorView(note)
                            .AddTo(Parent);
                    });
                    
                    RemoveFromParent();
                    
                }).AddTo(mNotesParent);
            }
        }

        ILayout mNotesParent = new ScrollLayout();


        void OpenProcessWindow(Note note)
        {
            var processWindow = Window.CreateSubWindow("处理");

            var hide = false;

            ProcessSystem.CreateQuestions(note)

                .BeginQuestion()
                    .Title(note.Content)
                    .Content("  是什么?")
                    .Menu("目标", () => { })
                    .Menu("参考/阅读资料", () => { })
                    .Menu("想法/Idea", () => { })
                    .Choice("事项/事件", "事项")
                .End()

                .BeginChoice("事项")
                    .BeginQuestion()
                        .Title("是否可以拆解为多步?")
                        .Choice("是","拆解多步")
                        .Choice("否","现在是否可以执行")
                    .End()
                .End()
                
                .BeginChoice("现在是否可以执行")
                .BeginQuestion()
                    .Title("现在是否可以执行")
                   .Menu("是", () =>
                    {
                       Convert2Codo(note,false);

                    })
                    .Menu("否", () =>
                    {
                        // 要提供一些条件,比如选择。
                        // 事项的条件。
                    })
                .End()
                .End()
                
                // 拆分逻辑
                .TodoSplitChoice()
                
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