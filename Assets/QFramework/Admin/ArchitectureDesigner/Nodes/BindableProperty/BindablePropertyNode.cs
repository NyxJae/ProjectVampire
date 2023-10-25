#if UNITY_EDITOR
using System.Text;
using UnityEngine;

namespace QFramework.Pro
{
    [CreateNodeMenu("BindableProperty")]
    public class BindablePropertyNode : ArchitectureNode
    {

        [SerializeField] public bool Static = true;
        [SerializeField] public TypeDefinitions Type;
        [SerializeField] public string Name;
        [SerializeField] public string InitValue;

        public string ToCode()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("public ");
            if (Static)
            {
                sb.Append("static ");
            }

            if (Type != TypeDefinitions.Custom)
            {
                sb.Append($"BindableProperty<{Type.ToCode()}> ");
            }
            
            sb.Append(Name + " = new");

            if (Type != TypeDefinitions.Custom)
            {
                sb.Append($" BindableProperty<{Type.ToCode()}>({InitValue});");
            }
            
            return sb.ToString();
        }
    }

#if UNITY_EDITOR
    // [CustomEditor(typeof(ModelNode))]
    // public class BindablePropertyNodeInspector : IMGUIGlobalGraphNodeInspector
    // {
    //     public override void OnInspectorGUI()
    //     {
    //         base.OnInspectorGUI();
    //
    //         EditorGUILayout.ObjectField("ModelGraph", target.As<ModelNode>().ModelGraph, typeof(ModelGraph), false);
    //     }
    // }

    [CustomNodeEditor(typeof(BindablePropertyNode))]
    public class BindablePropertyNodeView : GUIGraphNodeEditor
    {
        public override void OnHeaderGUI()
        {
            if (target.As<BindablePropertyNode>().Name.IsNotNullAndEmpty())
            {
                GUILayout.Label(target.As<BindablePropertyNode>().Name,
                    GUIGraphResources.styles.NodeHeader,
                    GUILayout.Height(30));
            }
            else
            {
                GUILayout.Label("BindableProperty",
                    GUIGraphResources.styles.NodeHeader,
                    GUILayout.Height(30));
            }
        }

        // public override void OnBodyGUI()
        // {
        //     base.OnBodyGUI();
        //
        //     if (GUILayout.Button("Open Graph"))
        //     {
        //         IMGUIGraphWindow.OpenWithGraph(target.As<ModelNode>().ModelGraph);
        //     }
        // }

        // public override void AddContextMenuItems(GenericMenu menu)
        // {
        //     base.AddContextMenuItems(menu);
        //
        //     if (Selection.objects.Length == 1 && Selection.activeObject is ModelNode)
        //     {
        //         var node = Selection.activeObject as ModelNode;
        //
        //         menu.AddItem(new GUIContent("Open "), false, () =>
        //         {
        //             IMGUIGraphWindow.OpenWithGraph(node.ModelGraph);
        //             // var monoScript = AssetDatabase.LoadAssetAtPath<MonoScript>(filePath);
        //             // AssetDatabase.OpenAsset(monoScript);
        //         });
        //
        //
        //         var filePath = (node.graph as ArchitectureGraph).ScriptsFolderPath + $"/Model/{node.ClassName}.cs";
        //
        //         if (File.Exists(filePath))
        //         {
        //             menu.AddItem(new GUIContent("Open " + node.ClassName + ".cs"), false, () =>
        //             {
        //                 var monoScript = AssetDatabase.LoadAssetAtPath<MonoScript>(filePath);
        //                 AssetDatabase.OpenAsset(monoScript);
        //             });
        //         }
        //     }
        // }
    }
#endif
}
#endif