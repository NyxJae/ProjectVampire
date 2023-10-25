using System;
using System.Linq;
using EGO.Constant;
using EGO.Framework;
using EGO.Util;
using EGO.V1;
using UnityEngine;

namespace EGO.ViewController
{
    public class FinishedTodoListView : VerticalLayout
    {
        public const string Key = nameof(FinishedTodoListView);

        public FinishedTodoListView()
        {
            AddChild(new SpaceView());

            var scrollLayout = new ScrollLayout();
            scrollLayout.AddChild(mTodosParentContainer);
            scrollLayout.AddTo(this); 
            
            this.RegisterEvent(GlobalEvents.OnTodoListMenuClicked, key =>
            {
                Debug.Log(key);
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
        
        ILayout mTodosParentContainer = new VerticalLayout("box");
        
        public void CreateTodoView(V1.Todo model)
        {
            var todoView = new FinishedTodoView(model);
            mTodosParentContainer.AddChild(todoView);
            mTodosParentContainer.AddChild(new SpaceView(4));
        }

        protected override void OnRefresh()
        {
            mTodosParentContainer.Clear();
            
            var groupsByDay = Model.Todos
                .Where(todo=>todo.State.Value == TodoState.Done)
                .GroupBy(todo => todo.FinishedAt.Date)
                .OrderByDescending(group=>group.Key);
            
            foreach (var grouping in groupsByDay)
            {
                
                var totalTime = TimeSpan.Zero;

                foreach (var todo in grouping)
                {
                    totalTime += todo.UsedTime();
                }

                new SpaceView(2).AddTo(mTodosParentContainer);

                mTodosParentContainer
                    .AddChild(new LabelView(grouping.Key.ToString("yyyy年MM月dd日 (共" + TodoExtension.UsedTime2Text(totalTime)) +")")
                        .FontSize(20)
                        .FontBold()
                        .TextMiddleLeft());

                new SpaceView(2).AddTo(mTodosParentContainer);
                
                foreach (var todo in grouping.OrderByDescending(todo=>todo.FinishedAt))
                {
                    CreateTodoView(todo);

                }
            }
        }

        protected override void OnShow()
        {
            RefreshNextFrame();
        }
    }
}