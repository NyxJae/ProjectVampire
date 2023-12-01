using UnityEngine;
using UnityEngine.UI;
using QFramework;

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
				UIKit.ClosePanel<UIWinPanel>();
				// 重置各项属性
				Global.ResetProperties();
				// 重新加载场景
				UnityEngine.SceneManagement.SceneManager.LoadScene(Global.GameScene);
			});
			// 注册 BtnBack 的点击事件
			BtnBack.onClick.AddListener(() =>
			{
				// 关闭当前界面
				UIKit.ClosePanel<UIWinPanel>();
				// 打开 Begin 场景
				 UnityEngine.SceneManagement.SceneManager.LoadScene(Global.BeginScene);
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
