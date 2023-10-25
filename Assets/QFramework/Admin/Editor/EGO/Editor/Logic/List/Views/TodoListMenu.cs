using EGO.Constant;
using EGO.Framework;
using EGO.Util;

namespace EGO.ViewController
{
    public class TodoListMenu : VerticalLayout
    {
        public const string Key = nameof(TodoListView);


        TodoListView             mTodoListView         = new TodoListView();
        FinishedTodoListView     mFinishedTodoListView = new FinishedTodoListView() {Visible = false};
        public  HideListView     HideListView { get; set; } = new HideListView() {Visible = false};
        private CategoryListView CategoryView { get; set; } = new CategoryListView() {Visible = false};

        public TodoListMenu()
        {
            new ToolbarView()
                .AddMenu("待办事项", _ => { EventDispatcher.Send(GlobalEvents.OnTodoListMenuClicked, TodoListView.Key); })
                .AddMenu("隐藏清单", _ => { EventDispatcher.Send(GlobalEvents.OnTodoListMenuClicked, HideListView.Key); })
                .AddMenu("分类管理", _ => { EventDispatcher.Send(GlobalEvents.OnTodoListMenuClicked, CategoryListView.Key); })
                .AddMenu("已完成",
                    _ => { EventDispatcher.Send(GlobalEvents.OnTodoListMenuClicked, FinishedTodoListView.Key); })
                .AddTo(this)
                .Height(30)
                .FontSize(20)
                .FontBold();


            this.RegisterEvent(GlobalEvents.OnTopBarMenuClicked, key =>
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

            mTodoListView.AddTo(this);
            mFinishedTodoListView.AddTo(this);
            CategoryView.AddTo(this);
            HideListView.AddTo(this);
        }
    }
}