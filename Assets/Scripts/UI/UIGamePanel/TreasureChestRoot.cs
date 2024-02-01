/****************************************************************************
 * 2024.1 DESKTOP-AETQQ8U
 ****************************************************************************/

using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public partial class TreasureChestRoot : UIElement, IController
    {
        public static EasyEvent OpenTreasureChestEvent = new();

        // 资源加载器
        private readonly ResLoader ResLoader = ResLoader.Allocate();

        // 升级项
        private ExpUpgradeItem item;

        // GlobalModel
        private GlobalModel _GlobalModel => this.GetModel<GlobalModel>();

        private void Awake()
        {
            Hide();
            OpenTreasureChestEvent.Register(() =>
            {
                Show();
                Time.timeScale = 0;
                item = this.GetSystem<ExpUpgradeSystem>().GetOneRandomUnUpdatedItem();
                TexTreasureChest.text = item != null ? item.Description : "增加10金币";
                Icon.sprite = ResLoader.LoadSync<Sprite>(item.IconName);
            }).UnRegisterWhenGameObjectDestroyed(this);

            BtnYes.onClick.AddListener(() =>
            {
                if (item != null)
                    item.Trigger();
                else
                    _GlobalModel.Coin.Value += 10;
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