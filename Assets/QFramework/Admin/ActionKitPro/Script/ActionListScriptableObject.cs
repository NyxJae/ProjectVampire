using UnityEngine;

namespace QFramework.Pro
{
    [CreateAssetMenu(menuName = "@ActionKit/ActionList", fileName = "New ActionList", order = int.MinValue + 2)]
    public class ActionListScriptableObject : ScriptableObject,ISerializationCallbackReceiver
    {
        

        public void OnBeforeSerialize()
        {
            
        }

        public void OnAfterDeserialize()
        {
            
        }
    }
}