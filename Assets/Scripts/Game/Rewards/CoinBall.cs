using DG.Tweening;
using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public partial class CoinBall : Entity
    {
        private const float CollectDistance = 0.1f; // 设定收集距离阈值为0.1单位
        protected override Collider2D HitBoxCollider2D => HitBox;

        private void Start()
        {
            HitBox.Show();
            HitBox.OnTriggerStay2DEvent(other =>
            {
                if (other.transform.parent.name == "PickAbility")
                    GetCoin();
            }).UnRegisterWhenGameObjectDestroyed(this);
        }

        public void GetCoin()
        {
            HitBox.Hide();
            var distance = 1f;
            var dir = (transform.position - Player.Instance.transform.position).normalized;
            var target = new Vector3(dir.x * distance, dir.y * distance, 0);

            transform.DOMove(transform.position + target, 0.3f).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                var moveDuration = 1f; // 接近玩家动画的持续时间
                DOVirtual.Float(0, 1, moveDuration, value =>
                {
                    if (Vector3.Distance(transform.position, Player.Instance.transform.position) <= CollectDistance)
                    {
                        CollectCoin();
                        return;
                    }

                    transform.position = Vector3.Lerp(transform.position, Player.Instance.transform.position, value);
                }).SetEase(Ease.OutSine).OnComplete(CollectCoin);
            });
        }

        private void CollectCoin()
        {
            AudioKit.PlaySound("Coin");
            Global.Coin.Value += 1;
            Destroy(gameObject);
        }
    }
}