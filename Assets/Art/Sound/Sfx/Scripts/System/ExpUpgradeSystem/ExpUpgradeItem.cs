using System;
using QFramework;

namespace ProjectVampire.System.ExpUpgradeSystem
{
    public class ExpUpgradeItem
    {
        // 升级Action
        private Action<ExpUpgradeItem> mOnUpgrade;


        // 升级项状态
        public bool IsUpdated { get; set; }

        // 升级项的key
        public string Key { get; private set; }

        // 升级项的描述
        public string Description { get; private set; }

        // 最大等级
        public int MaxLevel { get; private set; }

        // 是否可见
        public BindableProperty<bool> IsVisible { get; } = new();

        // 当前等级
        public BindableProperty<int> CurrentLevel { get; } = new(1);


        // 升级方法
        public void Upgrade()
        {
            // 执行外部设置的升级方法
            mOnUpgrade?.Invoke(this);
            // 升级
            CurrentLevel.Value++;
            // 如果升级到最大等级, 则设置为已经升级
            if (CurrentLevel.Value == MaxLevel)
                IsUpdated = true;
        }


        // 链式封装 setkey
        /// <summary>
        ///     设置升级项的key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ExpUpgradeItem SetKey(string key)
        {
            Key = key;
            return this;
        }

        // 链式封装 setdescription
        /// <summary>
        ///     设置升级项的描述
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public ExpUpgradeItem SetDescription(string description)
        {
            Description = $"{description}";
            return this;
        }

        // 链式封装 setmaxlevel
        /// <summary>
        ///     设置升级项的最大等级
        /// </summary>
        /// <param name="maxLevel"></param>
        /// <returns></returns>
        public ExpUpgradeItem SetMaxLevel(int maxLevel)
        {
            MaxLevel = maxLevel;
            return this;
        }

        // 链式封装 setonupgrade
        /// <summary>
        ///     设置升级项的升级方法
        /// </summary>
        /// <param name="onUpgrade"></param>
        /// <returns></returns>
        public ExpUpgradeItem SetOnUpgrade(Action<ExpUpgradeItem> onUpgrade)
        {
            mOnUpgrade = onUpgrade;
            return this;
        }
    }
}