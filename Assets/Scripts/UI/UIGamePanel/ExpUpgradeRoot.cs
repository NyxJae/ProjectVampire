/****************************************************************************
 * 2023.12 DESKTOP-AETQQ8U
 ****************************************************************************/

using System.Linq;
using QFramework;
using TMPro;
using UnityEngine;

namespace ProjectVampire
{
    public partial class ExpUpgradeRoot : UIElement, IController
    {
        private void Awake()
        {
            Hide();
            BtnExpUpgradePrefab.Hide();
            Global.Exp.Register(newValue =>
            {
                // 时间暂停
                Time.timeScale = 0;
                // 显示升级面板
                Show();
                foreach (var expGroupItem in this.GetSystem<ExpUpgradeSystem>().ExpUpdateItems
                             .Where(item => item.ConditionCheck()))
                    // 实例化升级项
                    BtnExpUpgradePrefab.InstantiateWithParent(GroupExpUpgradeBtns)
                        .Self(self =>
                        {
                            // 缓存升级项
                            var itemCatch = expGroupItem;
                            // 设置按钮的文本(textMeshPro)
                            self.GetComponentInChildren<TextMeshProUGUI>().text = itemCatch.Description;
                            // 给按钮添加点击事件
                            self.onClick.AddListener(() =>
                            {
                                itemCatch.Upgrade();
                                AudioKit.PlaySound("AbilityLevelUp");
                                Hide();
                                // 时间恢复
                                Time.timeScale = 1;
                            });
                        }).Show();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }

        protected override void OnBeforeDestroy()
        {
        }
    }
}