using QAssetBundle;
using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public partial class Enemy : Entity, IEnemy
    {
        [SerializeField] [Tooltip("敌人的血量")] private float mhealth = 100f;

        [SerializeField] [Tooltip("敌人的速度")] private float mspeed = 5f;

        // 最后一次受伤的时间
        private float lastHitTime = -0.1f; // 初始化为-0.1f确保一开始可以受到伤害

        /// <summary>
        ///     私有的 player 角色
        /// </summary>
        private GameObject player;

        protected override Collider2D HitBoxCollider2D => HitBox;

        private void Start()
        {
            // 获取 player 角色
            player = Player.Instance.gameObject;
        }

        private void Update()
        {
            // 如果 player 为空,则返回
            if (player == null) return;
            // 追逐 player
            ChasingPlayrt();
        }

        public float Health
        {
            get => mhealth;
            set => mhealth = value;
        }

        public float Speed
        {
            get => mspeed;
            set => mspeed = value;
        }

        public float Attack { get; set; } = 1f;

        /// <summary>
        ///     受伤处理，改变颜色并减少生命值，处理debuff。
        /// </summary>
        /// <param name="damage">受到的伤害值。</param>
        public void TakeDamage(float damage, bool isCritical = false)
        {
            if (Time.time - lastHitTime < 0.1f) return;
            lastHitTime = Time.time;
            Sprite.color = Color.red; // 暴击时使用黄色
            Health -= damage;
            FloatingText.Instance.Play(damage.ToString(), transform.position, isCritical); // 传递暴击信息
            ActionKit.Delay(0.1f, () => Sprite.color = Color.white).Start(this);
            CheckHealth();
        }

        public void AdjustAttributes(float healthMultiplier, float speedMultiplier)
        {
            Health *= healthMultiplier;
            Speed *= speedMultiplier;
        }

        /// <summary>
        ///     追逐 player
        /// </summary>
        private void ChasingPlayrt()
        {
            // 获取 player 的位置
            var playerPosition = player.transform.position;
            // 获取 enemy 的位置
            var enemyPosition = transform.position;
            // 计算 player 和 enemy 的距离
            var distance = Vector3.Distance(playerPosition, enemyPosition);
            // 如果距离大于 0.2f
            if (distance > 0.2f)
            {
                // 计算 player 和 enemy 的方向
                var direction = (playerPosition - enemyPosition).normalized;
                // 计算移动距离
                var moveDistance = direction * (Speed * Time.deltaTime);
                // 移动
                transform.Translate(moveDistance);
            }
        }

        /// <summary>
        ///     检测血量
        /// </summary>
        public void CheckHealth()
        {
            // 如果血量小于等于 0
            if (Health <= 0)
            {
                // 减少敌人数量
                EnemyGenerator.Instance.enemyCount--;
                // 掉落奖励
                PowerUpManager.Instance.DroReward(gameObject);
                // 播放死亡特效
                FxController.Instance.Play(Sprite);
                // 播放音效
                AudioKit.PlaySound(Sfx.ENEMY_DIE);
                // 销毁自身
                this.DestroyGameObjGracefully();
            }
        }
    }
}