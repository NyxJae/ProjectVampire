/****************************************************************************
 * Copyright (c) 2016 - 2022 liangxiegame UNDER MIT License
 * 
 * https://qframework.cn
 * https://github.com/liangxiegame/QFramework
 * https://gitee.com/liangxiegame/QFramework
 ****************************************************************************/

using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace QFramework.Experimental
{
    [CreateAssetMenu(fileName = "Scriptable Vector2", menuName = "EventKit/Scriptable Vector2", order = 1)]

    public class ScriptableVector2 : ScriptableProperty<Vector2>, ISerializationCallbackReceiver
    {
        //Float value
        [NonSerialized]
        public Vector2 Value;

        //Can the value be reset in game
        //public bool resettable;

        //When the game starts, the starting value we use (so we can reset if need be)
        [SerializeField]
        private Vector2 startingValue = Vector2.zero;

        /// <summary>
        /// Set sVector3 value
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(Vector2 value)
        {
            Value = value;
        }

        /// <summary>
        /// Set value to another sVector3 value
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(ScriptableVector2 value)
        {
            Value = value.Value;
        }

        /// <summary>
        /// Add a Vector3 value to the value
        /// </summary>
        /// <param name="value"></param>
        public void AddValue(Vector2 value)
        {
            Value += value;
        }

        /// <summary>
        /// Add another sVector3 value to the value
        /// </summary>
        /// <param name="value"></param>
        public void AddValue(ScriptableVector2 value)
        {
            Value += value.Value;
        }

        /// <summary>
        /// Recieve callback after unity deseriallzes the object
        /// </summary>
        public void OnAfterDeserialize()
        {
            Value = startingValue;
        }

        public void OnBeforeSerialize() { }

        /// <summary>
        /// Reset the value to it's inital value if it's resettable
        /// </summary>
        public override void ResetValue()
        {
            Value = startingValue;
        }
    }
    
    #if UNITY_EDITOR
    [CustomEditor(typeof(ScriptableVector2))]
    [CanEditMultipleObjects]
    public class SOVector2Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            //Draw the defualt inspector options
            DrawDefaultInspector();

            ScriptableVector2 script = (ScriptableVector2)target;

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            EditorGUILayout.LabelField("Debugging Options", EditorStyles.centeredGreyMiniLabel);

            EditorGUILayout.LabelField("Current value: " + script.Value, EditorStyles.boldLabel);

            //Display button that resets the value to the starting value
            if (GUILayout.Button("Reset Value"))
            {
                if (EditorApplication.isPlaying)
                {
                    script.ResetValue();
                }
            }

            EditorGUILayout.EndVertical();
        }
    }
    #endif
}
