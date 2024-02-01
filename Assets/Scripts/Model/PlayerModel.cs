using QFramework;

namespace ProjectVampire
{
    public class PlayerModel : AbstractModel
    {
        // 公开的 暴击伤害倍数 属性
        public BindableProperty<float> CriticalMultiplier = new(2.0f); // 默认暴击伤害倍数为 200%

        // 公开的 暴击率 属性
        public BindableProperty<float> CriticalRate = new(0.1f); // 默认暴击率为 10%

        // 公开的 经验 属性
        public BindableProperty<float> Exp = new();

        // 公开的 血量 属性
        public BindableProperty<float> Health = new(10);

        // 公开的 等级 属性
        public BindableProperty<int> Level = new(1);

        // 经验升级所需经验值
        public BindableProperty<float> MaxExp = new(10);

        // 公开的 最大血量 属性
        public BindableProperty<float> MaxHealth = new(10);

        /// <summary>
        ///     重置各项属性
        /// </summary>
        public void Reset()
        {
            Exp.Value = 0;
            MaxExp.Value = 10;
            Level.Value = 1;
            Health.Value = MaxHealth.Value;
        }

        protected override void OnInit()
        {
        }
    }
}