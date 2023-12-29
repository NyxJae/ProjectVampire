using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectVampire
{
	// Generate Id:29df6fd0-d808-470a-9105-e0edfec3a466
	public partial class UIBeginPanel
	{
		public const string Name = "UIBeginPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button BtnReward;
		[SerializeField]
		public UnityEngine.UI.Button BtnBegin;
		
		private UIBeginPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			BtnReward = null;
			BtnBegin = null;
			
			mData = null;
		}
		
		public UIBeginPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIBeginPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIBeginPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
