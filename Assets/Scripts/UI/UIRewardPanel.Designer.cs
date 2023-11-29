using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectVampire
{
	// Generate Id:f3742f46-6447-48e0-a00f-07bc70eab846
	public partial class UIRewardPanel
	{
		public const string Name = "UIRewardPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button BtnCoinUp;
		[SerializeField]
		public UnityEngine.UI.Button BtnExpUp;
		[SerializeField]
		public UnityEngine.UI.Button BtnClose;
		[SerializeField]
		public TMPro.TextMeshProUGUI TextCoin;
		
		private UIRewardPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			BtnCoinUp = null;
			BtnExpUp = null;
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
