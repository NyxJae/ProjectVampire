﻿using System.Collections.Generic;
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
            // 添加升级项 雷电场
            var lightingSite = Add(new ExpUpgradeItem()
                .SetKey("LightingSite") // 设置升级项的key
                .SetDescription(_ =>
                    "开启雷电场,攻击身边敌人"
                ) // 设置升级项的描述
                .SetMaxLevel(1)
                .SetOnUpgrade(item => { Player.Instance.Abilities.LightingSite.gameObject.SetActive(true); }));
            // 添加升级项 雷电场攻击力提升
            var lightingSiteAtkUpgrade = Add(new ExpUpgradeItem()
                .SetKey("LightingSiteATKUpgrade") // 设置升级项的key
                .SetDescription(lv => $"雷电场攻击力提升LV{lv}") // 设置升级项的描述
                .SetMaxLevel(10)
                .SetCondition(item => lightingSite.IsUpdated)
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 增加攻击力
                    Player.Instance.Abilities.LightingSite.Attack *= 1.1f;
                }));
            // 添加升级项 雷电场范围
            var lightingSiteRangeUpgrade = Add(new ExpUpgradeItem()
                .SetKey("LightingSiteRangeUpgrade") // 设置升级项的key
                .SetDescription(lv => $"雷电场范围提升LV{lv}") // 设置升级项的描述
                .SetMaxLevel(10)
                .SetCondition(item => lightingSite.IsUpdated)
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 增加范围
                    Player.Instance.Abilities.LightingSite.AttackRange.radius *= 1.1f;
                }));
            // 添加升级项 雷电场间隔
            var lightingSiteFrequencyUpgrade = Add(new ExpUpgradeItem()
                .SetKey("LightingSiteFrequencyUpgrade") // 设置升级项的key
                .SetDescription(lv => $"雷电场频率提升LV{lv}") // 设置升级项的描述
                .SetMaxLevel(10)
                .SetCondition(item => lightingSite.IsUpdated)
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 增加频率
                    Player.Instance.Abilities.LightingSite.AttackRate *= 0.9f;
                }));

            // 添加升级项 火球
            var fireBall = Add(new ExpUpgradeItem()
                .SetKey("FireBall") // 设置升级项的key
                .SetDescription(_ =>
                    "开启随身旋转火球,攻击碰到的敌人"
                ) // 设置升级项的描述
                .SetMaxLevel(1)
                .SetOnUpgrade(item => { Player.Instance.Abilities.FireBall.gameObject.SetActive(true); }));
            // 添加升级项 火球攻击力提升
            var fireBallAtkUpgrade = Add(new ExpUpgradeItem()
                .SetKey("FireBallATKUpgrade") // 设置升级项的key
                .SetDescription(lv => $"火球攻击力提升LV{lv}") // 设置升级项的描述
                .SetMaxLevel(10)
                .SetCondition(item => fireBall.IsUpdated)
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 增加攻击力
                    Player.Instance.Abilities.FireBall.Attack *= 1.1f;
                }));

            // 添加升级项 飞刀攻击力提升
            var knifeAtkUpgrade = Add(new ExpUpgradeItem()
                .SetKey("KnifeATKUpgrade") // 设置升级项的key
                .SetDescription(lv => $"飞刀攻击力提升LV{lv}") // 设置升级项的描述
                .SetMaxLevel(10)
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 增加攻击力
                    Player.Instance.Abilities.KnifeAbility.Attack *= 1.1f;
                }));

            // 添加升级项 飞刀攻击间隔提升
            var knifrangeUpgrade = Add(new ExpUpgradeItem()
                .SetKey("KnifeRangeUpgrade") // 设置升级项的key
                .SetDescription(lv => $"飞刀攻击间隔提升LV{lv}") // 设置升级项的描述
                .SetMaxLevel(10)
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 增加攻击间隔
                    Player.Instance.Abilities.KnifeAbility.AttackRate *= 0.9f;
                }));

            // 添加升级项 拾取范围提升
            var pickUpRangeUpgrade = Add(new ExpUpgradeItem()
                .SetKey("PickUpRangeUpgrade") // 设置升级项的key
                .SetDescription(lv => $"拾取范围提升LV{lv}") // 设置升级项的描述
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
            // 获取可升级的项
            var unUpdatedItems = ExpUpdateItems.Where(item => item.ConditionCheck()).ToList();
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
                item.CurrentLevel.Value = 0;
            }
        }
    }
}