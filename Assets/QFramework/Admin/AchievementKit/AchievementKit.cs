using System;
using UnityEngine;

namespace QFramework
{
    public class AchievementKit : Architecture<AchievementKit>
    {
        protected override void Init()
        {
            RegisterModel<IAchievementModel>(new AchievementModel());
        }

        public static void AddAchievement(Achievement achievement)
        {
            var achievementSystem = Interface.GetModel<IAchievementModel>();
            achievementSystem.Add(achievement);
        }

        public static void UnlockAchievement(string achievementID)
        {
            var achievementSystem = Interface.GetModel<IAchievementModel>();
            achievementSystem.UnlockAchievement(achievementID);
        }

        public static Action<Achievement, bool> SaveCommand = (achievement, unlock) =>
            PlayerPrefs.SetInt(achievement.AchievementID, unlock ? 1 : 0);

        public static Func<Achievement, bool> LoadCommand = (achievement) =>
            PlayerPrefs.GetInt(achievement.AchievementID, 0) == 1;

        public static void Load()
        {
            Interface.GetModel<IAchievementModel>().Load();
        }

        public static void Clear()
        {
            Interface.GetModel<IAchievementModel>().Clear();
        }

        public static void SyncSteamAchievements()
        {
            Interface.GetModel<IAchievementModel>().SyncSteamAchievements();
        }
    }
}