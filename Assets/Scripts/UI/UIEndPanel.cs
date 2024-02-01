using QFramework;
using UnityEngine.SceneManagement;

namespace ProjectVampire
{
    public class UIEndPanelData : UIPanelData
    {
    }

    public partial class UIEndPanel : UIPanel, IController

    {
        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIEndPanelData ?? new UIEndPanelData();
            // please add init code here
            BtnReplay.onClick.AddListener(() =>
            {
                // 关闭当前界面
                UIKit.ClosePanel<UIEndPanel>();
                // 发送开始游戏命令
                this.SendCommand<BeginGameCommand>();
            });
            BtnBack.onClick.AddListener(() =>
            {
                // 关闭当前界面
                UIKit.ClosePanel<UIEndPanel>();
                // 打开 Begin 场景
                SceneManager.LoadScene(Global.BeginScene);
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