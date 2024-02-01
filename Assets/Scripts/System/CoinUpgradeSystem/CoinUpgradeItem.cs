using System;
using QFramework;

namespace ProjectVampire
{
    public class CoinUpGradeItem : ISystemItem
    {
        // condition, 用于判断是否可以升级
        private Func<CoinUpGradeItem, bool> _funcCondition;

        // 描述Func
        private Func<int, int, string> _funcDescription;

        // 升级Action
        private Action<CoinUpGradeItem> _onUpgrade;

        // 创建发生更改事件
        public EasyEvent OnCoinUpdateItemChanged = new();


        // 图标名称
        public string IconName { get; private set; }


        // 升级项状态
        public bool IsUpdated { get; private set; }

        // 升级项的key
        public string Key { get; private set; }

        // 升级项的描述
        public string Description => _funcDescription?.Invoke(Lv, Price);

        // 升级项的初始价格
        public int Price { get; private set; }

        // 满级
        public int MaxLv { get; private set; }

        // 当前等级
        public int Lv { get; private set; } = 1;


        // save
        public void Save(SaveUtility saveUtility)
        {
            saveUtility.Save($"{Key}_Level", Lv);
            saveUtility.Save($"{Key}_Price", Price);
            saveUtility.Save($"{Key}_IsUpdated", IsUpdated);
        }

        // 升级方法 
        public void Trigger()
        {
            // 如果升级项的等级小于最大等级, 则升级
            if (Lv <= MaxLv)
            {
                // 等级+1
                Lv++;
                // 执行外部设置的升级方法
                _onUpgrade?.Invoke(this);
                // 提升价格
                Price *= 2;
                // 触发升级项发生更改事件
                OnCoinUpdateItemChanged.Trigger();
            }
            // 如果升级项的等级等于最大等级, 则不升级
            else
            {
                // 设置为已经升级
                IsUpdated = true;
            }
        }

        public void Load(SaveUtility saveUtility)
        {
            Lv = saveUtility.LoadInt($"{Key}_Level", Lv);
            Price = saveUtility.LoadInt($"{Key}_Price", Price);
            IsUpdated = saveUtility.LoadBool($"{Key}_IsUpdated", IsUpdated);
        }


        // 链式封装 setkey
        /// <summary>
        ///     设置升级项的key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public CoinUpGradeItem SetKey(string key)
        {
            Key = key;
            return this;
        }

        // 链式封装 seticonname
        /// <summary>
        ///     设置升级项的图标名称
        /// </summary>
        /// <param name="iconName"></param>
        /// <returns></returns>
        public CoinUpGradeItem SetIconName(string iconName)
        {
            IconName = iconName;
            return this;
        }

        // 链式封装 setdescription
        /// <summary>
        ///     设置升级项的描述
        /// </summary>
        /// <returns></returns>
        public CoinUpGradeItem SetDescription(Func<int, int, string> description)
        {
            _funcDescription = description;
            return this;
        }

        // 链式封装 setonupgrade
        /// <summary>
        ///     设置升级项的升级方法
        /// </summary>
        /// <param name="onUpgrade"></param>
        /// <returns></returns>
        public CoinUpGradeItem SetOnUpgrade(Action<CoinUpGradeItem> onUpgrade)
        {
            _onUpgrade = onUpgrade;
            return this;
        }

        // 链式封装 setprice
        /// <summary>
        ///     设置升级项的价格
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public CoinUpGradeItem SetPrice(int price)
        {
            Price = price;
            return this;
        }

        // 链式封装 setmaxlv
        /// <summary>
        ///     设置升级项的最大等级
        /// </summary>
        /// <param name="maxlv"></param>
        /// <returns></returns>
        public CoinUpGradeItem SetMaxLv(int maxlv)
        {
            MaxLv = maxlv;
            return this;
        }

        // 链式封装 setcondition
        public CoinUpGradeItem SetCondition(Func<CoinUpGradeItem, bool> condition)
        {
            _funcCondition = condition;
            return this;
        }


        /// <summary>
        ///     设置升级项的条件
        /// </summary>
        /// <returns></returns>
        public bool ConditionCheck()
        {
            // 如果有前置依赖条件, 则判断是否满足依赖条件且未升级
            if (_funcCondition != null)
                return !IsUpdated && _funcCondition.Invoke(this);
            // 如果没有前置依赖条件, 则判断是否已经升级
            return !IsUpdated;
        }
    }
}