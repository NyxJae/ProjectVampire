using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ProjectVampire
{
    public class Global
    {
        // 公开的 的 经验 属性
        public static BindableProperty<int> Exp = new BindableProperty<int>(0);
        // 公开的 的 等级 属性
        public static BindableProperty<int> Level = new BindableProperty<int>(1);
    }

}
