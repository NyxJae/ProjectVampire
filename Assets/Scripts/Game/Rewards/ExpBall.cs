using DG.Tweening;
using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public partial class ExpBall : Entity
    {
        // 设定爆炸距离阈值为0.1
        private readonly float ExplosionDistance = 0.1f;

        // 私有的经验值属性
        private readonly int mExp = 1;

        protected override Collider2D HitBoxCollider2D => HitBox;

        private void Start()
        {
            HitBox.Show();
            // 当碰撞触发时，检查碰撞体是否属于“PickAbility”父对象，如果是，则调用获取经验方法
            HitBox.OnTriggerStay2DEvent(other =>
            {
                if (other.transform.parent.name == "PickAbility")
                    GetExp();
            }).UnRegisterWhenGameObjectDestroyed(this);
        }

        public void GetExp()
        {
            HitBox.Hide();
            // 设置移动距离
            var distance = 1f;
            // 计算方向
            var dir = (transform.position - Player.Instance.transform.position).normalized;
            // 计算目标位置
            var target = new Vector3(dir.x * distance, dir.y * distance, 0);

            // 移动到目标位置
            transform.DOMove(transform.position + target, 0.3f).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                // 设置移动持续时间
                var moveDuration = 1.5f;
                // 动画移动经验值
                DOVirtual.Float(0, 1, moveDuration, value =>
                {
                    // 如果距离小于爆炸距离，则收集经验
                    if (Vector3.Distance(transform.position, Player.Instance.transform.position) <= ExplosionDistance)
                    {
                        CollectExp();
                        return;
                    }

                    // 使用线性插值移动位置
                    transform.position = Vector3.Lerp(transform.position, Player.Instance.transform.position, value);
                }).SetEase(Ease.OutSine).OnComplete(CollectExp);
            });
        }

        // 收集经验的逻辑抽象到一个方法中
        private void CollectExp()
        {
            // 播放经验值收集音效
            AudioKit.PlaySound("Exp");
            // 将经验值加到全局经验值中
            Global.Exp.Value += mExp;
            // 销毁当前对象
            Destroy(gameObject);
        }
    }
}