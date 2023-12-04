using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectVampire
{
	// Generate Id:b4f1be79-6ba3-44bb-b71c-ad0e9fc3c45b
	public partial class UIGamePanel
	{
		public const string Name = "UIGamePanel";
		
		[SerializeField]
		public UnityEngine.UI.Text TextExp;
		[SerializeField]
		public UnityEngine.UI.Text TextHP;
		[SerializeField]
		public UnityEngine.UI.Text TextLevel;
		[SerializeField]
		public UnityEngine.UI.Text TextTime;
		[SerializeField]
		public UnityEngine.UI.Text TextCoin;
		[SerializeField]
		public RectTransform BtnUpdateRoot;
		[SerializeField]
		public UnityEngine.UI.Button BtnUpdateATK;
		[SerializeField]
		public UnityEngine.UI.Button BtnUpgradeATKRate;
		
		private UIGamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			TextExp = null;
			TextHP = null;
			TextLevel = null;
			TextTime = null;
			TextCoin = null;
			BtnUpdateRoot = null;
			BtnUpdateATK = null;
			BtnUpgradeATKRate = null;
			
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
