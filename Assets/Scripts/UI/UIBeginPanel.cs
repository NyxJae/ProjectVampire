using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;

namespace ProjectVampire
{
	public class UIBeginPanelData : UIPanelData
	{
	}
	public partial class UIBeginPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIBeginPanelData ?? new UIBeginPanelData();
			// please add init code here
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
			// 注册 BtnBegin 的点击事件 
			BtnBegin.onClick.AddListener(() =>
			{
				// 关闭 Begin 界面
				UIKit.ClosePanel<UIBeginPanel>();
				// 打开游戏场景
				SceneManager.LoadScene(Global.GameScene);
				
			});
			// 注册 BtnReward 的点击事件
			BtnReward.onClick.AddListener(() =>
			{
				// 关闭 Begin 界面
				UIKit.ClosePanel<UIBeginPanel>();
				// 打开奖励界面
				UIKit.OpenPanel<UIRewardPanel>();
			});
		}
		
		protected override void OnShow()
		{
			// 
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}
	}
}
