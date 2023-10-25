#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace QFramework.Pro
{
    public abstract class ClassNode : ArchitectureNode
    {
        [SerializeField] public string Name;

        public abstract string ClassName { get; }


        public List<PropertyDefinitions> Properties;
    }

    [CreateNodeMenu("Model")]
    public class ModelNode : ClassNode
    {
        [HideInInspector] public ModelGraph ModelGraph;
        public bool WithInterface = true;

        private void OnValidate()
        {
            if (Name.IsNotNullAndEmpty())
            {
                name = Name + "Model";
            }

            if (ModelGraph == null)
            {
                ModelGraph = CreateInstance<ModelGraph>();
                ModelGraph.Model = this;
#if UNITY_EDITOR
                AssetDatabase.AddObjectToAsset(ModelGraph, graph);
#endif
            }

            ModelGraph.name = ClassName + "Graph";
        }

        private void OnDestroy()
        {
            DestroyImmediate(ModelGraph, true);
        }

        public override string ClassName => Name != null && Name.EndsWith("Model") ? Name : Name + "Model";
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(ModelNode))]
    public class ModelNodeInspector : GlobalGUIGraphNodeInspector
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.ObjectField("ModelGraph", target.As<ModelNode>().ModelGraph, typeof(ModelGraph), false);
        }
    }

    [CustomNodeEditor(typeof(ModelNode))]
    public class ModelNodeView : GUIGraphNodeEditor
    {
        
        public override void OnHeaderGUI()
        {
            if (target.As<ModelNode>().Name.IsNotNullAndEmpty())
            {
                GUILayout.Label(target.As<ModelNode>().Name + " Model",
                    GUIGraphResources.styles.NodeHeader,
                    GUILayout.Height(30));
            }
            else
            {
                GUILayout.Label(target.As<ModelNode>().Name,
                    GUIGraphResources.styles.NodeHeader,
                    GUILayout.Height(30));
            }
        }

        public override void OnBodyGUI()
        {
            base.OnBodyGUI();

            if (GUILayout.Button("Open Graph"))
            {
                GUIGraphWindow.OpenWithGraph(target.As<ModelNode>().ModelGraph);
            }
        }

        public override void AddContextMenuItems(GenericMenu menu)
        {
            base.AddContextMenuItems(menu);

            if (Selection.objects.Length == 1 && Selection.activeObject is ModelNode)
            {
                var node = Selection.activeObject as ModelNode;

                menu.AddItem(new GUIContent("Open "), false, () =>
                {
                    GUIGraphWindow.OpenWithGraph(node.ModelGraph);
                    // var monoScript = AssetDatabase.LoadAssetAtPath<MonoScript>(filePath);
                    // AssetDatabase.OpenAsset(monoScript);
                });


                var filePath = (node.graph as ArchitectureGraph).ScriptsFolderPath + $"/Model/{node.ClassName}.cs";

                if (File.Exists(filePath))
                {
                    menu.AddItem(new GUIContent("Open " + node.ClassName + ".cs"), false, () =>
                    {
                        var monoScript = AssetDatabase.LoadAssetAtPath<MonoScript>(filePath);
                        AssetDatabase.OpenAsset(monoScript);
                    });
                }
            }
        }
    }
#endif
}
#endif