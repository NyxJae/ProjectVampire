using System.Collections.Generic;
using UnityEngine;

namespace QFramework
{
    public abstract class ActionKitVisualEvent : MonoBehaviour
    {
        [HideInInspector]
        public List<ActionKitVisualAction> Acitons;

        protected ISequence mSequenceNode;
        public void Execute()
        {
            if (Acitons != null && Acitons.Count != 0)
            {
                if (mSequenceNode == null)
                {
                    mSequenceNode = ActionKit.Sequence();

                    foreach (var actionKitVisualAction in Acitons)
                    {
                        mSequenceNode.Append(actionKitVisualAction);
                    }
                }

                mSequenceNode.Start(this);
            }
        }
    }
}