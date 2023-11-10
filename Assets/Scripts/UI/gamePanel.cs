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
			// please add init code here
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
