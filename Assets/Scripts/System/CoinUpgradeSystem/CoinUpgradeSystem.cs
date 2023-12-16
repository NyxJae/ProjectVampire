using System.Collections.Generic;
using QFramework;

namespace ProjectVampire.System
{
    public class CoinUpgradeSystem : AbstractSystem
    {
        // 升级项列表
        public List<CoinUpdateItem> CoinUpdateItems { get; } = new();

        protected override void OnInit()
        {
            // 添加升级项
            CoinUpdateItems.Add(new CoinUpdateItem()
                .SetKey("CoinRateUpgrade") // 设置升级项的key
                .SetPrice(5) // 设置升级项的价格
                .SetDescription("升级金币掉落几率") // 设置升级项的描述
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 判断金币数量是否足够
                    if (Global.Coin.Value < item.Price)
                        // 不足够则返回
                        return;
                    // 扣除金币
                    Global.Coin.Value -= item.Price;
                    // 播放音效
                    AudioKit.PlaySound("AbilityLevelUp");
                    // 增加金币掉落几率
                    Global.DropCoinRate.Value += 0.1f;
                }));
        }
    }
}