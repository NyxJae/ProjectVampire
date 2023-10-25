using UnityEngine;

namespace QFramework
{
    public class UnlockAchievement : MonoBehaviour
    {
        public string AchievementID;

        public void Execute()
        {
            if (AchievementID.IsNotNullAndEmpty())
            {
                AchievementKit.UnlockAchievement(AchievementID);
            }
        }

    }
}