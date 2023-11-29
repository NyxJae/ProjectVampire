using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;

namespace ProjectVampire
{
    public class UIWinPanelData : UIPanelData
    {
    }
    public partial class UIWinPanel : UIPanel
    {
        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIWinPanelData ?? new UIWinPanelData();
            // 注册 BtnReplay 的点击事件
            BtnReplay.onClick.AddListener(() =>
            {
                // 关闭当前界面
                UIKit.ClosePanel<UIEndPanel>();
                // 重置各项属性
                Global.ResetProperties();
                // 重新加载场景
                SceneManager.LoadScene("GameScene");

            });
            // 注册 BtnBack 的点击事件 
            BtnReplay.onClick.AddListener(() =>
            {
                // 打开场景
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
