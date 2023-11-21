using UnityEngine;
using QFramework;

namespace ProjectVampire
{
    public partial class SampleAbility : ViewController
    {

        /// <summary>
        /// 私有 计时器 属性
        /// </summary>
        private float mTimer = 0f;

        // 私有 攻击距离 属性
        [SerializeField]
        private float mAttackDistance = 1.5f;

        // 私有 攻击间隔 属性
        [SerializeField]
        private float mAttackRate = 0.5f;
        // 公开 攻击间隔 属性
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


        void Start()
        {

        }

        private void Update()
        {
            // 计时器逐帧增加
            mTimer += Time.deltaTime;
            // 如果计时器大于事件间隔
            if (mTimer > mAttackRate)
            {
                // 重置计时器
                mTimer = 0f;
                // 根据 标签 获取所有的敌人
                var enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
                // 遍历所有的敌人
                foreach (var enemy in enemies)
                {

                    // 计算敌人与自己的距离
                    var distance = Vector3.Distance(transform.position, enemy.transform.position);
                    // 如果距离小于 攻击距离
                    if (distance < mAttackDistance)
                    {
                        enemy.TakeDamage(mAttack); // 对敌人造成伤害
                    }

                }

            }
        }
    }
}
