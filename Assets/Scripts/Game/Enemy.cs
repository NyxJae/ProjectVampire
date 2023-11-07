using UnityEngine;
using QFramework;
using Unity.VisualScripting;

namespace ProjectVampire
{
    public partial class Enemy : ViewController
    {
        /// <summary>
        /// 私有的 移动速度系数 属性 在 Inspector 中显示
        /// </summary>
        [SerializeField]
        private float mSpeed = 3.0f;

        /// <summary>
        /// 私有的 移动速度方向 属性
        /// </summary>
        private Vector2 mMoveInput = Vector2.zero;

        /// <summary>
        /// 私有的 player 角色
        /// </summary>
        private GameObject player = null;

        private void Start()
        {
            // 获取 player 角色,通过名字查找
            player = GameObject.Find("Player");

        }

        private void Update()
        {
            // 如果 player 为空,则返回
            if (player == null)
            {
                return;
            }
            // 追逐 player
            ChasingPlayrt();
        }


        /// <summary>
        /// 追逐 player
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
    }
}