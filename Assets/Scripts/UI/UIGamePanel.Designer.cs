using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectVampire
{
	// Generate Id:83a78f20-2c7d-4814-91bb-ee9d616e7f33
	public partial class UIGamePanel
	{
		public const string Name = "UIGamePanel";
		
		[SerializeField]
		public UnityEngine.UI.Text TextCoin;
		[SerializeField]
		public UnityEngine.UI.Text TextHP;
		[SerializeField]
		public UnityEngine.UI.Text TextLevel;
		[SerializeField]
		public UnityEngine.UI.Text TextTime;
		[SerializeField]
		public ExpUpgradeRoot ExpUpgradeRoot;
		[SerializeField]
		public UnityEngine.UI.Image ExpValue;
		
		private UIGamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			TextCoin = null;
			TextHP = null;
			TextLevel = null;
			TextTime = null;
			ExpUpgradeRoot = null;
			ExpValue = null;
			
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
