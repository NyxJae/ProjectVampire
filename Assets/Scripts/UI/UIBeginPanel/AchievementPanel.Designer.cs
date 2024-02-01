/****************************************************************************
 * 2024.1 DESKTOP-AETQQ8U
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectVampire
{
	public partial class AchievementPanel
	{
		[SerializeField] public UnityEngine.UI.Button BtnClose;
		[SerializeField] public AchievementRoot AchievementRoot;

		public void Clear()
		{
			BtnClose = null;
			AchievementRoot = null;
		}

		public override string ComponentName
		{
			get { return "AchievementPanel";}
		}
	}
}
