using System.Collections.Generic;
using UnityEngine;

namespace QFramework
{
    public class AddAchievements : MonoBehaviour
    {
        public List<Achievement> Achievements;


        public void Execute()
        {
            if (Achievements != null && Achievements.Count > 0)
            {
                foreach (var achievement in Achievements)
                {
                    AchievementKit.AddAchievement(achievement);
                }
            }
        }
    }
}