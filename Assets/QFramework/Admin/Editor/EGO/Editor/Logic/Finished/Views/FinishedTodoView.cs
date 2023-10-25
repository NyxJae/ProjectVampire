using EGO.Framework;
using EGO.V1;
using UnityEngine;

namespace EGO.ViewController
{
    public class FinishedTodoView : HorizontalLayout
    {
        public V1.Todo Data { get; }

        private ImageButtonView mBtnReset = null;

        public FinishedTodoView(V1.Todo data)
        {
            Data = data;
            
            mBtnReset = new ImageButtonView("reset", () =>
            {
                Data.State.Value = TodoState.NotStart;
                Model.Save();
                Refresh();
            }).Width(30).Height(20).Color(Color.gray);

            this.AddChild(mBtnReset);

            this.AddChild(new LabelView(Data.Content).Height(20).FontSize(15).TextMiddleLeft());
            this.AddChild(new LabelView(Data.FinishedAt.ToString("完成于 HH:mm:ss")).Height(20).Width(90).TextLowerRight());            

            this.AddChild(new LabelView(Data.UsedTimeText()).Height(20).Width(80).TextLowerRight());            

            this.AddChild(new ImageButtonView("delete", () =>
            {
                Data.State.UnBindAll();

                Model.RemoveTodo(Data);
                Model.Save();

                RemoveFromParent();
            }).Width(25).Height(25).Color(Color.red));
            

        }

        protected override void OnBeforeDraw()
        {
            Refresh();
        }

        protected override void OnRefresh()
        {
            if (Data.State.Value != TodoState.Done)
            {
                RemoveFromParent();
            }
        }
    }
}