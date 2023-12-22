using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public partial class SampleAbility : ViewController
    {
        /// <summary>
        ///     私有 攻击间隔 属性
        /// </summary>
        [SerializeField] private float mAttackRate = 1f;

        /// <summary>
        ///     私有 攻击力 属性
        /// </summary>
        [SerializeField] private int mAttack = 1;

        /// <summary>
        ///     私有 计时器 属性
        /// </summary>
        private float mTimer;


        /// <summary>
        ///     公开 攻击间隔 属性
        /// </summary>
        public float AttackRate
        {
            get => mAttackRate;
            set => mAttackRate = value;
        }

        /// <summary>
        ///     公开 攻击力 属性
        /// </summary>
        public int Attack
        {
            get => mAttack;
            set => mAttack = value;
        }


        private void Start()
        {
            AttackRange.OnTriggerStay2DEvent(Other =>
            {
                // 如果计时器大于攻击间隔
                if (mTimer >= mAttackRate)
                    // 确认碰撞的对象的父对象是否有Enemy标签
                    if (Other.transform.parent != null && Other.transform.parent.tag == "Enemy")
                    {
                        // 使用GetComponentInParent来获取父对象上的Enemy组件
                        var enemy = Other.GetComponentInParent<Enemy>();
                        // 如果敌人组件不为空
                        if (enemy != null)
                        {
                            // 播放攻击音效
                            AudioKit.PlaySound("Hit");
                            // 对敌人造成伤害
                            enemy.TakeDamage(mAttack);
                            // 延时重置计时器
                            ActionKit.Sequence()
                                .DelayFrame(1)
                                .Callback(() => mTimer = 0f)
                                .Start(this);
                        }
                    }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Update()
        {
            // 计时器逐帧增加
            mTimer += Time.deltaTime;
        }
    }
}