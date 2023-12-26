using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectVampire
{
	// Generate Id:add6afa9-53c6-4ac2-818f-5e5f75385afc
	public partial class UIEndPanel
	{
		public const string Name = "UIEndPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button BtnReplay;
		
		private UIEndPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			BtnReplay = null;
			
			mData = null;
		}
		
		public UIEndPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIEndPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIEndPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
