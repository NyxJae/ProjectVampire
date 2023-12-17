using System.Linq;
using ProjectVampire.System;
using QFramework;
using TMPro;
using UnityEngine;

namespace ProjectVampire
{
    public class UIRewardPanelData : UIPanelData
    {
    }

    public partial class UIRewardPanel : UIPanel, IController
    {
        // 私有的 升级所需金币数量
        [SerializeField] private int mCoinUp = 5;

        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }

        // 按钮更新方法
        private void ReFrashBtn()
        {
            // 清除所有的激活状态的升级按钮
            GroupBtns.DestroyChildrenWithCondition(btn => btn.gameObject.activeSelf);
            foreach (var coinUpdateItem in this.GetSystem<CoinUpgradeSystem>().CoinUpdateItems
                         .Where(item => item.ConditionCheck()))
                // 实例化升级项
                BtnUpgradePrefab.InstantiateWithParent(GroupBtns)
                    .Self(self =>
                    {
                        // 缓存升级项
                        var itemCatch = coinUpdateItem;
                        // 设置按钮的文本(textMeshPro)
                        self.GetComponentInChildren<TextMeshProUGUI>().text = itemCatch.Description;
                        // 给按钮添加点击事件
                        self.onClick.AddListener(() =>
                        {
                            itemCatch.Upgrade();
                            AudioKit.PlaySound("AbilityLevelUp");
                        });
                        // 缓存self
                        var selfCatch = self;
                        // 给按钮添加金币数量变化事件, 用于判断是否可以升级
                        Global.Coin.RegisterWithInitValue(coin =>
                        {
                            if (coin < itemCatch.Price)
                                selfCatch.interactable = false;
                            else
                                selfCatch.interactable = true;
                        }).UnRegisterWhenGameObjectDestroyed(selfCatch);
                    })
                    .Show();
        }

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIRewardPanelData ?? new UIRewardPanelData();
            // 给金币增加事件添加显示回调函数
            Global.Coin.RegisterWithInitValue(newValue => { TextCoin.text = $"金币:{newValue}"; })
                .UnRegisterWhenGameObjectDestroyed(gameObject);
            // 注册 BtnClose 的点击事件
            BtnClose.onClick.AddListener(() =>
            {
                // 关闭当前界面
                UIKit.ClosePanel<UIRewardPanel>();
            });
            // 注册 CoinUpgradeSystem 的变化事件
            CoinUpgradeSystem.OnCoinUpgradeSystemChanged.Register(() => { ReFrashBtn(); })
                .UnRegisterWhenGameObjectDestroyed(gameObject);
            // 更新按钮
            ReFrashBtn();
        }

        protected override void OnOpen(IUIData uiData = null)
        {
            // 隐藏升级按钮
            BtnUpgradePrefab.Hide();
        }

        protected override void OnShow()
        {
        }

        protected override void OnHide()
        {
        }

        protected override void OnClose()
        {
        }
    }
}