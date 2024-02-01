/****************************************************************************
 * 2024.1 DESKTOP-AETQQ8U
 ****************************************************************************/

using QFramework;
using UnityEngine.UI;

namespace ProjectVampire
{
    public partial class BtnAchievemenPrefab : UIElement
    {
        // 公开 Button 组件
        public Button Button => GetComponent<Button>();

        private void Awake()
        {
            Hide();
        }


        protected override void OnBeforeDestroy()
        {
        }
    }
}