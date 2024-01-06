/****************************************************************************
 * 2024.1 DESKTOP-AETQQ8U
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectVampire
{
	public partial class ExpUpgradeRoot
	{
		[SerializeField] public RectTransform GroupExpUpgradeBtns;
		[SerializeField] public UnityEngine.UI.Button BtnExpUpgradePrefab;

		public void Clear()
		{
			GroupExpUpgradeBtns = null;
			BtnExpUpgradePrefab = null;
		}

		public override string ComponentName
		{
			get { return "ExpUpgradeRoot";}
		}
	}
}
