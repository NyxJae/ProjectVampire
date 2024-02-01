using System.Linq;
using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public class UIRewardPanelData : UIPanelData
    {
    }

    public partial class UIRewardPanel : UIPanel, IController
    {
        // resloader 用于加载资源
        private readonly ResLoader _resLoader = ResLoader.Allocate();

        // saveutility
        private SaveUtility _saveUtility => this.GetUtility<SaveUtility>();

        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIRewardPanelData ?? new UIRewardPanelData();
            // 隐藏升级按钮
            BtnUpgradePrefab.Hide();
            // 给金币增加事件添加显示回调函数
            this.GetModel<GlobalModel>().Coin.RegisterWithInitValue(newValue => { TextCoin.text = $"金币:{newValue}"; })
                .UnRegisterWhenGameObjectDestroyed(gameObject);
            // 注册 BtnClose 的点击事件
            BtnClose.onClick.AddListener(() =>
            {
                // 关闭当前界面
                UIKit.ClosePanel<UIRewardPanel>();
            });
            foreach (var coinUpdateItem in this.GetSystem<CoinUpgradeSystem>().CoinUpdateItems
                         .Where(item => item.IsUpdated == false))
                // 实例化升级项
                BtnUpgradePrefab.InstantiateWithParent(GroupBtns)
                    .Self(self =>
                    {
                        // 缓存升级项
                        var itemCatch = coinUpdateItem;
                        // 设置按钮的文本(textMeshPro)
                        self.Text.text = itemCatch.Description;
                        self.Icon.sprite = _resLoader.LoadSync<Sprite>(itemCatch.IconName);
                        // 给按钮添加点击事件
                        self.Button.onClick.AddListener(() =>
                        {
                            var command = new OptionTriggerCommand(itemCatch, _saveUtility);
                            this.SendCommand(command);
                        });
                        // 缓存self
                        var selfCatch = self;
                        // 给按钮添加金币数量变化事件, 用于判断是否可以升级
                        this.GetModel<GlobalModel>().Coin.RegisterWithInitValue(coin =>
                        {
                            if (coin < itemCatch.Price)
                                selfCatch.Button.interactable = false;
                            else
                                selfCatch.Button.interactable = true;
                        }).UnRegisterWhenGameObjectDestroyed(selfCatch);
                        // 注册更改事件
                        itemCatch.OnCoinUpdateItemChanged.Register(() =>
                        {
                            // 如果可以显示
                            if (itemCatch.ConditionCheck())
                                // 显示按钮
                                selfCatch.Show();
                            else
                                // 隐藏按钮
                                selfCatch.Hide();
                        }).UnRegisterWhenGameObjectDestroyed(selfCatch);
                        // 如果可以显示
                        if (itemCatch.ConditionCheck())
                            // 显示按钮
                            selfCatch.Show();
                        else
                            // 隐藏按钮
                            selfCatch.Hide();
                    });
        }

        protected override void OnOpen(IUIData uiData = null)
        {
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