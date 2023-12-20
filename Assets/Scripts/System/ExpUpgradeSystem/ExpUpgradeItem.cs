using System;
using QFramework;

namespace ProjectVampire.System.ExpUpgradeSystem
{
    public class ExpUpgradeItem
    {
        // condition, 用于判断是否可以升级
        private Func<ExpUpgradeItem, bool> mCondition;

        // 升级Action
        private Action<ExpUpgradeItem> mOnUpgrade;

        // 创建发生更改事件
        public EasyEvent OnExpUpgradeItemChanged = new();

        // 升级项状态
        public bool IsUpdated { get; set; }

        // 升级项的key
        public string Key { get; private set; }

        // 升级项的描述
        public string Description { get; private set; }


        // 升级方法
        public void Upgrade()
        {
            // 执行外部设置的升级方法
            mOnUpgrade?.Invoke(this);
            // 设置升级项状态为已升级
            IsUpdated = true;
            // 触发升级事件
            CoinUpgradeSystem.OnCoinUpgradeSystemChanged.Trigger();
            // 触发升级项发生更改事件
            OnExpUpgradeItemChanged.Trigger();
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
            Description = description;
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


        // 链式封装 setcondition
        public ExpUpgradeItem SetCondition(Func<ExpUpgradeItem, bool> condition)
        {
            mCondition = condition;
            return this;
        }


        /// <summary>
        ///     设置升级项的条件
        /// </summary>
        /// <returns></returns>
        public bool ConditionCheck()
        {
            // 如果有前置依赖条件, 则判断是否满足依赖条件且未升级
            if (mCondition != null)
                return !IsUpdated && mCondition.Invoke(this);
            // 如果没有前置依赖条件, 则判断是否已经升级
            return !IsUpdated;
        }
    }
}