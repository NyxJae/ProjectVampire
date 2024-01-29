using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectVampire
{
	// Generate Id:207b61ff-c7b0-470d-b6f0-535f33331f1c
	public partial class UIRewardPanel
	{
		public const string Name = "UIRewardPanel";
		
		[SerializeField]
		public RectTransform GroupBtns;
		[SerializeField]
		public BtnUpgradePrefab BtnUpgradePrefab;
		[SerializeField]
		public UnityEngine.UI.Button BtnClose;
		[SerializeField]
		public TMPro.TextMeshProUGUI TextCoin;
		
		private UIRewardPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			GroupBtns = null;
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
