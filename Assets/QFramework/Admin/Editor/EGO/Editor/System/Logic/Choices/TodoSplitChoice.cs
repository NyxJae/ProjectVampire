
using System.Collections.Generic;

namespace EGO.System.Logic
{
    public static partial class Script
    {
        static void CreateTodo(string content, List<V1.Todo> todosAddTo)
        {
            if (todosAddTo == null)
            {
                Model.CreateTodo(content);
            }
            else
            {
                todosAddTo.Add(new V1.Todo()
                {
                    Content = content
                });
            }

            Model.Save();
        }

        public static ProcessSystem TodoSplitChoice(this ProcessSystem processSystem,List<V1.Todo> todosAddTo = null)
        {
            var firstInputContent = string.Empty;
            var secondInputContent = string.Empty;

            return processSystem.BeginChoice("拆解多步")
                .BeginQuestion()
                .Title("先做什么?")
                .TextArea(string.Empty, input => firstInputContent = input)
                .Menu("保存", () => { CreateTodo(firstInputContent,todosAddTo); })
                .End()

                .BeginQuestion()
                .Title("接着做什么?")
                .TextArea(string.Empty, input => secondInputContent = input)
                .RepeatSelfMenu("保存", () =>
                {
                    CreateTodo(secondInputContent,todosAddTo);
                })
                .Menu("保存并结束", () =>
                {
                    CreateTodo(secondInputContent,todosAddTo); 
                })
                .End()
                .End();
        }

        public static ProcessSystem TodoSplitQuestion(this ProcessSystem processSystem, List<V1.Todo> todosAddTo = null)
        {
            var firstInputContent = string.Empty;
            var secondInputContent = string.Empty;

            return processSystem
                .BeginQuestion()
                .Title("先做什么?")
                .TextArea(string.Empty, input => firstInputContent = input)
                .Menu("保存", () => { CreateTodo(firstInputContent, todosAddTo); })
                .End()

                .BeginQuestion()
                .Title("接着做什么?")
                .TextArea(string.Empty, input => secondInputContent = input)
                .RepeatSelfMenu("保存", () => { CreateTodo(secondInputContent, todosAddTo); })
                .Menu("保存并结束", () => { CreateTodo(secondInputContent, todosAddTo); })
                .End();
        }

    }
}