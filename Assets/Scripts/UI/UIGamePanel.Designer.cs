using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectVampire
{
	// Generate Id:8754e4df-647a-4dd6-836f-cd04cc86cb44
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
		[SerializeField]
		public UnityEngine.UI.Image ScreenFlash;
		[SerializeField]
		public TreasureChestRoot TreasureChestRoot;
		
		private UIGamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			TextCoin = null;
			TextHP = null;
			TextLevel = null;
			TextTime = null;
			ExpUpgradeRoot = null;
			ExpValue = null;
			ScreenFlash = null;
			TreasureChestRoot = null;
			
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
