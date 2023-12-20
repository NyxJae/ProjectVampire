using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectVampire
{
	// Generate Id:8d737291-bad3-444c-bf49-0a23030519a8
	public partial class UIGamePanel
	{
		public const string Name = "UIGamePanel";
		
		[SerializeField]
		public UnityEngine.UI.Text TextCoin;
		[SerializeField]
		public UnityEngine.UI.Text TextExp;
		[SerializeField]
		public UnityEngine.UI.Text TextHP;
		[SerializeField]
		public UnityEngine.UI.Text TextLevel;
		[SerializeField]
		public UnityEngine.UI.Text TextTime;
		[SerializeField]
		public ExpUpgradeRoot ExpUpgradeRoot;
		
		private UIGamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			TextCoin = null;
			TextExp = null;
			TextHP = null;
			TextLevel = null;
			TextTime = null;
			ExpUpgradeRoot = null;
			
			mData = null;
		}
		
		public UIGamePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGamePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGamePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
