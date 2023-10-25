using System;
using System.Collections.Generic;
using EGO.Framework;
using EGO.V1;

namespace EGO.System
{
    public class ProcessSystem : IQuestionContainer<ProcessSystem>
    {
        public Note Note { get; }


        public ProcessSystem(Note note)
        {
            Note = note;
            mMainQueue.System = this;
        }
        
        public static ProcessSystem CreateQuestions(Note note)
        {
            return new ProcessSystem(note);
        }

        private QuestionQueue mMainQueue = new QuestionQueue();
        
        private Dictionary<string, Choice> mChoices = new Dictionary<string, Choice>();


        public QuestionView<ProcessSystem> BeginQuestion()
        {
            var questionView = new QuestionView<ProcessSystem> {Container = this};
            mMainQueue.Add(questionView);
            return questionView;
        }

        public Choice BeginChoice(string choiceName)
        {
            var choice = mMainQueue.AddChoice();
            choice.System = this;
            mChoices.Add(choiceName,choice);
            return choice;
        }

        public ProcessSystem AddTo(ILayout layout)
        {
            mMainQueue.AddTo(layout);
            return this;
        }

        public void StartProcess(Action onFinish)
        {
            mMainQueue.Process();
            mMainQueue.OnFinish(onFinish);
        }

        public Choice GetChoice(string choiceName)
        {
            return mChoices[choiceName];
        }
    }
}