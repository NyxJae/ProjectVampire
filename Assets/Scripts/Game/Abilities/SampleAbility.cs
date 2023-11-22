using UnityEngine;
using QFramework;
using System;

namespace ProjectVampire
{
    public partial class SampleAbility : ViewController
    {

        /// <summary>
        /// 私有 计时器 属性
        /// </summary>
        private float mTimer = 0f;

        /// <summary>
        /// 私有 攻击距离 属性
        /// </summary>
        [SerializeField]
        private float mAttackDistance = 1.5f;
        /// <summary>
        /// 公开 攻击距离 属性
        /// </summary>
        public float AttackDistance
        {
            get { return mAttackDistance; }
            set
            {
                mAttackDistance = value;
                UpdateAttackTriggerSize(); // 当攻击距离变化时更新触发器大小
            }
        }

        private void UpdateAttackTriggerSize()
        {
            if (AttackRange != null)
            {
                AttackRange.radius = mAttackDistance; // 设置碰撞器的半径与攻击距离相等
            }
        }

        /// <summary>
        /// 私有 攻击间隔 属性
        /// </summary>
        [SerializeField]
        private float mAttackRate = 0.5f;
        /// <summary>
        /// 公开 攻击间隔 属性
        /// </summary>
        public float AttackRate
        {
            get { return mAttackRate; }
            set { mAttackRate = value; }
        }

        /// <summary>
        /// 私有 攻击力 属性
        /// </summary>
        [SerializeField]
        private int mAttack = 1;
        /// <summary>
        /// 公开 攻击力 属性
        /// </summary>
        public int Attack
        {
            get { return mAttack; }
            set { mAttack = value; }
        }

        //攻击状态 控制变量
        private bool canAttack = true; // 默认情况下可以攻击


        void Start()
        {
            // 初始化攻击触发器大小
            UpdateAttackTriggerSize();
            AttackRange.OnTriggerStay2DEvent(Other =>
            {
                // 如果计时器大于攻击间隔
                if (mTimer > mAttackRate)
                {
                    // 确认碰撞的对象的父对象或自身有Enemy标签
                    if (Other.gameObject.CompareTag("Enemy") || Other.transform.parent.CompareTag("Enemy"))
                    {
                        // 使用GetComponentInParent来获取父对象上的Enemy组件
                        var enemy = Other.GetComponentInParent<Enemy>();
                        // 如果敌人组件不为空
                        if (enemy != null)
                        {
                            // 对敌人造成伤害
                            enemy.TakeDamage(mAttack);
                        }
                    }
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Update()
        {
            // 计时器逐帧增加
            mTimer += Time.deltaTime;
            // 如果计时器大于攻击间隔
            if (mTimer > mAttackRate)
            {
                // 可以攻击
                canAttack = true;
                // 下一帧开始时,计时器归零
                ActionKit.DelayFrame(1, () =>
                {
                    mTimer = 0f;
                    canAttack = false;
                }).Start(this);
            }
        }


    }
}
