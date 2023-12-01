using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectVampire
{
    public class Global
    {
        // 公开的 经验 属性
        public static BindableProperty<int> Exp = new BindableProperty<int>(0);
        // 公开的 等级 属性
        public static BindableProperty<int> Level = new BindableProperty<int>(1);
        // 公开的 时间 属性
        public static BindableProperty<float> Time = new BindableProperty<float>(0);
        // 公开的 金币 属性
        public static BindableProperty<int> Coin = new BindableProperty<int>(0);
        // 定义 常量 场景名称 为 数字
        public const int BeginScene = 0;
        public const int GameScene = 1;
        
        // 当场景加载时,重置各项属性
        public static void ResetProperties()
        {
            Exp.Value = 0;
            Level.Value = 1;
            Time.Value = 0;
        }
    }
}

