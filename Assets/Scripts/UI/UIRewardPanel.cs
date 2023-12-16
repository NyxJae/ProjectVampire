using ProjectVampire.System;
using QFramework;
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

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIRewardPanelData ?? new UIRewardPanelData();
            this.GetSystem<CoinUpgradeSystem>();
        }

        protected override void OnOpen(IUIData uiData = null)
        {
            // 显示金币数量
            TextCoin.text = $"金币:{Global.Coin.Value}";
            // 给金币增加事件添加显示回调函数
            Global.Coin.Register(newValue => { TextCoin.text = $"金币:{newValue}"; })
                .UnRegisterWhenGameObjectDestroyed(gameObject);
            // 注册 BtnClose 的点击事件
            BtnClose.onClick.AddListener(() =>
            {
                // 关闭当前界面
                UIKit.ClosePanel<UIRewardPanel>();
                // 打开 Begin 界面
                UIKit.OpenPanel<UIBeginPanel>();
            });
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