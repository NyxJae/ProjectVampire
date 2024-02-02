using QFramework;

namespace ProjectVampire
{
    public class PlayerModel : AbstractModel
    {
        // 公开的 暴击伤害倍数 属性
        public BindableProperty<float> CriticalMultiplier = new(Config.DefaultCriticalMultiplier); // 默认暴击伤害倍数为 200%

        // 公开的 暴击率 属性
        public BindableProperty<float> CriticalRate = new(Config.DefaultCriticalRate); // 默认暴击率为 10%

        // 公开的 经验 属性
        public BindableProperty<float> Exp = new(Config.DefaultExp);

        // 公开的 血量 属性
        public BindableProperty<float> Health = new();

        // 公开的 等级 属性
        public BindableProperty<int> Level = new(Config.DefaultLevel);

        // 经验升级所需经验值
        public BindableProperty<float> MaxExp = new(Config.DefaultMaxExp);

        // 公开的 最大血量 属性
        public BindableProperty<float> MaxHealth = new(Config.DefaultMaxHealth);

        /// <summary>
        ///     重置各项属性
        /// </summary>
        public void Reset()
        {
            Exp.Value = Config.DefaultExp;
            MaxExp.Value = Config.DefaultMaxExp;
            Level.Value = Config.DefaultLevel;
            Health.Value = MaxHealth.Value;
            CriticalRate.Value = Config.DefaultCriticalRate;
            CriticalMultiplier.Value = Config.DefaultCriticalMultiplier;
        }

        protected override void OnInit()
        {
            MaxHealth.Value = this.GetUtility<SaveUtility>().LoadFloat("MaxHealth", Config.DefaultMaxHealth);
            Reset();
        }
    }
}