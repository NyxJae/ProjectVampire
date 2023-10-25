using System.IO;
using System.Linq;
using QFramework.Experimental;
using UnityEditor;
using UnityEngine;

namespace QFramework
{
    public class VariableKitEditor
    {
        public class PropertyEditorItem
        {
            public ScriptableProperty Property;
            public bool Foldout = false;
            public Editor Editor;
        }

        private PropertyEditorItem[] mPropertieEditorItems;

        public void Init()
        {
            mPropertieEditorItems = Resources.FindObjectsOfTypeAll<ScriptableProperty>()
                .Select(p => new PropertyEditorItem()
                {
                    Editor = Editor.CreateEditor(p),
                    Foldout = false,
                    Property = p
                }).ToArray();
        }

        public void OnGUI()
        {
            foreach (var item in mPropertieEditorItems)
            {
                GUILayout.BeginVertical("box");
                item.Foldout = EditorGUILayout.Foldout(item.Foldout, item.Property.name);
                if (item.Foldout)
                {
                    item.Editor.OnInspectorGUI();
                }
                GUILayout.EndVertical();
            }

            if (GUILayout.Button("生成代码"))
            {
                var path = "Assets/QFrameworkData/VariableNames.cs";

                var code = new RootCode();
                code.Using("QFramework")
                    .Using("UnityEngine")
                    .Using("System")
                    .EmptyLine()
                    .Namespace("QFramework",
                        ns =>
                        {
                            ns.Class("VariableNames", string.Empty, false, false, c =>
                            {
                                foreach (var item in mPropertieEditorItems)
                                {
                                    c.Custom(
                                        $"public const string {ValidateName(item.Property.name)} = \"{item.Property.name}\";");
                                }
                            });
                        });

                path.DeleteFileIfExists();
                using (var fileWriter = File.CreateText(path))
                {
                    code.Gen(new FileCodeWriter(fileWriter));
                }
            }
        }

        string ValidateName(string propertyName)
        {
            return propertyName.Replace(" ", "_");
        }

        public void OnDestroy()
        {
            mPropertieEditorItems = null;
        }
    }
}