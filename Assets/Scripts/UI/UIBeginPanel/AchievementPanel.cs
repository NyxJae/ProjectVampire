/****************************************************************************
 * 2024.1 DESKTOP-AETQQ8U
 ****************************************************************************/

using QFramework;

namespace ProjectVampire
{
    public partial class AchievementPanel : UIElement
    {
        private void Awake()
        {
            Hide();
            BtnClose.onClick.AddListener(Hide);
        }

        protected override void OnBeforeDestroy()
        {
        }
    }
}