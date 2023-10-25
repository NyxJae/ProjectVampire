

using System;
using EGO.Framework;

namespace EGO.System
{
    public class Choice : VerticalLayout,IQuestionContainer<Choice> 
    {
        QuestionQueue mQueue = new QuestionQueue();

        
        public ProcessSystem System { private get; set; }

        public QuestionView<Choice> BeginQuestion()
        {
            var questionView = new QuestionView<Choice>().AddTo(this);
            questionView.Hide();
            mQueue.Add(questionView);
            questionView.Container = this;
            return questionView;
        }

        public void StartProcess(Action onFinish)
        {
            mQueue.Process();
            mQueue.OnFinish(onFinish);
        }
        

        public ProcessSystem End()
        {
            mQueue.System = System;
            return System;
        }

        public Choice GetChoice(string choiceName)
        {
            return System.GetChoice(choiceName);
        }
    }
}