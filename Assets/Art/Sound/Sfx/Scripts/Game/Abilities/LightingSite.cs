using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public partial class LightingSite : ViewController, IController
    {
        /// <summary>
        ///     私有 攻击间隔 属性
        /// </summary>
        [SerializeField] private float mAttackRate = 1f;

        /// <summary>
        ///     私有 攻击力 属性
        /// </summary>
        [SerializeField] private float mAttack = 1;

        /// <summary>
        /// </summary>
        private readonly Queue<GameObject> attackQueue = new();

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
        public float Attack
        {
            get => mAttack;
            set => mAttack = value;
        }

        private void Start()
        {
            AttackRange.OnTriggerEnter2DEvent(Other =>
            {
                //如果计时器大于攻击间隔期间,将碰到的对象压入队列,计时器归零后,从队列中取出所有对象,发送攻击命令
                attackQueue.Enqueue(Other.gameObject);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Update()
        {
            // 计时器逐帧增加
            mTimer += Time.deltaTime;
            // 如果计时器大于攻击间隔期间
            if (mTimer > mAttackRate)
            {
                // 如果队列中有对象
                if (attackQueue.Count > 0)
                    // 从队列中取出所有对象
                    while (attackQueue.Count > 0)
                        // 发送攻击命令
                        this.SendCommand(new AttackEnemyCommand(attackQueue.Dequeue(), mAttack));

                // 计时器归零
                mTimer = 0;
            }
        }

        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }
    }
}