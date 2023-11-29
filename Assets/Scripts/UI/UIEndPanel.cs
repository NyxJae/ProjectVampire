using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;

namespace ProjectVampire
{
    public class UIEndPanelData : UIPanelData
    {
    }
    public partial class UIEndPanel : UIPanel
    {
        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIEndPanelData ?? new UIEndPanelData();
            // please add init code here
            BtnReplay.onClick.AddListener(() =>
            {
                // 关闭当前界面
                UIKit.ClosePanel<UIEndPanel>();
                // 重置各项属性
                Global.ResetProperties();
                // 重新加载场景
                SceneManager.LoadScene("GameScene");

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
