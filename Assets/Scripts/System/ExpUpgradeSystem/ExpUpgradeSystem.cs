using System.Collections.Generic;
using ProjectVampire.System.ExpUpgradeSystem;
using QFramework;

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
            // 添加升级项 基础能力攻击力LV1
            var attackUpgradeLv1 = Add(new ExpUpgradeItem()
                .SetKey("SampleAbilityATKLV1") // 设置升级项的key
                .SetDescription("基础能力攻击力LV1") // 设置升级项的描述
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 增加攻击力
                    Player.Instance.Abilities.SampleAbility.Attack += 1;
                }));
            // 添加升级项 基础能力攻击力LV2
            var attackUpgradeLv2 = Add(new ExpUpgradeItem()
                .SetKey("SampleAbilityATKLV2") // 设置升级项的key
                .SetDescription("基础能力攻击力LV2") // 设置升级项的描述
                .SetCondition(item => // 设置升级项的条件
                    // 判断是否显示
                    attackUpgradeLv1.IsUpdated)
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 增加攻击力
                    Player.Instance.Abilities.SampleAbility.Attack += 1;
                }));
            // 添加升级项 基础能力攻击力LV3
            var attackUpgradeLv3 = Add(new ExpUpgradeItem()
                .SetKey("SampleAbilityATKLV3") // 设置升级项的key
                .SetDescription("基础能力攻击力LV3") // 设置升级项的描述
                .SetCondition(item => // 设置升级项的条件
                    // 判断是否显示
                    attackUpgradeLv2.IsUpdated)
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 增加攻击力
                    Player.Instance.Abilities.SampleAbility.Attack += 1;
                }));
            // 添加升级项 基础能力攻击范围LV1
            var attackRangeUpgradeLv1 = Add(new ExpUpgradeItem()
                .SetKey("SampleAbilityATKRangeLV1") // 设置升级项的key
                .SetDescription("基础能力攻击范围LV1") // 设置升级项的描述
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 增加攻击范围
                    Player.Instance.Abilities.SampleAbility.AttackDistance *= 1.1f;
                }));
            // 添加升级项 基础能力攻击范围LV2
            var attackRangeUpgradeLv2 = Add(new ExpUpgradeItem()
                .SetKey("SampleAbilityATKRangeLV2") // 设置升级项的key
                .SetDescription("基础能力攻击范围LV2") // 设置升级项的描述
                .SetCondition(item => // 设置升级项的条件
                    // 判断是否显示
                    attackRangeUpgradeLv1.IsUpdated)
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 增加攻击范围
                    Player.Instance.Abilities.SampleAbility.AttackDistance *= 1.1f;
                }));
            // 添加升级项 基础能力攻击范围LV3
            var attackRangeUpgradeLv3 = Add(new ExpUpgradeItem()
                .SetKey("SampleAbilityATKRangeLV3") // 设置升级项的key
                .SetDescription("基础能力攻击范围LV3") // 设置升级项的描述
                .SetCondition(item => // 设置升级项的条件
                    // 判断是否显示
                    attackRangeUpgradeLv2.IsUpdated)
                .SetOnUpgrade(item => // 设置升级项的升级方法
                {
                    // 增加攻击范围
                    Player.Instance.Abilities.SampleAbility.AttackDistance *= 1.1f;
                }));
        }
    }
}