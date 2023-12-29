using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public partial class Enemy : ViewController, IEnemy
    {
        /// <summary>
        ///     私有的 移动速度系数 属性 在 Inspector 中显示
        /// </summary>
        [SerializeField] [Tooltip("移动速度系数")] private float mSpeed = 3.0f;


        // 公开的 血量 属性
        [SerializeField] [Tooltip("血量")] private float mHealth = 3;

        /// <summary>
        ///     私有的 player 角色
        /// </summary>
        private GameObject player;


        private void Start()
        {
            // 获取敌人生成器
            EnemyGenerator.Instance.EnemyCount++;
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
            get => mHealth;
            set => mHealth = value;
        }

        /// <summary>
        ///     受伤处理，改变颜色并减少生命值。
        /// </summary>
        /// <param name="damage">受到的伤害值。</param>
        /// <param name="changeDuration">颜色改变持续的时间。</param>
        public void TakeDamage(float damage, float changeDuration = 0.1f)
        {
            Sprite.color = Color.red; // 改变颜色为红色
            Health -= damage; // 减少生命值
            // 显示浮动文字
            FloatingText.Instance.play(damage.ToString(), transform.position);
            ActionKit.Delay(changeDuration, () => Sprite.color = Color.white).Start(this); // 延时后恢复颜色
            CheckHealth();
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
                var moveDistance = direction * (mSpeed * Time.deltaTime);
                // 移动
                transform.Translate(moveDistance);
            }
        }

        /// <summary>
        ///     检测血量
        /// </summary>
        private void CheckHealth()
        {
            // 如果血量小于等于 0
            if (Health <= 0)
            {
                // 减少敌人数量
                EnemyGenerator.Instance.EnemyCount--;
                // 掉落奖励
                PowerUpManager.Instance.DroReward(gameObject);
                // 销毁自身
                this.DestroyGameObjGracefully();
            }
        }
    }
}