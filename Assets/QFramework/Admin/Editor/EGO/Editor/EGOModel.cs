using System;
using System.Collections.Generic;
using EGO.Framework;
namespace EGO
{
    [Serializable]
    public class TodoList
    {   
        public List<Todo> Todos = new List<Todo>();
    }
    
    [Serializable]
    public class Todo
    {
        public string Content;

        public Property<bool> Finished  =  new Property<bool>();
    }
}

