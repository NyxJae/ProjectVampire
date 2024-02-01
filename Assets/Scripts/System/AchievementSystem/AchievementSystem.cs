using System.Collections.Generic;
using System.Linq;
using QAssetBundle;
using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public class AchievementSystem : AbstractSystem
    {
        // expUpgradeitems 为经验升级系统的经验升级列表
        public List<ExpUpgradeItem> expUpgradeItems = new();

        // lst 为成就列表    
        public List<AchievementItem> lst = new();

        // saveUtility 为存档工具
        private SaveUtility _saveUtility => this.GetUtility<SaveUtility>();

        // GlobalModel
        private GlobalModel GlobalModel => this.GetModel<GlobalModel>();

        // PlayerModel
        private PlayerModel PlayerModel => this.GetModel<PlayerModel>();


        public void Load()
        {
            // 读取成就数据
            foreach (var item in lst) item.Load(_saveUtility);
        }

        // 链式封装 Add 方法 添加成就
        public AchievementSystem Add(AchievementItem item)
        {
            lst.Add(item);
            return this;
        }

        protected override void OnInit()
        {
            expUpgradeItems = this.GetSystem<ExpUpgradeSystem>().ExpUpdateItems;
            // 添加成就 初次见面  
            Add(new AchievementItem()
                .SetName("初次见面")
                .SetDescription("完成第一次游戏\n获得5个金币")
                .SetKey("FirstGame")
                .SetIconName(Icons.FIRSTMEETICON)
                .SetCondition(() => GlobalModel.Time.Value > 0)
                .SetReward(() => GlobalModel.Coin.Value += 5)
            );
            // 添加成就 坚持5分钟 
            Add(new AchievementItem()
                .SetName("坚持5分钟")
                .SetDescription("坚持5分钟\n获得10个金币")
                .SetKey("Keep5Minutes")
                .SetIconName(Icons.TIMEENOUGHICON)
                .SetCondition(() => GlobalModel.Time.Value > 5 * 60)
                .SetReward(() => GlobalModel.Coin.Value += 10)
            );
            // 添加成就 坚持10分钟
            Add(new AchievementItem()
                .SetName("坚持10分钟")
                .SetDescription("坚持10分钟\n获得10个金币")
                .SetKey("Keep10Minutes")
                .SetIconName(Icons.TIMEENOUGHICON)
                .SetCondition(() => GlobalModel.Time.Value > 10 * 60)
                .SetReward(() => GlobalModel.Coin.Value += 10)
            );
            // 添加成就 坚持30分钟
            Add(new AchievementItem()
                .SetName("坚持30分钟")
                .SetDescription("坚持30分钟\n获得10个金币")
                .SetKey("Keep30Minutes")
                .SetIconName(Icons.TIMEENOUGHICON)
                .SetCondition(() => GlobalModel.Time.Value > 30 * 60)
                .SetReward(() => GlobalModel.Coin.Value += 10)
            );
            // 添加成就 达到等级5
            Add(new AchievementItem()
                .SetName("达到等级5")
                .SetDescription("达到等级5\n获得10个金币")
                .SetKey("Level5")
                .SetIconName(Icons.LEVELUPENOUGHICON)
                .SetCondition(() => PlayerModel.Level.Value >= 5)
                .SetReward(() => GlobalModel.Coin.Value += 10)
            );
            // 添加成就 达到等级10
            Add(new AchievementItem()
                .SetName("达到等级10")
                .SetDescription("达到等级10\n获得10个金币")
                .SetKey("Level10")
                .SetIconName(Icons.LEVELUPENOUGHICON)
                .SetCondition(() => PlayerModel.Level.Value >= 10)
                .SetReward(() => GlobalModel.Coin.Value += 10)
            );
            // 添加成就 达到等级15
            Add(new AchievementItem()
                .SetName("达到等级15")
                .SetDescription("达到等级15\n获得10个金币")
                .SetKey("Level15")
                .SetIconName(Icons.LEVELUPENOUGHICON)
                .SetCondition(() => PlayerModel.Level.Value >= 15)
                .SetReward(() => GlobalModel.Coin.Value += 10)
            );
            // 添加成就 达到等级20
            Add(new AchievementItem()
                .SetName("达到等级20")
                .SetDescription("达到等级20\n获得10个金币")
                .SetKey("Level20")
                .SetIconName(Icons.LEVELUPENOUGHICON)
                .SetCondition(() => PlayerModel.Level.Value >= 20)
                .SetReward(() => GlobalModel.Coin.Value += 10)
            );
            // 添加成就 达到金币50
            Add(new AchievementItem()
                .SetName("达到金币50")
                .SetDescription("达到金币50\n获得10个金币")
                .SetKey("Coin50")
                .SetIconName(Icons.COINENOUGHICON)
                .SetCondition(() => GlobalModel.Coin.Value >= 50)
                .SetReward(() => GlobalModel.Coin.Value += 10)
            );
            // 添加成就 达到金币100
            Add(new AchievementItem()
                .SetName("达到金币100")
                .SetDescription("达到金币100\n获得10个金币")
                .SetKey("Coin100")
                .SetIconName(Icons.COINENOUGHICON)
                .SetCondition(() => GlobalModel.Coin.Value >= 100)
                .SetReward(() => GlobalModel.Coin.Value += 10)
            );
            // 雷电场解锁成就
            Add(new AchievementItem()
                .SetName("雷电场解锁")
                .SetDescription("解锁雷电场\n获得10个金币")
                .SetKey("UnlockThunder")
                .SetIconName(Icons.LIGHTNINGFIELDICON)
                .SetCondition(() => expUpgradeItems.First(item => item.Key == "LightingSite").IsUpdated)
                .SetReward(() => GlobalModel.Coin.Value += 10)
            );
            // 火球解锁成就
            Add(new AchievementItem()
                .SetName("火球解锁")
                .SetDescription("解锁火球\n获得10个金币")
                .SetKey("UnlockFireball")
                .SetIconName(Icons.FIREBALLICON)
                .SetCondition(() => expUpgradeItems.First(item => item.Key == "FireBall").IsUpdated)
                .SetReward(() => GlobalModel.Coin.Value += 10)
            );
            Load();


            // 成就解锁检测
            ActionKit.OnUpdate.Register(() =>
            {
                // 每过30帧检测一次
                if (Time.frameCount % 30 == 0)
                    foreach (var item in lst.Where(item => !item.IsCompleted && item.Condition()))
                    {
                        item.Unlock();
                        item.Save(_saveUtility);
                    }
            });
        }
    }
}