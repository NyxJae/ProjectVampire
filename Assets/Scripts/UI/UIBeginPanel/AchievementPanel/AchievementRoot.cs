/****************************************************************************
 * 2024.1 DESKTOP-AETQQ8U
 ****************************************************************************/

using System.Collections.Generic;
using System.Linq;
using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    // 根据成就系统的成就列表生成成就按钮
    public partial class AchievementRoot : UIElement, IController
    {
        // reskit 的 loader
        private readonly ResLoader _resLoader = ResLoader.Allocate();

        // 获取achievement系统的成就列表
        private List<AchievementItem> mAchievementList;


        private void Awake()
        {
            var mAchievementSystem = this.GetSystem<AchievementSystem>();
            mAchievementList = mAchievementSystem.lst;
            // 遍历成就列表
            foreach (var item in mAchievementList.Where(item => !item.IsRewarded))
            {
                var itemCastch = item;
                BtnAchievemenPrefab.InstantiateWithParent(this)
                    .Self(self =>
                    {
                        // 缓存self
                        var selfCatch = self;
                        selfCatch.Text.text = itemCastch.Description;
                        selfCatch.Icon.sprite = _resLoader.LoadSync<Sprite>(itemCastch.IconName);
                        selfCatch.Button.interactable = itemCastch.IsCompleted;
                        // 设置成就按钮的点击事件
                        selfCatch.Button.onClick.AddListener(() =>
                        {
                            var command = new OptionTriggerCommand(itemCastch, this.GetUtility<SaveUtility>());
                            this.SendCommand(command);
                            selfCatch.Button.Hide();
                        });
                    }).Show();
            }
        }

        private void OnEnable()
        {
            // 循环遍历成就列表 显示没有领取的成就
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