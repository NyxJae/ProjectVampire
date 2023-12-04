using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectVampire
{
	// Generate Id:245fb286-04f1-4a2f-bf1b-3c3a5f2c24fb
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
