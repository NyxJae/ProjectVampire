using UnityEngine;
using QFramework;

namespace ProjectVampire
{
    public partial class SampleAbilities : ViewController
    {
        /// <summary>
        /// 私有 事件间隔 属性
        /// </summary>
        [SerializeField]
        private float mInterval = 1.5f;

        /// <summary>
        /// 私有 计时器 属性
        /// </summary>
        private float mTimer = 0f;

        // 私有 攻击距离 属性
        [SerializeField]
        private float mAttackDistance = 5f;


        void Start()
        {

        }

        private void Update()
        {
            // 计时器逐帧增加
            mTimer += Time.deltaTime;
            // 如果计时器大于事件间隔
            if (mTimer > mInterval)
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
                        // 修改敌人为红色
                        enemy.Sprite.color = Color.red;
                        // 扣血
                        enemy.Health -= 1;
                        // 延迟 0.1s,如果敌人角色不为空则改回白色
                        ActionKit.Delay(0.1f, () => { if (enemy != null) enemy.Sprite.color = Color.white; }).StartGlobal();

                    }
                }

            }
        }
    }
}
