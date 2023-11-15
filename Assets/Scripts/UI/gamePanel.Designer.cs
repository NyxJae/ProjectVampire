using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectVampire
{
	// Generate Id:6441d8fa-07c4-4fc4-89aa-e66d9a9a30d8
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
		public UnityEngine.UI.Button BtnUpdate;
		
		private gamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			TextExp = null;
			TextHP = null;
			TextLevel = null;
			TextTime = null;
			BtnUpdate = null;
			
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
