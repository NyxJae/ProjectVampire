using System;
using System.Linq;
using EGO.Framework;
using EGO.Util;
using UnityEngine;

namespace EGO.ViewController
{
    public class TodoListInputView : VerticalLayout
    {
        public Action<V1.Todo> OnTodoCreate;

        private string mInputContent = string.Empty;

        public TodoListInputView()
        {
            VerticalStyle = "box";

            var horizontalLayout = new HorizontalLayout();

            var popupView = new PopupView(0, ModelLoader<V1.TodoList>.Model.Categories
                    .Select(category => category.Name).ToArray())
                .AddTo(horizontalLayout)
                .Width(100);
            
            var inputTextArea = new TextAreaView(mInputContent)
                .Height(25)
                .FontSize(15)
                .AddTo(horizontalLayout);

            inputTextArea
                .Content
                .Bind(newContent => mInputContent = newContent);

            var button = new ImageButtonView("add", () =>
            {
                if (!string.IsNullOrEmpty(mInputContent))
                {
                    var newTodo = new V1.Todo
                    {
                        Content = mInputContent, Category = Model.GetCategoryByIndex(popupView.IndexProperty.Value)
                    };


                    OnTodoCreate(newTodo);

                    this.PushCommand(() =>
                    {
                        mInputContent = string.Empty;
                        inputTextArea.Content.Value = string.Empty;
                    });
                }
            }).Width(25).Height(25).Color(Color.yellow);;
            
            horizontalLayout.AddChild(button);

            AddChild(horizontalLayout);
        }
    }
}