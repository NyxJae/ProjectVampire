using System;

namespace QFramework
{
    public class TransitionInOut<TIn,TOut> : IAction 
        where TIn : FadeTransition 
        where TOut : FadeTransition
    {
        private Action mOnInFinish;
        private TIn mIn;
        private TOut mOut;
        
        public TransitionInOut<TIn,TOut> In(Action<TIn> inConfig)
        {
            inConfig(mIn);
            return this;
        }

        public TransitionInOut<TIn,TOut> Out(Action<TOut> outConfig)
        {
            outConfig(mOut);
            return this;
        }
        
        public TransitionInOut<TIn,TOut> SetIn(TIn transitionIn)
        {
            mIn = transitionIn;
            return this;
        }
        
        public TransitionInOut<TIn,TOut> SetOut(TOut transitionOut)
        {
            mOut = transitionOut;
            return this;
        }
        
        public TransitionInOut<TIn,TOut> OnInFinish(Action onInFinish)
        {
            mOnInFinish = onInFinish;
            return this;
        }
        

        public ulong ActionID { get; set; }
        public ActionStatus Status { get; set; }
        public void OnStart()
        {
            ActionKit.Sequence()
                .Append(mIn)
                .Callback(mOnInFinish)
                .Append(mOut)
                .StartGlobal(this.Finish);
        }

        public void OnExecute(float dt)
        {
        }

        public void OnFinish()
        {
        }

        public bool Deinited { get; set; }
        public bool Paused { get; set; }
        public void Reset()
        {
        }

        public void Deinit()
        {
        }
    }
}