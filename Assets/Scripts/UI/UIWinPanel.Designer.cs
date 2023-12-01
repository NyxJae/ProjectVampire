using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectVampire
{
	// Generate Id:a19c5a76-e241-458b-ac02-ecc272116b43
	public partial class UIWinPanel
	{
		public const string Name = "UIWinPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button BtnReplay;
		[SerializeField]
		public UnityEngine.UI.Button BtnBack;
		
		private UIWinPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			BtnReplay = null;
			BtnBack = null;
			
			mData = null;
		}
		
		public UIWinPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIWinPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIWinPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
