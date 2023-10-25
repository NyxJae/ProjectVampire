namespace QFramework.Pro
{
    [ActionGroup("Prefab")]
    public class RunPrefab : ActionKitVisualEvent
    {
        public void Run()
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
            }
        }
    }
}