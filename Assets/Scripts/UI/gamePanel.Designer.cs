using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectVampire
{
	// Generate Id:31ec7a14-1212-4b17-a9f7-0f609de1b414
	public partial class gamePanel
	{
		public const string Name = "gamePanel";
		
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
		
		private gamePanelData mPrivateData = null;
		
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
		
		public gamePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		gamePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new gamePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
