using EGO.V1;
using UnityEngine;

namespace EGO.Util
{
    public class ModelUpdater
    {
        public static TNewModel Update<TNewModel>(string strContent) where TNewModel : class
        {
            var oldValue = JsonUtility.FromJson<TodoList>(strContent);

            var updateCommand = new ModelUpdateCommandV1();
            updateCommand.Execute(oldValue);

            return updateCommand.Result as TNewModel;
        }
    }

    public class ModelUpdateCommandV1 : UpdateAction<TodoList,V1.TodoList>
    {
        protected override V1.TodoList ConvertOld2New(TodoList oldModel)
        {
            var newTodoList = new V1.TodoList();

            foreach (var oldTodo in oldModel.Todos)
            {
                var newTodo = new V1.Todo();
                newTodo.Content = oldTodo.Content;
                newTodo.State.Value = oldTodo.Finished.Value ? TodoState.Done : TodoState.NotStart;
                newTodoList.Todos.Add(newTodo);
            }

            return newTodoList;
        }
    }
    
    

    public abstract class UpdateAction<TOldModel,TNewModel> 
        where TNewModel : class
        where TOldModel : class
    {
        public TNewModel Result { get; private set; }
        
        public void Execute(object oldValue)
        {
            Result = ConvertOld2New(oldValue as TOldModel);
        }

        protected abstract TNewModel ConvertOld2New(TOldModel oldModel);
    }
}