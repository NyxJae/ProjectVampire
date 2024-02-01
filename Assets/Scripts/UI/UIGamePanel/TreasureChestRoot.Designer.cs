/****************************************************************************
 * 2024.1 DESKTOP-AETQQ8U
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectVampire
{
	public partial class TreasureChestRoot
	{
		[SerializeField] public UnityEngine.UI.Button BtnYes;
		[SerializeField] public TMPro.TextMeshProUGUI TexTreasureChest;
		[SerializeField] public UnityEngine.UI.Image Icon;

		public void Clear()
		{
			BtnYes = null;
			TexTreasureChest = null;
			Icon = null;
		}

		public override string ComponentName
		{
			get { return "TreasureChestRoot";}
		}
	}
}
