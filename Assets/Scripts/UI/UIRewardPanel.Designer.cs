using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectVampire
{
	// Generate Id:8989b624-1c0c-4ea2-9423-b52c9c13b157
	public partial class UIRewardPanel
	{
		public const string Name = "UIRewardPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button BtnCoinUp;
		[SerializeField]
		public UnityEngine.UI.Button BtnExpUp;
		[SerializeField]
		public UnityEngine.UI.Button BtnMaxHPUp;
		[SerializeField]
		public UnityEngine.UI.Button BtnClose;
		[SerializeField]
		public TMPro.TextMeshProUGUI TextCoin;
		
		private UIRewardPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			BtnCoinUp = null;
			BtnExpUp = null;
			BtnMaxHPUp = null;
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
