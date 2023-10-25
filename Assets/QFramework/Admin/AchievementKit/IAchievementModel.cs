using System.Collections.Generic;
#if !UNITY_WEBGL
#if !DISABLESTEAMWORKS && STEAMWORK
using Steamworks;
#endif

#endif

namespace QFramework
{
    public interface IAchievementModel : IModel
    {
        void Add(Achievement achievement);
        void UnlockAchievement(string achievementId);

        void SyncSteamAchievements();
        void Load();

        void Clear();
    }

    public class AchievementModel : AbstractModel, IAchievementModel
    {
        private readonly Dictionary<string, Achievement> mAchievements = new Dictionary<string, Achievement>()
        {
        };


        protected override void OnInit()
        {
        }

        public void Add(Achievement achievement)
        {
            mAchievements.Add(achievement.AchievementID, achievement);
        }


        public void Load()
        {
            foreach (var achievement in mAchievements.Values)
            {
                achievement.UnlockedStatus = AchievementKit.LoadCommand(achievement);
            }
        }

        public void Clear()
        {
            foreach (var achievement in mAchievements.Values)
            {
                achievement.UnlockedStatus = false;
                AchievementKit.SaveCommand(achievement, achievement.UnlockedStatus);
#if !DISABLESTEAMWORKS && STEAMWORK
                if (SteamManager.Initialized)
                {
                    SteamUserStats.ClearAchievement(achievement.AchievementID);
                }
#endif
            }

#if !DISABLESTEAMWORKS && STEAMWORK
            if (SteamManager.Initialized)
            {
                SteamUserStats.StoreStats();
            }
#endif
        }

        public void SyncSteamAchievements()
        {
#if !DISABLESTEAMWORKS && STEAMWORK

            foreach (var kv in mAchievements)
            {
                if (kv.Value.UnlockedStatus)
                {
                    if (SteamManager.Initialized)
                    {
                        SteamUserStats.GetAchievement(kv.Value.AchievementID, out var unlocked);
                        if (!unlocked)
                        {
                            SteamUserStats.SetAchievement(kv.Value.AchievementID);
                            SteamUserStats.StoreStats();    
                        }
                    }
                }
            }
#endif
        }


        public void UnlockAchievement(string achievementId)
        {
            if (mAchievements.ContainsKey(achievementId))
            {
                var achievement = mAchievements[achievementId];

                if (!achievement.UnlockedStatus)
                {
                    achievement.UnlockedStatus = true;

                    AchievementKit.SaveCommand(achievement, achievement.UnlockedStatus);
                }

#if !DISABLESTEAMWORKS && STEAMWORK
                if (SteamManager.Initialized)
                {
                    SteamUserStats.SetAchievement(achievement.AchievementID);

                    SteamUserStats.StoreStats();
                }
#endif
            }
        }
    }
}