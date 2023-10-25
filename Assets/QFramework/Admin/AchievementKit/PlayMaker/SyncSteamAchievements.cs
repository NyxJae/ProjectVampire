#if PLAYMAKER
using HutongGames.PlayMaker;
using QFramework;

namespace GamePixEngine
{
    [ActionCategory("GamePixEngine/AchievementKit 成就")]
    public class SyncSteamAchievements : FsmStateAction
    {
        public override void OnEnter()
        {
            base.OnEnter();

#if !DISABLESTEAMWORKS
            AchievementKit.SyncSteamAchievements();
#endif

            Finish();
        }
    }
}
#endif