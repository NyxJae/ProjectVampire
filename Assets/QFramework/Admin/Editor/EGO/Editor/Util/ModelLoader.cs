using System;
using EGO.Framework;
using UnityEditor;
using UnityEngine;

namespace EGO.Util
{
    public static class ModelLoader<TModel> where TModel : class, new()
    {
        private const string KEY = "EGO_TODOS";

        private static TModel mModel = null;

        public static TModel Model
        {
            get
            {
                if (mModel == null)
                {
                    Load();
                }
                
                return mModel;    
            }
        }

        private static TModel Load()
        {
            var todoContent = EditorPrefs.GetString(KEY, string.Empty);

            Debug.Log(todoContent);
            
            if (string.IsNullOrEmpty(todoContent))
            {
                return mModel = new TModel();
            }
            try
            {
                mModel =  JsonUtility.FromJson<TModel>(todoContent);
            }
            catch (Exception e)
            {
                mModel = ModelUpdater.Update<TModel>(todoContent);
            }

            return mModel;
        }
        
    }

    public static class ModelExtension
    {
        public static void Save<TModel>(this TModel todoList) where TModel : IModel
        {
            Debug.Log("save");
            EditorPrefs.SetString("EGO_TODOS", JsonUtility.ToJson(todoList,true));
        }
    }
}