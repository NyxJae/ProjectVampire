using System;

namespace ProjectVampire.System
{
    public class CoinUpdateItem
    {
        // 升级Action
        private Action<CoinUpdateItem> mOnUpgrade;

        // 升级项的key
        public string Key { get; set; }

        // 升级项的描述
        public string Description { get; set; }

        // 升级项的价格
        public int Price { get; set; }

        // 升级方法
        public void Upgrade()
        {
            mOnUpgrade?.Invoke(this);
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
    }
}