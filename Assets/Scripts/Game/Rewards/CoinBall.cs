using DG.Tweening;
using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public partial class CoinBall : Entity, IController
    {
        private const float CollectDistance = 0.1f; // 设定收集距离阈值为0.1单位

        protected override Collider2D HitBoxCollider2D => HitBox;

        //GlobalModel
        private GlobalModel GlobalModel => this.GetModel<GlobalModel>();

        private void Start()
        {
            HitBox.Show();
            HitBox.OnTriggerStay2DEvent(other =>
            {
                if (other.transform.parent.name == "PickAbility")
                    GetCoin();
            }).UnRegisterWhenGameObjectDestroyed(this);
        }

        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
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
                    // 在回调中检查 CoinBall 是否已经被销毁
                    if (this == null) return;

                    if (Vector3.Distance(transform.position, Player.Instance.transform.position) <= CollectDistance)
                    {
                        CollectCoin();
                        return;
                    }

                    transform.position = Vector3.Lerp(transform.position, Player.Instance.transform.position, value);
                }).SetEase(Ease.OutSine).OnComplete(() =>
                {
                    // 再次检查 CoinBall 是否还存在
                    if (this != null) CollectCoin();
                });
            });
        }


        private void CollectCoin()
        {
            AudioKit.PlaySound("Coin");
            GlobalModel.Coin.Value += 1;
            Destroy(gameObject);
        }
    }
}