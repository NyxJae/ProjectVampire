using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectVampire
{
	// Generate Id:1d83804e-76ba-48a1-8cd5-6d5c33405725
	public partial class UIRewardPanel
	{
		public const string Name = "UIRewardPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button BtnUpgradePrefab;
		[SerializeField]
		public UnityEngine.UI.Button BtnClose;
		[SerializeField]
		public TMPro.TextMeshProUGUI TextCoin;
		
		private UIRewardPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			BtnUpgradePrefab = null;
			BtnClose = null;
			TextCoin = null;
			
			mData = null;
		}
		
		public UIRewardPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIRewardPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIRewardPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
