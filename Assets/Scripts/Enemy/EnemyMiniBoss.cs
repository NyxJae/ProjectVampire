using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public partial class EnemyMiniBoss : ViewController, IEnemy
    {
        [SerializeField] [Tooltip("敌人的血量")] private float mhealth = 100f;
        [SerializeField] [Tooltip("敌人的速度")] private float mspeed = 5f;

        /// <summary>
        ///     私有的 攻击距离 属性 在 Inspector 中显示
        /// </summary>
        [SerializeField] [Tooltip("敌人的攻击距离")] private float mAttackDistance = 5f;

        /// <summary>
        ///     私有的 攻击前摇时间 属性 在 Inspector 中显示
        /// </summary>
        [SerializeField] [Tooltip("敌人的攻击前摇时间")]
        private float mAfterAttackTime = 3f;

        // 声明状态机
        private readonly FSM<State> mFsm = new();


        // rigidbody2D 组件
        private Rigidbody2D mRigidbody2D;

        // 玩家实例
        private Player player;

        private void Start()
        {
            // 增加敌人数量
            EnemyGenerator.Instance.EnemyCount++;
            // 获取玩家实例
            player = Player.Instance;
            // 获取rigidbody2D组件
            mRigidbody2D = GetComponent<Rigidbody2D>();
            // 状态机添加状态
            mFsm.State(State.follow)
                .OnFixedUpdate(() =>
                {
                    // log 现在状态
                    Debug.Log($"当前状态: {State.follow}");
                    // 获取 player 的位置
                    var playerPosition = player.transform.position;
                    // 获取 enemy 的位置
                    var enemyPosition = transform.position;
                    // 计算 player 和 enemy 的距离
                    var distance = Vector3.Distance(playerPosition, enemyPosition);
                    // 如果距离大于 攻击距离
                    if (distance > mAttackDistance)
                    {
                        // 计算 player 和 enemy 的方向
                        var direction = (playerPosition - enemyPosition).normalized;
                        // 计算移动距离
                        var moveDistance = direction * (Speed * Time.deltaTime);
                        // 移动
                        transform.Translate(moveDistance);
                    }
                    else
                    {
                        // 切换状态
                        mFsm.ChangeState(State.accumulation);
                    }
                });
            mFsm.State(State.accumulation)
                .OnUpdate(() =>
                {
                    // todo: 频率由快至慢的红白闪烁
                    // 计算当前状态持续时间的百分比
                    var timePercent = mFsm.SecondsOfCurrentState / mAfterAttackTime;
                    // 计算闪烁频率，随时间逐渐减慢
                    var blinkFrequency = Mathf.Lerp(0.1f, 1.0f, timePercent);
                    // 通过时间和频率计算颜色改变周期
                    var colorChangeCycle = Mathf.Sin(Time.time * Mathf.PI * blinkFrequency);
                    // 根据周期选择颜色
                    Sprite.color = colorChangeCycle > 0 ? Color.red : Color.white;

                    if (mFsm.SecondsOfCurrentState > mAfterAttackTime)
                    {
                        mFsm.ChangeState(State.attack);
                        // 变回白色
                        Sprite.color = Color.white;
                    }
                });
            mFsm.State(State.attack)
                .OnEnter(() =>
                {
                    // 获取 player 的位置
                    var playerPosition = player.transform.position;
                    // 获取 enemy 的位置
                    var enemyPosition = transform.position;
                    // 计算 方向
                    var direction = (playerPosition - enemyPosition).normalized;
                    // 设置速度
                    mRigidbody2D.velocity = direction * Speed * 5f;
                })
                .OnUpdate(() =>
                {
                    if (mFsm.SecondsOfCurrentState > 0.5f)
                    {
                        mFsm.ChangeState(State.follow);
                        // 速度归零
                        mRigidbody2D.velocity = Vector2.zero;
                    }
                });
            // 设置初始状态
            mFsm.StartState(State.follow);
        }

        private void Update()
        {
            // 执行状态机
            mFsm.Update();
        }

        private void FixedUpdate()
        {
            // 执行状态机
            mFsm.FixedUpdate();
        }

        public float Health { get; set; } = 30;
        public float Speed { get; set; }


        public void TakeDamage(float damage, float changeDuration = 0.1f)
        {
            Sprite.color = Color.red; // 改变颜色为红色
            Health -= damage; // 减少生命值
            // 显示浮动文字
            FloatingText.Instance.play(damage.ToString(), transform.position);
            ActionKit.Delay(changeDuration, () => Sprite.color = Color.white).Start(this); // 延时后恢复颜色
            CheckHealth();
        }

        public void AdjustAttributes(float multiplier)
        {
            // 调整血量和速度
            Health *= multiplier;
            Speed *= multiplier;
        }

        /// <summary>
        ///     检查血量
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
                Destroy(gameObject);
            }
        }

        // 状态 枚举
        private enum State
        {
            /// <summary>
            ///     追逐
            /// </summary>
            follow,

            /// <summary>
            ///     蓄力
            /// </summary>
            accumulation,

            /// <summary>
            ///     攻击
            /// </summary>
            attack
        }
    }
}