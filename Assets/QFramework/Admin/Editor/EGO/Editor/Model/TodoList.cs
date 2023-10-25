using System;
using System.Collections.Generic;
using EGO.Framework;
using EGO.Logic;

namespace EGO.V1
{
    [Serializable]
    public class TodoList : IModel
    {   
        public List<Todo> Todos = new List<Todo>();
        
        public List<Category> Categories = new List<Category>();
        
        public List<Note> Notes = new List<Note>();
        
        public List<Product> Products = new List<Product>();
    }
}