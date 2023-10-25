using UnityEngine;

namespace QFramework
{
    public abstract class ActionKitVisualAction : MonoBehaviour, IAction
    {
        #region IAction Support
        #endregion

        #region ResetableSupport

        public void Reset()
        {
            Status = ActionStatus.NotStart;
            OnReset();
        }

        public bool Paused { get; set; }
        public bool Deinited { get; set; }

        public void Deinit()
        {
            
        }

        #endregion

        

        protected virtual void OnReset()
        {
        }

        public bool Finished { get; }

        public ulong ActionID { get; set; }
        public ActionStatus Status { get; set; }

        void IAction<ActionStatus>.OnStart()
        {
            OnStart();
        }
        
        protected virtual void OnStart()
        {
        }

        
        void IAction<ActionStatus>.OnExecute(float dt)
        {
            OnExecute(dt);
        }
        /// <summary>
        /// finished
        /// </summary>
        protected virtual void OnExecute(float dt)
        {
        }

        void IAction<ActionStatus>.OnFinish()
        {
            OnFinish();
        }
        
        protected virtual void OnFinish()
        {
        }


        public void ExecuteByCall()
        {
            this.Start(this);
        }
    }
}