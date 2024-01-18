/****************************************************************************
 * 2024.1 DESKTOP-AETQQ8U
 ****************************************************************************/

using System.Linq;
using ProjectVampire.System.ExpUpgradeSystem;
using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public partial class TreasureChestRoot : UIElement, IController
    {
        public static EasyEvent openTreasureChestEvent = new();

        // 升级项
        private ExpUpgradeItem item;

        private void Awake()
        {
            Hide();
            openTreasureChestEvent.Register(() =>
            {
                Show();
                Time.timeScale = 0;
                this.GetSystem<ExpUpgradeSystem>().RandomUnUpdatedItem(1);
                item = this.GetSystem<ExpUpgradeSystem>().ExpUpdateItems.Where(item => item.ConditionCheck())
                    .ToList()[0];
                TexTreasureChest.text = item != null ? item.Description : "增加10金币";
            }).UnRegisterWhenGameObjectDestroyed(this);

            BtnYes.onClick.AddListener(() =>
            {
                if (item != null)
                    item.Upgrade();
                else
                    Global.Coin.Value += 10;
                Hide();
                Time.timeScale = 1;
            });
        }

        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }

        protected override void OnBeforeDestroy()
        {
        }
    }
}