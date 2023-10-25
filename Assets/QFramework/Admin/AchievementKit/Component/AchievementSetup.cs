using System.Collections.Generic;
using UnityEngine;

namespace QFramework
{
    public class AchievementSetup : MonoBehaviour
    {
        public List<Achievement> Achievements = new List<Achievement>();
        
        private void Awake()
        {
            foreach (var achievement in Achievements)
            {
                AchievementKit.AddAchievement(achievement);
            }
        }
    }
}