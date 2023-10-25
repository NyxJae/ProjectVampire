using System;
using UnityEngine;

namespace QFramework
{
    [Serializable]
    public class Achievement
    {
        [Header("标识")]
        public string AchievementID;
        
        
        public bool UnlockedStatus;

        
        public virtual void UnlockAchievement()
        {
            // if the achievement has already been unlocked, we do nothing and exit
            if (UnlockedStatus)
            {
                return;
            }

            UnlockedStatus = true;

        }

        /// <summary>
        /// Locks the achievement.
        /// </summary>
        public virtual void LockAchievement()
        {
            UnlockedStatus = false;
        }



        public virtual Achievement Copy()
        {
            var clone = new Achievement();
            // we use Json utility to store a copy of our achievement, not a reference
            clone = JsonUtility.FromJson<Achievement>(JsonUtility.ToJson(this));
            return clone;
        }
    }
}