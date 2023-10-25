using System;
using System.Collections.Generic;
using System.Linq;
using EGO.Framework.Util;
using EGO.Logic;
using EGO.Util;
using EGO.V1;
using UnityEngine;

namespace EGO
{
    public static class Model
    {
        public static void CreateTodo(string content,bool hide = false)
        {
            var newTodo = new V1.Todo {Content = content, Hide = hide};

            mModel.Todos.Add(newTodo);
        }

        public static void AddTodo(V1.Todo todo)
        {
            mTodosForId.Add(todo.Id, todo);
            mModel.Todos.Add(todo);
        }

        public static IEnumerable<V1.Todo> Todos => mModel.Todos;
        
        
        public static IEnumerable<Category> Categories => mModel.Categories;

        public static IEnumerable<Note> Notes => mModel.Notes;

        public static IEnumerable<Product> Products => mModel.Products;

        public static void RemoveNote(Note note)
        {
            mModel.Notes.Remove(note);
        }


        public static void Save()
        {
            mModel.Save();
        }


        private static bool mInited = false;
        
        private static V1.TodoList mModel
        {
            get
            {
                if (!mInited)
                {   
                    var todos = ModelLoader<V1.TodoList>.Model.Todos;

                    foreach (var todo1 in todos.Where(todo=>todo.Id == "00000000-0000-0000-0000-000000000000"))
                    {
                        todo1.Id = Guid.NewGuid().ToString();
                    }
                    
                    mTodosForId = todos.ToDictionary(todo => todo.Id);
                    
                    
                    mInited = true;
                }
                return ModelLoader<V1.TodoList>.Model;
            }
        }
        
        static Dictionary<string,V1.Todo> mTodosForId  = new Dictionary<string, V1.Todo>();

        public static V1.Todo GetTodo(string id)
        {
            if (mTodosForId.ContainsKey(id))
            {
                return mTodosForId[id];
            }    
            
            Debug.LogError(id);
            return new V1.Todo()
            {
                Id = id
            };
        }

        public static void CreateNote(string content)
        {
            mModel.Notes.Add(new Note() {Content = content});
        }


        public static int IndexOfCategory(Category dataCategory)
        {
            return mModel.Categories.IndexOf(dataCategory);
        }

        public static void RemoveCategory(Category category)
        {
            mModel.Categories.Remove(category);
        }

        public static void CreateCategory(string name, Color color)
        {
            ModelLoader<V1.TodoList>.Model.Categories.Add(new Category()
            {
                Name = name,
                Color = color.ToText()
            });
        }

        public static void RemoveTodo(V1.Todo data)
        {
            mTodosForId.Remove(data.Id);
            mModel.Todos.Remove(data);
        }

        public static Category GetCategoryByIndex(int indexPropertyValue)
        {
            return mModel.Categories[indexPropertyValue];
        }

        public static void LogJson()
        {
            Debug.Log(JsonUtility.ToJson(ModelLoader<V1.TodoList>.Model,true));
        }

        public static void CreateProduct(string productName, string productDescription)
        {
            mModel.Products.Add(new Product()
            {
                Name = productName,
                Description = productDescription
            });
        }

        public static void DeleteProduct(Product product)
        {
            mModel.Products.Remove(product);
        }
    }
}