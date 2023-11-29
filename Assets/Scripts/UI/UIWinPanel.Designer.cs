using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectVampire
{
	// Generate Id:ac0c4a15-c8ea-4c83-93d1-bb30d7be0176
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
