/****************************************************************************
 * 2023.12 DESKTOP-AETQQ8U
 ****************************************************************************/

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
            foreach (var expGroupItem in this.GetSystem<ExpUpgradeSystem>().ExpUpdateItems)
                // 实例化升级项
                BtnExpUpgradePrefab.InstantiateWithParent(GroupExpUpgradeBtns)
                    .Self(self =>
                    {
                        // 缓存升级项
                        var itemCatch = expGroupItem;
                        // 给按钮添加点击事件
                        self.onClick.AddListener(() =>
                        {
                            itemCatch.Upgrade();
                            AudioKit.PlaySound("AbilityLevelUp");
                            Hide();
                            // 时间恢复
                            Time.timeScale = 1;
                        });
                        // 缓存self
                        var selfCatch = self;
                        // 设置可见与描述
                        itemCatch.IsVisible.Register(newValue =>
                        {
                            // 更新升级项描述(textMeshPro)
                            selfCatch.GetComponentInChildren<TextMeshProUGUI>().text =
                                $"{itemCatch.Description}LV{itemCatch.CurrentLevel.Value}";
                            // 设置按钮是否可见
                            selfCatch.gameObject.SetActive(newValue);
                        }).UnRegisterWhenGameObjectDestroyed(selfCatch);
                    });
            Global.Level.Register(newValue =>
            {
                // 时间暂停
                Time.timeScale = 0;
                // 显示升级面板
                Show();
                // 随机n个未升级的升级项设置为可见
                this.GetSystem<ExpUpgradeSystem>().RandomUnUpdatedItem(4);
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