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
            Global.Exp.RegisterWithInitValue(newValue =>
            {
                TextExp.text = "经验值:" + newValue.ToString();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            // 给等级增加事件添加显示回调函数
            Global.Level.Register(newValue =>
            {
                TextLevel.text = "等级:" + newValue.ToString();
                // 显示升级按钮
                BtnUpdate.Show();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // 给时间增加事件添加显示回调函数
            Global.Time.Register(newValue =>
            {
                // 每30帧执行一次显示时间
                if (Time.frameCount % 30 == 0)
                {
                    // 时间格式转换 从浮点数转换为 00:00
                    // 分钟
                    int minute = (int)newValue / 60;
                    // 秒
                    int second = (int)newValue % 60;
                    // 显示时间
                    TextTime.text = "时间:" + minute.ToString("00") + ":" + second.ToString("00");
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            // 血量显示
            Player.Instance.Health.RegisterWithInitValue(newValue =>
            {
                // 显示血量
                TextHP.text = "血量:" + newValue.ToString();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);



            // 给升级按钮增加点击事件
            BtnUpdate.onClick.AddListener(() =>
            {
                // 取得玩家实例.以获取技能脚本
                var sampleAbility = Player.Instance.Abilities.SampleAbility;
                // sampleAbility 提升攻击力
                sampleAbility.Attack += 1;
                // 隐藏升级按钮
                BtnUpdate.Hide();
                // 时间恢复
                Time.timeScale = 1;
            });

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
