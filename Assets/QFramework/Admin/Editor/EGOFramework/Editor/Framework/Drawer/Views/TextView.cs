using QFramework;
using UnityEditor;
using UnityEngine;

namespace EGO.Framework
{
    public class TextView : View
    {
        public TextView(string content)
        {
            Content = new Property<string>(content);
            Style = new FluentGUIStyle(()=>new GUIStyle(GUI.skin.textField));
        }
        
        public Property<string> Content { get; set; }

        protected override void OnGUI()
        {
            Content.Value = EditorGUILayout.TextField(Content.Value,Style.Value,LayoutStyles);
        }
    }
}