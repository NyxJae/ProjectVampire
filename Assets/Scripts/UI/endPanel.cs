using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;

namespace QFramework.Example
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
                // 打印日志
                Debug.Log("重新开始");
                // 关闭当前界面
                UIKit.ClosePanel<endPanel>();
                // 重新加载场景
                SceneManager.LoadScene("SampleScene");

            }).disposeWhenGameObjectDestroyed(gameObject);

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
