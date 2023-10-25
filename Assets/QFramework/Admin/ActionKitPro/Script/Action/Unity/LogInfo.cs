using UnityEngine;

namespace QFramework
{
    [ActionGroup("调试/Debug")]
    public class LogInfo : ActionKitVisualAction
    {
        public string Message;

        protected override void OnStart()
        {
            Debug.Log(Message);
            
            this.Finish();
        }
    }
}