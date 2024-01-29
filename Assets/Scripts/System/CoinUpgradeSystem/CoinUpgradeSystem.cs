using System.Collections.Generic;
using QAssetBundle;
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
            var saveSystem = this.GetSystem<SaveSystem>();
            foreach (var coinUpdateItem in CoinUpdateItems)
            {
                // 保存升级项的等级和价格
                saveSystem.Save($"{coinUpdateItem.Key}_Level", coinUpdateItem.lv);
                saveSystem.Save($"{coinUpdateItem.Key}_Price", coinUpdateItem.Price);
                saveSystem.Save($"{coinUpdateItem.Key}_IsUpdated", coinUpdateItem.IsUpdated);
            }
        }

        public void Load()
        {
            var saveSystem = this.GetSystem<SaveSystem>();
            foreach (var coinUpdateItem in CoinUpdateItems)
            {
                // 读取升级项的等级和价格
                coinUpdateItem.lv = saveSystem.LoadInt($"{coinUpdateItem.Key}_Level", 1);
                coinUpdateItem.Price = saveSystem.LoadInt($"{coinUpdateItem.Key}_Price", 10);
                coinUpdateItem.IsUpdated = saveSystem.LoadBool($"{coinUpdateItem.Key}_IsUpdated");
            }
        }

        // 链式封装 add操作
        private CoinUpGradeItem Add(CoinUpGradeItem item)
        {
            CoinUpdateItems.Add(item);
            return item;
        }

        protected override void OnInit()
        {
            // 添加升级项 经验掉落几率
            var expRateUpgradeLv1 = Add(new CoinUpGradeItem()
                .SetKey("ExpRateUpgrade") // 设置升级项的key
                .SetPrice(5) // 设置升级项的初始价格
                .SetMaxLv(10) // 设置升级项的最大等级
                .SetIconName(Icons.EXPDROPICON)
                .SetDescription((lv, price) => $"经验掉落几率LV{lv}|价格:{price}金币") // 设置升级项的描述
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 扣除金币
                    Global.Coin.Value -= item.Price;
                    // 增加经验掉落几率
                    Global.DropExpRate.Value += 0.1f;
                }));

            // 添加升级项 金币掉落几率
            var coinRateUpgradeLv1 = Add(new CoinUpGradeItem()
                .SetKey("CoinRateUpgrade") // 设置升级项的key
                .SetPrice(10) // 设置升级项的初始价格
                .SetMaxLv(10) // 设置升级项的最大等级
                .SetIconName(Icons.GOLDDROPICON)
                .SetDescription((lv, price) => $"金币掉落几率LV{lv}|价格:{price}金币") // 设置升级项的描述
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 扣除金币
                    Global.Coin.Value -= item.Price;
                    // 增加金币掉落几率
                    Global.DropCoinRate.Value += 0.1f;
                }));

            // 添加升级项 血瓶掉落几率
            var bloodDropRateUpgrade = Add(new CoinUpGradeItem()
                .SetKey("BloodDropRateUpgrade") // 设置升级项的key
                .SetPrice(15) // 设置升级项的初始价格
                .SetMaxLv(10) // 设置升级项的最大等级
                .SetIconName(Icons.HPBOTTLEDROPICON) // 设置升级项的图标
                .SetDescription((lv, price) => $"血瓶掉落几率LV{lv}|价格:{price}金币") // 设置升级项的描述
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 扣除金币
                    Global.Coin.Value -= item.Price;
                    // 增加血瓶掉落几率
                    Global.DropHPBottleRate.Value += 0.1f;
                }));
            // 添加升级项 磁铁掉落几率
            var magnetDropRateUpgrade = Add(new CoinUpGradeItem()
                .SetKey("MagnetDropRateUpgrade") // 设置升级项的key
                .SetPrice(20) // 设置升级项的初始价格
                .SetMaxLv(10) // 设置升级项的最大等级
                .SetIconName(Icons.MAGNETICON) // 设置升级项的图标
                .SetDescription((lv, price) => $"磁铁掉落几率LV{lv}|价格:{price}金币") // 设置升级项的描述
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 扣除金币
                    Global.Coin.Value -= item.Price;
                    // 增加磁铁掉落几率
                    Global.DropMagnetRate.Value += 0.1f;
                }));
            // 添加升级项 炸弹掉落几率
            var bombDropRateUpgrade = Add(new CoinUpGradeItem()
                .SetKey("BombDropRateUpgrade") // 设置升级项的key
                .SetPrice(25) // 设置升级项的初始价格
                .SetMaxLv(10) // 设置升级项的最大等级
                .SetIconName(Icons.BOMBICON) // 设置升级项的图标
                .SetDescription((lv, price) => $"炸弹掉落几率LV{lv}|价格:{price}金币") // 设置升级项的描述
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 扣除金币
                    Global.Coin.Value -= item.Price;
                    // 增加炸弹掉落几率
                    Global.DropBombRate.Value += 0.1f;
                }));

            // 添加升级项 最大血量
            var maxHealthUpgrade = Add(new CoinUpGradeItem()
                .SetKey("MaxHealthUpgrade") // 设置升级项的key
                .SetPrice(30) // 设置升级项的初始价格
                .SetMaxLv(10) // 设置升级项的最大等级
                .SetIconName(Icons.MAXHPBOOSTICON) // 设置升级项的图标
                .SetDescription((lv, price) => $"最大血量LV{lv}|价格:{price}金币") // 设置升级项的描述
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 扣除金币
                    Global.Coin.Value -= item.Price;
                    // 增加最大血量
                    Global.MaxHealth.Value += 10;
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