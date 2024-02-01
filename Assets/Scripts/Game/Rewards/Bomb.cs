using DG.Tweening;
using QFramework;
using UnityEngine;

// dotween

namespace ProjectVampire
{
    public partial class Bomb : Entity
    {
        private readonly float ExplosionDistance = 0.1f; // 设定爆炸距离阈值为0.1单位

        // 计时器
        private float _Timer;
        protected override Collider2D HitBoxCollider2D => HitBox;

        private void Start()
        {
            HitBox.Show();
            HitBox.OnTriggerStay2DEvent(other =>
            {
                // 如果碰撞器的父物体的名字为PickAbility 并且没有飞向玩家
                if (other.transform.parent.name == "PickAbility") GetBomb();
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
            HitBox.Hide();
            var distance = 1f;
            var dir = (transform.position - Player.Instance.transform.position).normalized;
            var target = new Vector3(dir.x * distance, dir.y * distance, 0);

            transform.DOMove(transform.position + target, 0.3f).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                var moveDuration = 1f;
                DOVirtual.Float(0, 1, moveDuration, value =>
                {
                    if (this == null) return;
                    // 如果炸弹距离玩家小于等于ExplosionDistance，则直接执行爆炸效果
                    if (Vector3.Distance(transform.position, Player.Instance.transform.position) <=
                        ExplosionDistance)
                    {
                        PerformBombEffects();
                        return; // 执行爆炸后立即返回，不再继续移动
                    }

                    transform.position =
                        Vector3.Lerp(transform.position, Player.Instance.transform.position, value);
                }).SetEase(Ease.OutSine).OnComplete(() =>
                {
                    // 当炸弹到达玩家位置时也执行爆炸效果
                    PerformBombEffects();
                });
            });
        }

        // 炸弹爆炸效果
        private void PerformBombEffects()
        {
            AudioKit.PlaySound("Bomb");
            UIGamePanel.screenFlashEvent.Trigger();
            MainCamera.Instance.Shake();
            var enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            foreach (var enemy in enemies) enemy.TakeDamage(99999);
            PowerUpManager.Instance.DecreaseBombCount();
            Destroy(gameObject);
        }
    }
}