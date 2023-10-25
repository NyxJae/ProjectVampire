using System;
using System.Collections.Generic;
using EGO.Framework;
using UnityEngine;

namespace EGO.System
{
    public class QuestionQueue : VerticalLayout
    {
        public ProcessSystem System { get; set; }

        private Queue<IQuestion> mQueue = new Queue<IQuestion>();

        private Action mOnFinished;

        public Choice GetChoice(string choiceName)
        {
            return System.GetChoice(choiceName);
        }
        public Choice AddChoice()
        {
            return new Choice().AddTo(this);
        }

        public void Add(IQuestion questionView)
        {
            questionView.Hide();
            questionView.AddTo(this);
            mQueue.Enqueue(questionView);
            questionView.OnProcess(Next);
            questionView.OnChoice(choiceName =>
            {
                var choice = GetChoice(choiceName);
                choice.StartProcess(Next);
            });
        }

        public void Process()
        {
            Next();
        }

        /// <summary>
        /// 处理问题
        /// </summary>
        private void Next()
        {
            Debug.Log("called ");
            if (mQueue.Count == 0)
            {
                Debug.Log("finished ");
                mOnFinished?.Invoke();
                return;
            }

            mQueue.Dequeue().Show();
        }

        public void OnFinish(Action onFinish)
        {
            mOnFinished = onFinish;
        }
    }
}