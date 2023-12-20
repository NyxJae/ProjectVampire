using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public class UIGamePanelData : UIPanelData
    {
    }

    public partial class UIGamePanel : UIPanel, IController
    {
        public IArchitecture GetArchitecture()
        {
            // 返回全局架构
            return Global.Interface;
        }

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIGamePanelData ?? new UIGamePanelData();
            // 给经验值增加事件添加显示回调函数
            Global.Exp.RegisterWithInitValue(newValue =>
            {
                TextExp.text = $"经验值:{newValue}/{Player.Instance.ExpToNextLevel()}";
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            // 显示等级
            TextLevel.text = $"等级:{Global.Level.Value}";
            // 给等级增加事件添加显示回调函数
            Global.Level.Register(newValue => { TextLevel.text = $"等级:{newValue}"; })
                .UnRegisterWhenGameObjectDestroyed(gameObject);
            // 显示金币数量
            TextCoin.text = $"金币:{Global.Coin.Value}";
            // 给金币增加事件添加显示回调函数
            Global.Coin.Register(newValue => { TextCoin.text = $"金币:{newValue}"; })
                .UnRegisterWhenGameObjectDestroyed(gameObject);

            // 给时间增加事件添加显示回调函数
            Global.Time.Register(newValue =>
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
            Global.Health.RegisterWithInitValue(newValue =>
            {
                // 显示血量
                TextHP.text = $"血量:{newValue}/{Global.MaxHealth.Value}";
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            ActionKit.OnUpdate.Register(() =>
            {
                // 时间
                Global.Time.Value += Time.deltaTime;
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
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