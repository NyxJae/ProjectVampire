using System;
using System.Collections.Generic;
using EGO.Framework;

namespace EGO.V1
{
    [Serializable]
    public enum TodoState
    {
        NotStart,
        Started,
        Done,
    }

    [Serializable]
    public enum TodoPriority
    {
        A,
        B,
        C,
        D,
        None
    }
    
    [Serializable]
    public class Todo
    {
        public string Id = Guid.NewGuid().ToString();

        public string Content;

        public DateTime CreateAt = DateTime.Now;

        public DateTime FinishedAt;

        public DateTime StartTime;

        public Property<TodoState> State = new Property<TodoState>(TodoState.NotStart);

        public Property<TodoPriority> Priority = new Property<TodoPriority>(TodoPriority.None);

        public List<Todo> Children = new List<Todo>();

        public Category Category;

        public bool Hide;
    }

    public static class TodoExtension
    {
        public static TimeSpan UsedTime(this Todo todo)
        {
            return todo.FinishedAt - todo.StartTime;
        }

        public static string UsedTimeText(this Todo todo)
        {
            var usedTime = todo.UsedTime();
            return UsedTime2Text(usedTime);
        }

        public static string UsedTime2Text(TimeSpan usedTime)
        {
            if (usedTime.TotalSeconds < 60)
            {
                return $"花费 {usedTime.Seconds} 秒钟";
            }
            else if (usedTime.TotalMinutes < 60)
            {
                return $"花费 {usedTime.Minutes} 分钟";
            }
            else if (usedTime.TotalHours < 24)
            {
                return $"花费 {usedTime.Hours} 小时 {usedTime.Minutes} 分钟";
            }
            else if (usedTime.TotalDays < 7)
            {
                return $"花费 {usedTime.Hours} 天";
            }

            return $"花费 {usedTime.TotalDays / 7} 周";
        }
    }
}