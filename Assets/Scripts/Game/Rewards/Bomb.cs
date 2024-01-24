using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public partial class Bomb : Entity
    {
        protected override Collider2D HitBoxCollider2D => HitBox;

        private void Start()
        {
            HitBox.OnTriggerStay2DEvent(other =>
            {
                // 如果碰撞器的父物体的名字为PickAbility
                if (other.transform.parent.name == "PickAbility")
                    // todo 动画 最开始30帧先远离玩家 然后再向玩家靠近 碰到玩家后爆炸
                    // 获取炸弹方法
                    GetBomb();
            }).UnRegisterWhenGameObjectDestroyed(this);
            ActionKit.Repeat()
                .Delay(1f)
                // 与play的距离超过10f
                .Condition(() => Vector3.Distance(transform.position, Player.Instance.transform.position) > 30f)
                .Callback(() =>
                {
                    // 减少PowerUpManager中炸弹的计数器
                    PowerUpManager.Instance.DecreaseBombCount();
                    // 销毁自身
                    Destroy(gameObject);
                })
                .Start(this);
        }

        // 公开的 获取炸弹 方法
        public void GetBomb()
        {
            // 播放音效
            AudioKit.PlaySound("Bomb");
            UIGamePanel.screenFlashEvent.Trigger();
            // 震动摄像机
            MainCamera.Instance.Shake();
            // 找到所有Enemy,不排序,不包含隐藏的
            var enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            // 遍历所有敌人
            foreach (var enemy in enemies)
                // 减少敌人血量
                enemy.TakeDamage(99999);
            // 销毁自身
            Destroy(gameObject);

            // 减少PowerUpManager中炸弹的计数器
            PowerUpManager.Instance.DecreaseBombCount();
        }
    }
}