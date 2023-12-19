using System;
using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public class SaveSystem : AbstractSystem
    {
        /// <summary>
        ///     保存整数
        /// </summary>
        public void Save(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }

        /// <summary>
        ///     保存浮点数
        /// </summary>
        public void Save(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }

        /// <summary>
        ///     保存字符串
        /// </summary>
        public void Save(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }

        /// <summary>
        ///     保存布尔值
        /// </summary>
        public void Save(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }

        /// <summary>
        ///     保存通用对象
        /// </summary>
        public void Save<T>(string key, T obj)
        {
            var json = JsonUtility.ToJson(obj);
            PlayerPrefs.SetString(key, json);
        }

        /// <summary>
        ///     读取整数
        /// </summary>
        public int LoadInt(string key, int defaultValue = 0)
        {
            return PlayerPrefs.GetInt(key);
        }

        /// <summary>
        ///     读取浮点数
        /// </summary>
        public float LoadFloat(string key, float defaultValue = 0)
        {
            return PlayerPrefs.GetFloat(key);
        }

        /// <summary>
        ///     读取字符串
        /// </summary>
        public string LoadString(string key, string defaultValue = "")
        {
            return PlayerPrefs.GetString(key);
        }

        /// <summary>
        ///     读取布尔值
        /// </summary>
        public bool LoadBool(string key, bool defaultValue = false)
        {
            return PlayerPrefs.GetInt(key) == 1;
        }

        /// <summary>
        ///     读取通用对象
        /// </summary>
        public T Load<T>(string key, T defaultValue = default)
        {
            var json = PlayerPrefs.GetString(key);

            try
            {
                return JsonUtility.FromJson<T>(json);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error loading object from key {key}: {e.Message}");
                return default;
            }
        }

        protected override void OnInit()
        {
        }
    }
}