using QFramework;

namespace ProjectVampire
{
    public class GlobalModel : AbstractModel
    {
        // 公开的 金币 属性
        public BindableProperty<int> Coin = new();

        // 公开的 炸弹掉落几率 属性
        public BindableProperty<float> DropBombRate = new(Config.DefaultDropBombRate);

        // 公开的 金币掉落几率 属性
        public BindableProperty<float> DropCoinRate = new(Config.DefaultDropCoinRate);

        // 公开的 经验掉落几率 属性
        public BindableProperty<float> DropExpRate = new(Config.DefaultDropExpRate);

        // 公开的 血瓶掉落几率 属性
        public BindableProperty<float> DropHPBottleRate = new(Config.DefaultDropHPBottleRate);

        // 公开的 吸铁石掉落几率 属性
        public BindableProperty<float> DropMagnetRate = new(Config.DefaultDropMagnetRate);

        // saveUtility
        private SaveUtility saveUtility;

        // 公开的 时间 属性
        public BindableProperty<float> Time = new();

        /// <summary>
        ///     重置各项属性
        /// </summary>
        public void Reset()
        {
            Time.Value = 0;
        }

        protected override void OnInit()
        {
            // saveUtility
            saveUtility = this.GetUtility<SaveUtility>();
            Time.Value = 0;
            // 注册coin更改 事件
            Coin.Register(newValue => saveUtility.Save("Coin", newValue));
            // 注册DropExpRate更改 事件
            DropExpRate.Register(newValue => saveUtility.Save("DropExpRate", newValue));
            // 注册DropCoinRate更改 事件
            DropCoinRate.Register(newValue => saveUtility.Save("DropCoinRate", newValue));
            // 注册DropHPBottleRate更改 事件
            DropHPBottleRate.Register(newValue => saveUtility.Save("DropHPBottleRate", newValue));
            // 注册DropBombRate更改 事件
            DropBombRate.Register(newValue => saveUtility.Save("DropBombRate", newValue));
            // 注册DropMagnetRate更改 事件
            DropMagnetRate.Register(newValue => saveUtility.Save("DropMagnetRate", newValue));
            Load();
        }

        public void Load()
        {
            Coin.Value = saveUtility.LoadInt("Coin");
            DropExpRate.Value = saveUtility.LoadFloat("DropExpRate", Config.DefaultDropExpRate);
            DropCoinRate.Value = saveUtility.LoadFloat("DropCoinRate", Config.DefaultDropCoinRate);
            DropHPBottleRate.Value = saveUtility.LoadFloat("DropHPBottleRate", Config.DefaultDropHPBottleRate);
            DropBombRate.Value = saveUtility.LoadFloat("DropBombRate", Config.DefaultDropBombRate);
            DropMagnetRate.Value = saveUtility.LoadFloat("DropMagnetRate", Config.DefaultDropMagnetRate);
        }
    }
}