using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public class Global : Architecture<Global>
    {
        // 定义 常量 场景名称 为 数字
        public const int BeginScene = 0;

        public const int GameScene = 1;

        // 公开的 经验 属性
        public static BindableProperty<float> Exp = new();

        // 经验升级所需经验值
        public static BindableProperty<float> MaxExp = new();

        // 公开的 等级 属性
        public static BindableProperty<int> Level = new(1);

        // 公开的 时间 属性
        public static BindableProperty<float> Time = new();

        // 公开的 血量 属性
        public static BindableProperty<int> Health = new(10);

        // 公开的 最大血量 属性
        public static BindableProperty<int> MaxHealth = new(10);

        // 公开的 金币 属性
        public static BindableProperty<int> Coin = new();

        // 公开的 金币掉落几率 属性
        public static BindableProperty<float> DropCoinRate = new(0.1f);

        // 公开的 经验掉落几率 属性
        public static BindableProperty<float> DropExpRate = new(0.5f);

        // 公开的 血瓶掉落几率 属性
        public static BindableProperty<float> DropHPBottleRate = new(0.1f);

        // 公开的 炸弹掉落几率 属性
        public static BindableProperty<float> DropBombRate = new(0.1f);

        // 公开的 吸铁石掉落几率 属性
        public static BindableProperty<float> DropMagnetRate = new(0.1f);


        /// <summary>
        ///     重置各项属性
        /// </summary>
        public static void ResetProperties()
        {
            Exp.Value = 0;
            MaxExp.Value = 10;
            Level.Value = 1;
            Time.Value = 0;
            Health.Value = MaxHealth.Value;
        }

        /// <summary>
        ///     初始化 存储与读取
        /// </summary>
        public static void InitData()
        {
            // 金币 存储与读取
            if (PlayerPrefs.HasKey("Coin"))
                Coin.Value = PlayerPrefs.GetInt("Coin");
            else
                PlayerPrefs.SetInt("Coin", 0);
            // 注册金币增加事件,并存储金币
            Coin.Register(newValue => { PlayerPrefs.SetInt("Coin", newValue); });
            // 经验掉落几率 存储与读取
            if (PlayerPrefs.HasKey("DropExpRate"))
                DropExpRate.Value = PlayerPrefs.GetFloat("DropExpRate");
            else
                PlayerPrefs.SetFloat("DropExpRate", 0.5f);
            // 注册经验掉落几率增加事件,并存储经验掉落几率
            DropExpRate.Register(newValue => { PlayerPrefs.SetFloat("DropExpRate", newValue); });

            // 金币掉落几率 存储与读取
            if (PlayerPrefs.HasKey("DropCoinRate"))
                DropCoinRate.Value = PlayerPrefs.GetFloat("DropCoinRate");
            else
                PlayerPrefs.SetFloat("DropCoinRate", 0.1f);
            // 注册金币掉落几率增加事件,并存储金币掉落几率
            DropCoinRate.Register(newValue => { PlayerPrefs.SetFloat("DropCoinRate", newValue); });
            // 最大血量 存储与读取
            if (PlayerPrefs.HasKey("MaxHealth"))
                MaxHealth.Value = PlayerPrefs.GetInt("MaxHealth");
            else
                PlayerPrefs.SetInt("MaxHealth", 100);
            // 注册最大血量增加事件,并存储最大血量
            MaxHealth.Register(newValue => { PlayerPrefs.SetInt("MaxHealth", newValue); });
            // 血瓶掉落几率 存储与读取
            if (PlayerPrefs.HasKey("DropHPBottleRate"))
                DropHPBottleRate.Value = PlayerPrefs.GetFloat("DropHPBottleRate");
            else
                PlayerPrefs.SetFloat("DropHPBottleRate", 0.1f);
            // 注册血瓶掉落几率增加事件,并存储血瓶掉落几率
            DropHPBottleRate.Register(newValue => { PlayerPrefs.SetFloat("DropHPBottleRate", newValue); });
            // 炸弹掉落几率 存储与读取
            if (PlayerPrefs.HasKey("DropBombRate"))
                DropBombRate.Value = PlayerPrefs.GetFloat("DropBombRate");
            else
                PlayerPrefs.SetFloat("DropBombRate", 0.1f);
            // 注册炸弹掉落几率增加事件,并存储炸弹掉落几率
            DropBombRate.Register(newValue => { PlayerPrefs.SetFloat("DropBombRate", newValue); });
            // 吸铁石掉落几率 存储与读取
            if (PlayerPrefs.HasKey("DropMagnetRate"))
                DropMagnetRate.Value = PlayerPrefs.GetFloat("DropMagnetRate");
            else
                PlayerPrefs.SetFloat("DropMagnetRate", 0.1f);
            // 注册吸铁石掉落几率增加事件,并存储吸铁石掉落几率
            DropMagnetRate.Register(newValue => { PlayerPrefs.SetFloat("DropMagnetRate", newValue); });
        }


        protected override void Init()
        {
            RegisterSystem(new CoinUpgradeSystem());
            RegisterSystem(new SaveSystem());
            RegisterSystem(new ExpUpgradeSystem());
        }
    }
}