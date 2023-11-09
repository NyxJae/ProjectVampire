using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:8f668017-7dd3-4f44-8497-0ab3a5fb4260
	public partial class endPanel
	{
		public const string Name = "endPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button BtnReplay;
		
		private endPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			BtnReplay = null;
			
			mData = null;
		}
		
		public endPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		endPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new endPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
