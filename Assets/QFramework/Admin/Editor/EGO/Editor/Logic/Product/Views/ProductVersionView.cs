using System;
using System.Linq;
using EGO.Framework;
using EGO.V1;
using EGO.ViewController;
using UnityEngine;

namespace EGO.Logic
{
    public class ProductVersionView : VerticalLayout
    {
        private ProductVersion mProductVersion = null;
        ILayout                mTodosParent    = new VerticalLayout("box");

        public ProductVersionView(ProductVersion productVersion,Action onEdit)
        {
            mProductVersion = productVersion;

            var inputView = new TodoListInputView();
            
            mTodosParent = new VerticalLayout("box");

            inputView.OnTodoCreate = todo =>
            {
                productVersion.AddTodo(todo);
                Model.Save();

                RefreshNextFrame();
            };

            var finishedBox = new BoxView("已完成")
                .FontColor(Color.white)
                .FontBold()
                .FontSize(12)
                .Width(50)
                .Height(25)
                .Color(Color.green);
            
            var startedBox = new BoxView("正在进行")
                .FontColor(Color.white)
                .FontBold()
                .FontSize(12)
                .Width(75)
                .Height(25)
                .Color(Color.blue);

            new TreeNode(productVersion.State == TodoState.Started, " " + productVersion.Version + " " + productVersion.Name)
                .Add2FirstLine(finishedBox)
                .Add2FirstLine(startedBox)
                .Add2FirstLine(new ImageButtonView("edit", onEdit)
                    .Width(25)
                    .Height(25)
                    .Color(Color.black))
                .Add2Spread(inputView)
                .Add2Spread(mTodosParent)
                .FontSize(12)
                .FontBold()
                .AddTo(this);



            if (productVersion.State == TodoState.Done)
            {
                finishedBox.Show();
                startedBox.Hide();
                inputView.Hide();
            }
            else if (productVersion.State == TodoState.Started)
            {
                finishedBox.Hide();
                startedBox.Show();
            }
            else
            {
                finishedBox.Hide();
                startedBox.Hide();
            }

            Refresh();
        }

        protected override void OnRefresh()
        {
            mTodosParent.Clear();

            foreach (var todo in mProductVersion.Todos.OrderBy(todo=>todo.State.Value))
            {
                new TodoView(todo, () => { }, () => { mProductVersion.RemoveTodo(todo); },false)
                    .AddTo(mTodosParent);

                new SpaceView(4).AddTo(mTodosParent);   
            }
        }
    }
}    