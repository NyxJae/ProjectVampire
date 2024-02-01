using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectVampire
{
	// Generate Id:3462cef7-6f9e-4507-be7a-ad2e6d627934
	public partial class UIBeginPanel
	{
		public const string Name = "UIBeginPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button BtnReward;
		[SerializeField]
		public UnityEngine.UI.Button BtnBegin;
		[SerializeField]
		public UnityEngine.UI.Button BtnAchievement;
		[SerializeField]
		public AchievementPanel AchievementPanel;
		
		private UIBeginPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			BtnReward = null;
			BtnBegin = null;
			BtnAchievement = null;
			AchievementPanel = null;
			
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
