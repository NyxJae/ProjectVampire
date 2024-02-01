using QFramework;

namespace ProjectVampire
{
    public class Global : Architecture<Global>
    {
        // 定义 常量 场景名称 为 数字
        public const int BeginScene = 0;

        public const int GameScene = 1;

        protected override void Init()
        {
            AudioKit.PlaySoundMode = AudioKit.PlaySoundModes.IgnoreSameSoundInGlobalFrames;

            RegisterSystem(new CoinUpgradeSystem());
            RegisterSystem(new ExpUpgradeSystem());
            RegisterSystem(new AchievementSystem());

            RegisterModel(new GlobalModel());
            RegisterModel(new PlayerModel());

            RegisterUtility(new SaveUtility());
        }
    }
}