/****************************************************************************
 * 2024.1 DESKTOP-AETQQ8U
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectVampire
{
	public partial class AchievementRoot
	{
		[SerializeField] public BtnAchievemenPrefab BtnAchievemenPrefab;

		public void Clear()
		{
			BtnAchievemenPrefab = null;
		}

		public override string ComponentName
		{
			get { return "AchievementRoot";}
		}
	}
}
