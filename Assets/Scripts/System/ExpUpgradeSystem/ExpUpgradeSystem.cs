using System.Collections.Generic;
using System.Linq;
using ProjectVampire.System.ExpUpgradeSystem;
using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public class ExpUpgradeSystem : AbstractSystem
    {
        // 升级项列表
        public List<ExpUpgradeItem> ExpUpdateItems { get; } = new();

        // 链式封装 add操作
        private ExpUpgradeItem Add(ExpUpgradeItem item)
        {
            ExpUpdateItems.Add(item);
            return item;
        }

        protected override void OnInit()
        {
            // 添加升级项 雷电场攻击力
            var attackUpgrade = Add(new ExpUpgradeItem()
                .SetKey("LightingSiteATK") // 设置升级项的key
                .SetDescription("雷电场攻击力") // 设置升级项的描述
                .SetMaxLevel(10)
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 增加攻击力
                    Player.Instance.Abilities.LightingSite.Attack *= 1.1f;
                }));
            // 添加升级项 雷电场攻击范围
            var attackRangeUpgrade = Add(new ExpUpgradeItem()
                .SetKey("LightingSiteATKRange") // 设置升级项的key
                .SetDescription("雷电场攻击范围") // 设置升级项的描述
                .SetMaxLevel(10)
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 增加攻击范围
                    Player.Instance.Abilities.LightingSite.transform.localScale *= 1.1f;
                }));
            // 添加升级项 雷电场攻击速度
            var attackSpeedUpgrade = Add(new ExpUpgradeItem()
                .SetKey("LightingSiteATKSpeed") // 设置升级项的key
                .SetDescription("雷电场攻击速度") // 设置升级项的描述
                .SetMaxLevel(10)
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 增加攻击速度
                    Player.Instance.Abilities.LightingSite.AttackRate *= 1.1f;
                }));
            // 添加升级项 拾取范围提升
            var pickUpRangeUpgrade = Add(new ExpUpgradeItem()
                .SetKey("PickUpRangeUpgrade") // 设置升级项的key
                .SetDescription("拾取范围提升") // 设置升级项的描述
                .SetMaxLevel(10)
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 增加拾取范围
                    Player.Instance.Abilities.PickAbility.PickRange.radius *= 1.1f;
                }));
        }

        // 随机获取n个未升级的升级项
        /// <summary>
        ///     随机获取n个未升级的升级项
        /// </summary>
        /// <param name="n"> 随机获取的数量 </param>
        public void RandomUnUpdatedItem(int n)
        {
            // 每个项设为不可见
            foreach (var item in ExpUpdateItems)
                item.IsVisible.Value = false;
            // 获取未升级的项
            var unUpdatedItems = ExpUpdateItems.Where(item => !item.IsUpdated).ToList();
            // 随机获取n个未升级的项
            var randomItems = unUpdatedItems.OrderBy(item => Random.Range(0, 100)).Take(n).ToList();
            // 将n个未升级的项设为可见
            foreach (var item in randomItems) item.IsVisible.Value = true;
        }

        // 重置所有升级项
        public void ResetAll()
        {
            foreach (var item in ExpUpdateItems)
            {
                item.IsUpdated = false;
                item.CurrentLevel.Value = 1;
            }
        }
    }
}