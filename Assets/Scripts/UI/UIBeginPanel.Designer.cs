using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectVampire
{
	// Generate Id:390d15b3-b4da-4847-838a-b8a432093ac3
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
