using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EGO.V1;

namespace EGO.Logic
{
    public class Version
    {
        public int Major;
        public int Middle;
        public int Small;

        public Version(int maigor, int middle, int small)
        {
            Major = maigor;
            Middle = middle;
            Small = small;
        }

        public override string ToString()
        {
            return $"v{Major}.{Middle}.{Small}";
        }

        public int VersionNumber => Major * 10000 + Middle * 100 + Small;
    }

    public class ProductVersion
    {
        public string Id = Guid.NewGuid().ToString();

        public string Name;

        public Version Version;
        
        public List<Note> Notes = new List<Note>();

        public IEnumerable<V1.Todo> Todos
        {
            get { return TodoIds.Select(id => Model.GetTodo(id)); }
        } 

        public void AddTodo(V1.Todo todo)
        {
            TodoIds.Add(todo.Id);
            Model.AddTodo(todo);
        }

        public void RemoveTodo(V1.Todo todo)
        {
            TodoIds.Remove(todo.Id);
            Model.RemoveTodo(todo);
        }
        
        

        public List<string> TodoIds = new List<string>();

        public TodoState State = TodoState.NotStart;
    }

    /// <summary>
    /// 特性的意思，也是功能的意思
    /// </summary>
    public class Feature
    {   
        public string Name;

        public string Description;

        public List<Feature> Children = new List<Feature>();
    }

    public class Product
    {
        public string Name;
        
        public string Description;
        
        public List<Feature> Features = new List<Feature>();

        public List<ProductVersion> Versions = new List<ProductVersion>();
    }
}