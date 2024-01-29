/****************************************************************************
 * 2024.1 DESKTOP-AETQQ8U
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectVampire
{
	public partial class BtnUpgradePrefab
	{
		[SerializeField] public TMPro.TextMeshProUGUI Text;
		[SerializeField] public UnityEngine.UI.Image Icon;

		public void Clear()
		{
			Text = null;
			Icon = null;
		}

		public override string ComponentName
		{
			get { return "BtnUpgradePrefab";}
		}
	}
}
