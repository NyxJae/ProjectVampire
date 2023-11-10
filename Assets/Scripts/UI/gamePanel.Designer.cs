using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectVampire
{
	// Generate Id:f30618fc-5281-40f1-b829-ce128c211814
	public partial class gamePanel
	{
		public const string Name = "gamePanel";
		
		[SerializeField]
		public UnityEngine.UI.Text TextExp;
		
		private gamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			TextExp = null;
			
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
