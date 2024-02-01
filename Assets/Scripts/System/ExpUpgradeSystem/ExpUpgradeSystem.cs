using System.Collections.Generic;
using System.Linq;
using QAssetBundle;
using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public class ExpUpgradeSystem : AbstractSystem
    {
        // 升级项列表
        public List<ExpUpgradeItem> ExpUpdateItems { get; } = new();

        // PlayerModel
        private PlayerModel _PlayerModel => this.GetModel<PlayerModel>();

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
                .SetIconName(Icons.LIGHTNINGFIELDICON)
                .SetMaxLevel(1)
                .SetOnUpgrade(item => { Player.Instance.Abilities.LightingSite.gameObject.SetActive(true); }));
            // 添加升级项 雷电场攻击力提升
            var lightingSiteAtkUpgrade = Add(new ExpUpgradeItem()
                .SetKey("LightingSiteATKUpgrade") // 设置升级项的key
                .SetDescription(lv => $"雷电场攻击力提升LV{lv}") // 设置升级项的描述
                .SetMaxLevel(10)
                .SetIconName(Icons.LIGHTNINGFIELDICON)
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
                .SetIconName(Icons.LIGHTNINGFIELDICON)
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
                .SetIconName(Icons.LIGHTNINGFIELDICON)
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
                .SetIconName(Icons.FIREBALLICON)
                .SetMaxLevel(1)
                .SetOnUpgrade(item => { Player.Instance.Abilities.FireBallAbility.gameObject.SetActive(true); }));
            // 添加升级项 火球攻击力提升
            var fireBallAtkUpgrade = Add(new ExpUpgradeItem()
                .SetKey("FireBallATKUpgrade") // 设置升级项的key
                .SetDescription(lv => $"火球攻击力提升LV{lv}") // 设置升级项的描述
                .SetMaxLevel(10)
                .SetIconName(Icons.FIREBALLICON)
                .SetCondition(item => fireBall.IsUpdated)
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 增加攻击力
                    Player.Instance.Abilities.FireBallAbility.Attack *= 1.1f;
                }));
            // 火球数量提升
            var fireBallCountUpgrade = Add(new ExpUpgradeItem()
                .SetKey("FireBallCountUpgrade") // 设置升级项的key
                .SetDescription(lv => $"火球数量提升LV{lv}") // 设置升级项的描述
                .SetMaxLevel(8)
                .SetIconName(Icons.FIREBALLICON)
                .SetCondition(item => fireBall.IsUpdated)
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 增加火球数量
                    Player.Instance.Abilities.FireBallAbility.FireBallCount.Value += 1;
                }));

            // 添加升级项 飞刀攻击力提升
            var knifeAtkUpgrade = Add(new ExpUpgradeItem()
                .SetKey("KnifeATKUpgrade") // 设置升级项的key
                .SetDescription(lv => $"飞刀攻击力提升LV{lv}") // 设置升级项的描述
                .SetMaxLevel(10)
                .SetIconName(Icons.KNIFEICON)
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
                .SetIconName(Icons.KNIFEICON)
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 增加攻击间隔
                    Player.Instance.Abilities.KnifeAbility.AttackRate *= 0.9f;
                }));
            // 添加升级项 飞刀数量提升
            var knifeCountUpgrade = Add(new ExpUpgradeItem()
                .SetKey("KnifeCountUpgrade") // 设置升级项的key
                .SetDescription(lv => $"飞刀数量提升LV{lv}") // 设置升级项的描述
                .SetMaxLevel(10)
                .SetIconName(Icons.KNIFEICON)
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 增加飞刀数量
                    Player.Instance.Abilities.KnifeAbility.KnifeCount += 1;
                }));
            // 添加升级项 飞刀穿透敌人数量提升
            var knifePierceCountUpgrade = Add(new ExpUpgradeItem()
                .SetKey("KnifePierceCountUpgrade") // 设置升级项的key
                .SetDescription(lv => $"飞刀穿透敌人数量提升LV{lv}") // 设置升级项的描述
                .SetMaxLevel(5)
                .SetIconName(Icons.KNIFEICON)
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 增加飞刀穿透敌人数量
                    Player.Instance.Abilities.KnifeAbility.KnifePierceCount += 1;
                }));
            // 添加升级项 黑洞能力
            var BlackHoleAbility = Add(new ExpUpgradeItem()
                .SetKey("BlackHoleAbility") // 设置升级项的key
                .SetDescription(_ =>
                    "开启黑洞能力,吸引敌人,并攻击"
                ) // 设置升级项的描述
                .SetMaxLevel(1)
                .SetIconName(Icons.BLACKHOLEICON)
                .SetOnUpgrade(item => { Player.Instance.Abilities.BlackHoleAbility.gameObject.SetActive(true); }));
            // 添加升级项 黑洞攻击力提升
            Add(new ExpUpgradeItem()
                .SetKey("BlackHoleATKUpgrade") // 设置升级项的key
                .SetDescription(lv => $"黑洞攻击力提升LV{lv}") // 设置升级项的描述
                .SetMaxLevel(10)
                .SetIconName(Icons.BLACKHOLEICON)
                .SetCondition(item => BlackHoleAbility.IsUpdated)
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 增加攻击力
                    Player.Instance.Abilities.BlackHoleAbility.Attack *= 1.1f;
                }));
            // 添加升级项 黑洞大小提升
            Add(new ExpUpgradeItem()
                .SetKey("BlackHoleSizeUpgrade") // 设置升级项的key
                .SetDescription(lv => $"黑洞大小提升LV{lv}") // 设置升级项的描述
                .SetMaxLevel(10)
                .SetIconName(Icons.BLACKHOLEICON)
                .SetCondition(item => BlackHoleAbility.IsUpdated)
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 增加大小
                    Player.Instance.Abilities.BlackHoleAbility.Size *= 1.1f;
                }));
            // 添加升级项 黑洞持续时间提升
            Add(new ExpUpgradeItem()
                .SetKey("BlackHoleDurationUpgrade") // 设置升级项的key
                .SetDescription(lv => $"黑洞持续时间提升LV{lv}") // 设置升级项的描述
                .SetMaxLevel(10)
                .SetIconName(Icons.BLACKHOLEICON)
                .SetCondition(item => BlackHoleAbility.IsUpdated)
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 增加持续时间
                    Player.Instance.Abilities.BlackHoleAbility.Duration *= 1.1f;
                }));
            // 添加升级项 黑洞冷却时间减少
            Add(new ExpUpgradeItem()
                .SetKey("BlackHoleCooldownUpgrade") // 设置升级项的key
                .SetDescription(lv => $"黑洞冷却时间减少LV{lv}") // 设置升级项的描述
                .SetMaxLevel(10)
                .SetIconName(Icons.BLACKHOLEICON)
                .SetCondition(item => BlackHoleAbility.IsUpdated)
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 减少冷却时间
                    Player.Instance.Abilities.BlackHoleAbility.Cooldown *= 0.9f;
                }));
            // 添加升级项 黑洞移动速度提升
            Add(new ExpUpgradeItem()
                .SetKey("BlackHoleMoveSpeedUpgrade") // 设置升级项的key
                .SetDescription(lv => $"黑洞移动速度提升LV{lv}") // 设置升级项的描述
                .SetMaxLevel(10)
                .SetIconName(Icons.BLACKHOLEICON)
                .SetCondition(item => BlackHoleAbility.IsUpdated)
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 增加移动速度
                    Player.Instance.Abilities.BlackHoleAbility.MoveSpeed *= 1.1f;
                }));
            // 添加升级项 黑洞数量提升
            Add(new ExpUpgradeItem()
                .SetKey("BlackHoleCountUpgrade") // 设置升级项的key
                .SetDescription(lv => $"黑洞数量提升LV{lv}") // 设置升级项的描述
                .SetMaxLevel(5)
                .SetIconName(Icons.BLACKHOLEICON)
                .SetCondition(item => BlackHoleAbility.IsUpdated)
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 增加黑洞数量
                    Player.Instance.Abilities.BlackHoleAbility.Count += 1;
                }));


            // 添加升级项 拾取范围提升
            var pickUpRangeUpgrade = Add(new ExpUpgradeItem()
                .SetKey("PickUpRangeUpgrade") // 设置升级项的key
                .SetDescription(lv => $"拾取范围提升LV{lv}") // 设置升级项的描述
                .SetMaxLevel(10)
                .SetIconName(Icons.PICKRANGEICON)
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 增加拾取范围
                    Player.Instance.Abilities.PickAbility.PickRange.radius *= 1.1f;
                }));
            // 添加升级项 暴击率提升
            var criticalRateUpgrade = Add(new ExpUpgradeItem()
                .SetKey("CriticalRateUpgrade") // 设置升级项的key
                .SetDescription(lv => $"暴击率提升LV{lv}") // 设置升级项的描述
                .SetMaxLevel(10) // 最大等级设置为5
                .SetIconName(Icons.STRIKERATEICON)
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 提升暴击率
                    _PlayerModel.CriticalRate.Value += 0.09f; //
                }));

            // 添加升级项 暴击伤害提升
            var criticalDamageUpgrade = Add(new ExpUpgradeItem()
                .SetKey("CriticalDamageUpgrade") // 设置升级项的key
                .SetDescription(lv => $"暴击伤害提升LV{lv}") // 设置升级项的描述
                .SetMaxLevel(10) // 最大等级设置为5
                .SetIconName(Icons.STRIKERATEICON)
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 提升暴击伤害倍数
                    _PlayerModel.CriticalMultiplier.Value += 0.8f; //
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

        // 随机提供1个未升级的升级项
        /// <summary>
        ///     随机提供1个未升级的升级项
        /// </summary>
        /// <returns> 未升级的升级项 </returns>
        public ExpUpgradeItem GetOneRandomUnUpdatedItem()
        {
            // 获取可升级的项
            var unUpdatedItems = ExpUpdateItems.Where(item => item.ConditionCheck()).ToList();
            // 随机获取1个未升级的项
            var randomItem = unUpdatedItems.OrderBy(item => Random.Range(0, 100)).Take(1).ToList();
            // 返回未升级的项
            return randomItem[0];
        }

        // 重置所有升级项
        public void ResetAll()
        {
            foreach (var item in ExpUpdateItems) item.Reset();
        }
    }
}