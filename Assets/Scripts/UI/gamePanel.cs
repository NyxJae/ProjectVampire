using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectVampire
{
    public class gamePanelData : UIPanelData
    {
    }
    public partial class gamePanel : UIPanel
    {
        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as gamePanelData ?? new gamePanelData();
            // 给经验值增加事件添加显示回调函数
            Global.Exp.RegisterWithInitValue(newVlaue =>
            {
                TextExp.text = "经验值:" + newVlaue.ToString();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            // 给等级增加事件添加显示回调函数
            Global.Level.Register(newVlaue =>
            {
                TextLevel.text = "等级:" + newVlaue.ToString();
                // 显示升级按钮
                BtnUpdate.Show();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // 给时间增加事件添加显示回调函数
            Global.Time.Register(newVlaue =>
            {
                // 时间格式转换 从浮点数转换为 00:00
                // 分钟
                int minute = (int)newVlaue / 60;
                // 秒
                int second = (int)newVlaue % 60;
                // 显示时间
                TextTime.text = "时间:" + minute.ToString("00") + ":" + second.ToString("00");

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // 给升级按钮增加点击事件
            BtnUpdate.onClick.AddListener(() =>
            {
                // 隐藏升级按钮
                BtnUpdate.Hide();
                // 时间恢复
                Time.timeScale = 1;
            });

            ActionKit.OnUpdate.Register(() =>
            {
                // 显示血量
                TextHP.text = "血量:" + Player.Instance.Health.ToString();
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
