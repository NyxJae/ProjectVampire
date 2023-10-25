using System.Text;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace QFramework.Pro
{
    [System.Serializable]
    public class PropertyDefinitions
    {
#if UNITY_EDITOR
        public AccessTypeDefinitions AccessType;
#endif
        public StaticDefinitions StaticDefinitions;

        public TypeDefinitions Type;

        public string Name;

        public string InitValue;

        public string ToCode()
        {
            var builder = new StringBuilder();
#if UNITY_EDITOR
            if (AccessType == AccessTypeDefinitions.Public)
            {
                builder.Append("public ");
            }
            else if (AccessType == AccessTypeDefinitions.Private)
            {
                builder.Append("private ");
            }
#endif

            builder.Append(StaticDefinitions.ToCode());

            builder.Append(Type.ToCode() + " ");
            builder.Append(Name);

            if (InitValue.IsNotNullAndEmpty())
            {
                builder.Append(" = " + InitValue + ";");
            }
            else
            {
                builder.Append(";");
            }

            return builder.ToString();
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(PropertyDefinitions))]
    public class PropertyDefinitionsDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            // Draw label
            // position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            var accessTypeRect = new Rect(position.x, position.y, 60, position.height);
            var isStaticRect = new Rect(position.x + 65, position.y, 50, position.height);
            var typeRect = new Rect(position.x + 120, position.y, 50, position.height);
            var nameRect = new Rect(position.x + 175, position.y, 160, position.height);
            var equalsRect = new Rect(position.x + 340, position.y, 20, position.height);
            var initValueRect = new Rect(position.x + 360, position.y, 50, position.height);

            EditorGUI.PropertyField(accessTypeRect, property.FindPropertyRelative("AccessType"), GUIContent.none);
            EditorGUI.PropertyField(isStaticRect, property.FindPropertyRelative("StaticDefinitions"), GUIContent.none);
            EditorGUI.PropertyField(typeRect, property.FindPropertyRelative("Type"), GUIContent.none);
            EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("Name"), GUIContent.none);
            EditorGUI.LabelField(equalsRect, "=");
            EditorGUI.PropertyField(initValueRect, property.FindPropertyRelative("InitValue"), GUIContent.none);

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }


        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label);
        }
    }
#endif
}