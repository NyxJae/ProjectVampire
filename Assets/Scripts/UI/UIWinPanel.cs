using QFramework;
using UnityEngine.SceneManagement;

namespace ProjectVampire
{
    public class UIWinPanelData : UIPanelData
    {
    }

    public partial class UIWinPanel : UIPanel, IController
    {
        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIWinPanelData ?? new UIWinPanelData();
            // 注册 BtnReplay 的点击事件
            BtnReplay.onClick.AddListener(() =>
            {
                // 关闭当前界面
                UIKit.ClosePanel<UIWinPanel>();
                // 发送重置模型数据的命令
                this.SendCommand(new ResetModelCommand());
                // 重新加载场景
                SceneManager.LoadScene(Global.GameScene);
            });
            // 注册 BtnBack 的点击事件
            BtnBack.onClick.AddListener(() =>
            {
                // 关闭当前界面
                UIKit.ClosePanel<UIWinPanel>();
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