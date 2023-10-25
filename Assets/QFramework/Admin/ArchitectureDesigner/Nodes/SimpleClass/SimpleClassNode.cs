
#if UNITY_EDITOR
using UnityEngine;


namespace QFramework.Pro
{
    [CreateNodeMenu("SimpleClass")]
    public class SimpleClassNode : ClassNode
    {
        public bool Static = false;
        public override string ClassName => Name;
    }

    [CustomNodeEditor(typeof(SimpleClassNode))]
    public class SimpleClassView : GUIGraphNodeEditor
    {
        public override void OnHeaderGUI()
        {
            if (target.As<SimpleClassNode>().Name.IsNotNullAndEmpty())
            {
                GUILayout.Label(target.As<SimpleClassNode>().Name,
                    GUIGraphResources.styles.NodeHeader,
                    GUILayout.Height(30));
            }
            else
            {
                GUILayout.Label("SimpleClass",
                    GUIGraphResources.styles.NodeHeader,
                    GUILayout.Height(30));
            }
        }
        
        public override int GetWidth()
        {
            return 480;
        }
    }
}
#endif