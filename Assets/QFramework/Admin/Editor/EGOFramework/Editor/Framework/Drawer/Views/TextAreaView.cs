using QFramework;
using UnityEditor;
using UnityEngine;

namespace EGO.Framework
{
    public class TextAreaView : View
    {
        public TextAreaView(string content)
        {
            Content = new Property<string>(content);
            Style = new FluentGUIStyle(()=>new GUIStyle(GUI.skin.textArea));
        }
        
        public Property<string> Content { get; set; }

        protected override void OnGUI()
        {
            Content.Value = EditorGUILayout.TextArea(Content.Value,Style.Value,LayoutStyles);
        }
    }
}