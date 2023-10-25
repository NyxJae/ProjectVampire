using EGO.Constant;
using EGO.Framework;
using EGO.Logic;
using EGO.Util;
using UnityEngine;

namespace EGO.ViewController
{
    public class TodoListController : Framework.ViewController
    {

        private NoteListView         NoteListView     { get; set; } = new NoteListView() {Visible = true};
        public TodoListMenu TodoListView     { get; set; } = new TodoListMenu() {Visible = false};



        private ProductListView ProductListView { get; set; } = new ProductListView() {Visible = false};

        public override void SetUpView()
        {
            View.VerticalStyle = "box";

            new ToolbarView()
                .AddMenu("笔记", _ =>
                {
                    EventDispatcher.Send(GlobalEvents.OnTopBarMenuClicked,NoteListView.Key);
                })
                .AddMenu("清单", _ =>
                {
                    EventDispatcher.Send(GlobalEvents.OnTopBarMenuClicked,TodoListMenu.Key);
                })
                .AddMenu("产品", _ =>
                {
                    EventDispatcher.Send(GlobalEvents.OnTopBarMenuClicked,ProductListView.KEY);
                })
                .AddTo(View)
                .Height(30)
                .FontSize(20)
                .FontBold();

            View.AddChild(TodoListView);

            View.AddChild(ProductListView);

            NoteListView.AddTo(View);
        }
    }
}