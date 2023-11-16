using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;

namespace ProjectVampire
{
    public class endPanelData : UIPanelData
    {
    }
    public partial class endPanel : UIPanel
    {
        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as endPanelData ?? new endPanelData();
            // please add init code here
            BtnReplay.onClick.AddListener(() =>
            {
                // 关闭当前界面
                UIKit.ClosePanel<endPanel>();
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
