using System;
using EGO.Framework;
using EGO.Logic;

namespace EGO.System
{
    public class QuestionView<T> : VerticalLayout, IQuestion<T> where T : IQuectionContainer
    {
        public QuestionView()
        {
            mContentArea.AddTo(this)
                .ExpandHeight();
        }

        ILayout mContentArea = new VerticalLayout();
        
        public QuestionView<T> Title(string title)
        {
            new SpaceView().AddTo(mContentArea);
            new LabelView(title).FontBold().FontSize(30).TextMiddleCenter()
                .AddTo(mContentArea);
            return this;
        }
        
        public QuestionView<T> Content(string content)
        {
            new SpaceView().AddTo(mContentArea);
            new LabelView(content).FontBold().FontSize(30).TextMiddleCenter()
                .AddTo(mContentArea);
            return this;
        }
      
        public QuestionView<T> TextArea(string content,Action<string> onInput)
        {
            new TextAreaView(content).ExpandHeight().FontSize(15).AddTo(mContentArea)
                .Content.Bind(onInput);
            return this;
        }

        public QuestionView<T> Menu(string name, Action action)
        {
            new ButtonView(name, () =>
            {
                action();
                Hide();
                mOnProcessed?.Invoke();
            })
                .FontSize(EGOTheme.TEXT_BUTTON_SIZE)
                .FontBold()
                .AddTo(this);
            return this;
        }
        
        public QuestionView<T> RepeatSelfMenu(string name, Action action)
        {
            new ButtonView(name, () =>
            {
                action();
                
            })
                .FontBold()
                .FontSize(EGOTheme.TEXT_BUTTON_SIZE)
                .AddTo(this);
            return this;
        }


        public QuestionView<T> Choice(string name, string dstMenuName, Action onChoice = null)
        {
            new ButtonView(name, () =>
                {
                    onChoice?.Invoke();
                    Hide();
                    mOnChoice.Invoke(dstMenuName);
                })
                .FontSize(EGOTheme.TEXT_BUTTON_SIZE)
                .FontBold()
                .AddTo(this);
            return this;
        }

        private Action mOnProcessed = null;
        private Action<string> mOnChoice = null;
        
        public T Container { private get; set; }
        
        public T End()
        {
            return Container;
        }

        void IQuestion.OnProcess(Action onProcess)
        {
            mOnProcessed = onProcess;
        }

        void IQuestion.OnChoice(Action<string> onChoice)
        {
            mOnChoice = onChoice;
        }
    }
}