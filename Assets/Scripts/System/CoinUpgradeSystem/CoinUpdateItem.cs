using System;
using UnityEngine;

namespace ProjectVampire.System
{
    public class CoinUpdateItem
    {
        // condition, 用于判断是否可以升级
        private Func<CoinUpdateItem, bool> mCondition;

        // 升级Action
        private Action<CoinUpdateItem> mOnUpgrade;

        // 升级项状态
        public bool IsUpdated { get; private set; }

        // 升级项的key
        public string Key { get; private set; }

        // 升级项的描述
        public string Description { get; private set; }

        // 升级项的价格
        public int Price { get; private set; }

        // 升级方法
        public void Upgrade()
        {
            // 执行外部设置的升级方法
            mOnUpgrade?.Invoke(this);
            // 设置升级项状态为已升级
            IsUpdated = true;
            // 触发升级事件
            CoinUpgradeSystem.OnCoinUpgradeSystemChanged.Trigger();
        }

        // 链式封装 setkey
        /// <summary>
        ///     设置升级项的key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public CoinUpdateItem SetKey(string key)
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
        public CoinUpdateItem SetDescription(string description)
        {
            Description = description + $"|价格:{Price}金币";
            return this;
        }

        // 链式封装 setonupgrade
        /// <summary>
        ///     设置升级项的升级方法
        /// </summary>
        /// <param name="onUpgrade"></param>
        /// <returns></returns>
        public CoinUpdateItem SetOnUpgrade(Action<CoinUpdateItem> onUpgrade)
        {
            mOnUpgrade = onUpgrade;
            return this;
        }

        // 链式封装 setprice
        /// <summary>
        ///     设置升级项的价格
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public CoinUpdateItem SetPrice(int price)
        {
            Price = price;
            return this;
        }

        // 链式封装 setcondition
        public CoinUpdateItem SetCondition(Func<CoinUpdateItem, bool> condition)
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