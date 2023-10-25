using System.Linq;
using EGO.Constant;
using EGO.Framework;
using EGO.V1;

namespace EGO.ViewController
{
    public class TodoListView : VerticalLayout
    {
        public TodoListInputView InputView { get; set; } = new TodoListInputView();

        public const string Key = nameof(TodoListView);

        public TodoListView()
        {   
            AddChild(new SpaceView());
            InputView.OnTodoCreate = (newTodo) =>
            {
                newTodo.State.Bind(_ => Model.Save());
                Model.AddTodo(newTodo);
                Model.Save();
                CreateTodoView(newTodo);
                Model.LogJson();
            };

            InputView.AddTo(this);

            new SpaceView().AddTo(this);

            mTodosParentContainer.AddTo(new ScrollLayout().AddTo(this));
            
            RegisterEvent(GlobalEvents.OnTodoListMenuClicked, key =>
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
        
        ILayout mTodosParentContainer  = new VerticalLayout("box");

        protected override void OnShow()
        {
            RefreshNextFrame();
        }

        protected override void OnRefresh()
        {            
            mTodosParentContainer.Clear();
                            
            foreach (var todo in Model.Todos
                .Where(todo=>todo.State.Value != TodoState.Done && !todo.Hide)
                .OrderByDescending(todo=>todo.State.Value)
                .ThenBy(todo=>todo.Priority.Value))
            {
                CreateTodoView(todo);
            }
        }

        public void CreateTodoView(V1.Todo model)
        {
            var todoView = new TodoView(model,RefreshNextFrame, () =>
            {
                Model.RemoveTodo(model);
            });
            mTodosParentContainer.AddChild(todoView);
            mTodosParentContainer.AddChild(new SpaceView(4));
            
            model.Priority.Bind(_ =>
            {
                RefreshNextFrame();
            });
        }      
    }
}