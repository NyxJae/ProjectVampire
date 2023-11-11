using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectVampire
{
	// Generate Id:22e05649-c499-42a0-aee6-8e05512f6f40
	public partial class gamePanel
	{
		public const string Name = "gamePanel";
		
		[SerializeField]
		public UnityEngine.UI.Text TextExp;
		[SerializeField]
		public UnityEngine.UI.Text TextHP;
		[SerializeField]
		public UnityEngine.UI.Text TextLevel;
		
		private gamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			TextExp = null;
			TextHP = null;
			TextLevel = null;
			
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
