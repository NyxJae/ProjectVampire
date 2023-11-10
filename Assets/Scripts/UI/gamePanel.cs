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
            // 给经验值增加事件添加回调函数
            Global.Exp.RegisterWithInitValue(newVlaue =>
            {
                TextExp.text = "经验值:" + newVlaue.ToString();
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
