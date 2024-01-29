/****************************************************************************
 * 2024.1 DESKTOP-AETQQ8U
 ****************************************************************************/

using QFramework;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectVampire
{
    public partial class BtnUpgradePrefab : UIElement
    {
        // 对外提供 Button 组件
        public Button Button => GetComponent<Button>();

        // 对外提供 gameObject 组件
        public GameObject GameObject => gameObject;

        private void Awake()
        {
        }

        protected override void OnBeforeDestroy()
        {
        }
    }
}