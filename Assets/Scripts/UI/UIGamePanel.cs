using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public class UIGamePanelData : UIPanelData
    {
    }

    public partial class UIGamePanel : UIPanel, IController
    {
        // 屏幕闪烁事件
        public static EasyEvent screenFlashEvent = new();

        public IArchitecture GetArchitecture()
        {
            // 返回全局架构
            return Global.Interface;
        }

        protected override void OnInit(IUIData uiData = null)
        {
            var playerModel = this.GetModel<PlayerModel>();
            var globalModel = this.GetModel<GlobalModel>();
            mData = uiData as UIGamePanelData ?? new UIGamePanelData();
            // 给经验值增加事件添加显示回调函数
            playerModel.Exp.RegisterWithInitValue(newValue =>
                {
                    ExpValue.fillAmount = newValue / playerModel.MaxExp.Value;
                })
                .UnRegisterWhenGameObjectDestroyed(gameObject);
            // 给等级增加事件添加显示回调函数
            playerModel.Level.RegisterWithInitValue(newValue => { TextLevel.text = $"等级:{newValue}"; })
                .UnRegisterWhenGameObjectDestroyed(gameObject);
            // 给金币增加事件添加显示回调函数
            globalModel.Coin.RegisterWithInitValue(newValue => { TextCoin.text = $"金币:{newValue}"; })
                .UnRegisterWhenGameObjectDestroyed(gameObject);

            // 给时间增加事件添加显示回调函数
            globalModel.Time.Register(newValue =>
            {
                // 每30帧执行一次显示时间
                if (Time.frameCount % 30 == 0)
                {
                    // 时间格式转换 从浮点数转换为 00:00
                    // 分钟
                    var minute = (int)newValue / 60;
                    // 秒
                    var second = (int)newValue % 60;
                    // 显示时间
                    TextTime.text = "时间:" + minute.ToString("00") + ":" + second.ToString("00");
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            // 血量显示
            playerModel.Health.RegisterWithInitValue(newValue =>
            {
                // 显示血量
                TextHP.text = $"血量:{newValue}/{playerModel.MaxHealth.Value}";
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            ActionKit.OnUpdate.Register(() =>
            {
                // 时间
                globalModel.Time.Value += Time.deltaTime;
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            // 注册屏幕闪烁事件
            screenFlashEvent.Register(() =>
            {
                // 屏幕闪烁
                ActionKit.Sequence()
                    .Callback(() => ScreenFlash.Show())
                    .Lerp(0, 0.5f, 0.1f, value => { ScreenFlash.ColorAlpha(value); })
                    .Lerp(0.5f, 0, 0.1f, value => { ScreenFlash.ColorAlpha(value); })
                    .Callback(() => ScreenFlash.Hide())
                    .Start(this);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }


        protected override void OnOpen(IUIData uiData = null)
        {
            // 每次打开都隐藏ExpUpgradeRoot
            ExpUpgradeRoot.Hide();
            // 每次打开都隐藏TreasureChestRoot
            TreasureChestRoot.Hide();
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