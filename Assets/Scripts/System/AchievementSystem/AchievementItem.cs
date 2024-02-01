using System;
using QFramework;

namespace ProjectVampire
{
    public class AchievementItem : ISystemItem
    {
        // 成就解锁事件
        public EasyEvent OnAchievementItemUnlocked = new();

        // 成就名称
        public string Name { get; private set; }

        // 成就描述
        public string Description { get; private set; }

        // 成就图标
        public string IconName { get; private set; }

        // 成就key
        public string Key { get; private set; }

        // 成就是否完成
        public bool IsCompleted { get; private set; }

        // 成就是否领取
        public bool IsRewarded { get; private set; }

        // 成就完成条件
        public Func<bool> Condition { get; private set; }

        // 成就奖励
        public Action Reward { get; private set; }

        // save
        public void Save(SaveUtility saveUtility)
        {
            saveUtility.Save($"{Key}_IsCompleted", IsCompleted);
            saveUtility.Save($"{Key}_IsRewarded", IsRewarded);
        }

        // 成就完成
        public void Unlock()
        {
            IsCompleted = true;
            OnAchievementItemUnlocked.Trigger();
        }

        // load 
        public void Load(SaveUtility saveUtility)
        {
            IsCompleted = saveUtility.LoadBool($"{Key}_IsCompleted", IsCompleted);
            IsRewarded = saveUtility.LoadBool($"{Key}_IsRewarded", IsRewarded);
        }

        // 链式封装 Set 方法
        // 设置成就名称
        public AchievementItem SetName(string name)
        {
            Name = name;
            return this;
        }

        // 设置成就描述
        public AchievementItem SetDescription(string description)
        {
            Description = description;
            return this;
        }

        // 设置成就图标
        public AchievementItem SetIconName(string iconName)
        {
            IconName = iconName;
            return this;
        }

        // 设置成就key
        public AchievementItem SetKey(string key)
        {
            Key = key;
            return this;
        }

        // 设置成就完成条件
        public AchievementItem SetCondition(Func<bool> condition)
        {
            Condition = condition;
            return this;
        }

        // 设置成就奖励
        public AchievementItem SetReward(Action reward)
        {
            Reward = reward;
            return this;
        }

        // 领取奖励
        public void Trigger()
        {
            IsRewarded = true;
            Reward?.Invoke();
        }
    }
}