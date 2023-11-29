using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectVampire
{
	// Generate Id:e2ec1c5b-b724-4b36-acb2-18c23ef60b04
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
		public RectTransform BtnUpdateRoot;
		[SerializeField]
		public UnityEngine.UI.Button BtnUpdateATK;
		[SerializeField]
		public UnityEngine.UI.Button BtnUpgradeATKRate;
		[SerializeField]
		public UnityEngine.UI.Text TextCoin;
		
		private UIGamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			TextExp = null;
			TextHP = null;
			TextLevel = null;
			TextTime = null;
			BtnUpdateRoot = null;
			BtnUpdateATK = null;
			BtnUpgradeATKRate = null;
			TextCoin = null;
			
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
