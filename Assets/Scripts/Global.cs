using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectVampire
{
    public class Global : Architecture<Global>
    {
        // 公开的 经验 属性
        public static BindableProperty<int> Exp = new BindableProperty<int>(0);
        // 公开的 等级 属性
        public static BindableProperty<int> Level = new BindableProperty<int>(1);
        // 公开的 时间 属性
        public static BindableProperty<float> Time = new BindableProperty<float>(0);
        // 公开的 血量 属性
        public static BindableProperty<int> Health = new BindableProperty<int>(100);
        // 公开的 最大血量 属性
        public static BindableProperty<int> MaxHealth = new BindableProperty<int>(100);
        // 公开的 金币 属性
        public static BindableProperty<int> Coin = new BindableProperty<int>(0);
        // 公开的 金币掉落几率 属性
        public static BindableProperty<float> DropCoinRate = new BindableProperty<float>(0.1f);
        // 公开的 经验掉落几率 属性
        public static BindableProperty<float> DropExpRate = new BindableProperty<float>(0.5f);
        // 公开的 血瓶掉落几率 属性
        public static BindableProperty<float> DropHPBottleRate = new BindableProperty<float>(0.1f);
        // 定义 常量 场景名称 为 数字
        public const int BeginScene = 0;
        public const int GameScene = 1;


        /// <summary>
        ///  重置各项属性
        /// </summary>
        public static void ResetProperties()
        {
            Exp.Value = 0;
            Level.Value = 1;
            Time.Value = 0;
            Health.Value = 100;
        }

        /// <summary>
        /// 初始化 存储与读取
        /// </summary>
        public static void InitData()
        {
            // 金币 存储与读取
            if (PlayerPrefs.HasKey("Coin"))
            {
                Coin.Value = PlayerPrefs.GetInt("Coin");
            }
            else
            {
                PlayerPrefs.SetInt("Coin", 0);
            }
            // 注册金币增加事件,并存储金币
            Coin.Register(newValue =>
            {
                PlayerPrefs.SetInt("Coin", newValue);
            });
            // 经验掉落几率 存储与读取
            if (PlayerPrefs.HasKey("DropExpRate"))
            {
                DropExpRate.Value = PlayerPrefs.GetFloat("DropExpRate");
            }
            else
            {
                PlayerPrefs.SetFloat("DropExpRate", 0.5f);
            }
            // 注册经验掉落几率增加事件,并存储经验掉落几率
            DropExpRate.Register(newValue =>
            {
                PlayerPrefs.SetFloat("DropExpRate", newValue);
            });
            
            // 金币掉落几率 存储与读取
            if (PlayerPrefs.HasKey("DropCoinRate"))
            {
                DropCoinRate.Value = PlayerPrefs.GetFloat("DropCoinRate");
            }
            else
            {
                PlayerPrefs.SetFloat("DropCoinRate", 0.1f);
            }
            // 注册金币掉落几率增加事件,并存储金币掉落几率
            DropCoinRate.Register(newValue =>
            {
                PlayerPrefs.SetFloat("DropCoinRate", newValue);
            });
            // 最大血量 存储与读取
            if (PlayerPrefs.HasKey("MaxHealth"))
            {
                MaxHealth.Value = PlayerPrefs.GetInt("MaxHealth");
            }
            else
            {
                PlayerPrefs.SetInt("MaxHealth", 100);
            }
            // 注册最大血量增加事件,并存储最大血量
            MaxHealth.Register(newValue =>
            {
                PlayerPrefs.SetInt("MaxHealth", newValue);
            });
            // 血瓶掉落几率 存储与读取
            if (PlayerPrefs.HasKey("DropHPBottleRate"))
            {
                DropHPBottleRate.Value = PlayerPrefs.GetFloat("DropHPBottleRate");
            }
            else
            {
                PlayerPrefs.SetFloat("DropHPBottleRate", 0.1f);
            }
            // 注册血瓶掉落几率增加事件,并存储血瓶掉落几率
            DropHPBottleRate.Register(newValue =>
            {
                PlayerPrefs.SetFloat("DropHPBottleRate", newValue);
            });
        }


        protected override void Init()
        {
            
        }
    }
}

