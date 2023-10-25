#if PLAYMAKER
using HutongGames.PlayMaker;
using QFramework;

namespace GamePixEngine
{
    [ActionCategory("GamePixEngine/AchievementKit 成就")]
    public class UnlockAchievement : FsmStateAction
    {
        public FsmString AchievementID;

        public override void OnEnter()
        {
            base.OnEnter();

#if !DISABLESTEAMWORKS
            AchievementKit.UnlockAchievement(AchievementID.Value);
#endif
            Finish();
        }
    }
}
#endif