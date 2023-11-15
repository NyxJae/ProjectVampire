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
    }

}
