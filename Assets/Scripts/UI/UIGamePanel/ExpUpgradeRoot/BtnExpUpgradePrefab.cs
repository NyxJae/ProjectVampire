/****************************************************************************
 * 2024.1 DESKTOP-AETQQ8U
 ****************************************************************************/

using QFramework;
using UnityEngine.UI;

namespace ProjectVampire
{
    public partial class BtnExpUpgradePrefab : UIElement
    {
        // button 组件
        public Button Button => GetComponent<Button>();

        private void Awake()
        {
        }

        protected override void OnBeforeDestroy()
        {
        }
    }
}