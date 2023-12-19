using System.Collections.Generic;
using QFramework;

namespace ProjectVampire
{
    public class CoinUpgradeSystem : AbstractSystem, ICanSave
    {
        // 升级事件
        public static EasyEvent OnCoinUpgradeSystemChanged = new();

        // 升级项列表
        public List<CoinUpGradeItem> CoinUpdateItems { get; } = new();

        public void Save()
        {
            // 获取保存系统
            var saveSystem = this.GetSystem<SaveSystem>();
            // 保存升级项的状态
            foreach (var coinUpdateItem in CoinUpdateItems)
                saveSystem.Save(coinUpdateItem.Key, coinUpdateItem.IsUpdated);
        }

        public void Load()
        {
            // 获取保存系统
            var saveSystem = this.GetSystem<SaveSystem>();
            // 读取升级项的状态
            foreach (var coinUpdateItem in CoinUpdateItems)
                coinUpdateItem.IsUpdated = saveSystem.LoadBool(coinUpdateItem.Key);
        }

        // 链式封装 add操作
        private CoinUpGradeItem Add(CoinUpGradeItem item)
        {
            CoinUpdateItems.Add(item);
            return item;
        }

        protected override void OnInit()
        {
            // 添加升级项 金币掉落几率LV1
            var coinRateUpgradeLv1 = Add(new CoinUpGradeItem()
                .SetKey("CoinRateUpgradeLV1") // 设置升级项的key
                .SetPrice(10) // 设置升级项的价格
                .SetDescription("升级金币掉落几率LV1") // 设置升级项的描述
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 扣除金币
                    Global.Coin.Value -= item.Price;
                    // 增加金币掉落几率
                    Global.DropCoinRate.Value += 0.1f;
                }));
            // 添加升级项 金币掉落几率LV2
            var coinRateUpgradeLv2 = Add(new CoinUpGradeItem()
                .SetKey("CoinRateUpgradeLV2") // 设置升级项的key
                .SetPrice(20) // 设置升级项的价格
                .SetDescription("升级金币掉落几率LV2") // 设置升级项的描述
                .SetCondition(item => coinRateUpgradeLv1.IsUpdated) // 设置升级项的依赖条件
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 扣除金币
                    Global.Coin.Value -= item.Price;
                    // 增加金币掉落几率
                    Global.DropCoinRate.Value += 0.1f;
                }));
            // 添加升级项 金币掉落几率LV3
            var coinRateUpgradeLv3 = Add(new CoinUpGradeItem()
                .SetKey("CoinRateUpgradeLv3") // 设置升级项的key
                .SetPrice(30) // 设置升级项的价格
                .SetDescription("升级金币掉落几率LV3") // 设置升级项的描述
                .SetCondition(item => coinRateUpgradeLv2.IsUpdated) // 设置升级项的依赖条件
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 扣除金币
                    Global.Coin.Value -= item.Price;
                    // 增加金币掉落几率
                    Global.DropCoinRate.Value += 0.1f;
                }));

            // 添加升级项 经验掉落几率
            CoinUpdateItems.Add(new CoinUpGradeItem()
                .SetKey("ExpRateUpgradeLv1") // 设置升级项的key
                .SetPrice(5) // 设置升级项的价格
                .SetDescription("升级经验掉落几率Lv1") // 设置升级项的描述
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 扣除金币
                    Global.Coin.Value -= item.Price;
                    // 增加经验掉落几率
                    Global.DropExpRate.Value += 0.1f;
                }));
            // 读取升级项的状态
            Load();

            //注册升级事件
            OnCoinUpgradeSystemChanged.Register(() =>
            {
                // 保存升级项的状态
                Save();
            });
        }
    }
}