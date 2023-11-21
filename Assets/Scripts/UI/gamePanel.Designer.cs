using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectVampire
{
	// Generate Id:5e9db04b-52d5-4cce-b135-fd55b6f91f97
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
