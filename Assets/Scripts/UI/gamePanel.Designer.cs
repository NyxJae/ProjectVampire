using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectVampire
{
	// Generate Id:842bb062-6012-455e-8082-a7b33c53b8d5
	public partial class gamePanel
	{
		public const string Name = "gamePanel";
		
		[SerializeField]
		public UnityEngine.UI.Text TextExp;
		[SerializeField]
		public UnityEngine.UI.Text TextHP;
		
		private gamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			TextExp = null;
			TextHP = null;
			
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
