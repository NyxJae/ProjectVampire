using UnityEngine;
using QFramework;

namespace ProjectVampire
{
    public partial class EnemyGenerater : ViewController
    {
        /// <summary>
        /// 私有 生成敌人的时间最短间隔 属性
        /// </summary>
        [SerializeField]
        [Tooltip("生成敌人的时间最短间隔")]
        private float mGenerateInterval = 1f;

        /// <summary>
        /// 私有 生成敌人的时间最长间隔 属性
        /// </summary>
        [SerializeField]
        [Tooltip("生成敌人的时间最长间隔")]
        private float mGenerateIntervalMax = 3f;

        /// <summary>
        /// 私有 敌人生成时间间隔 计时器
        /// </summary>
        private float mGenerateTimer = 0f;


        /// <summary>
        /// 私有的 敌人生成最近距离 属性
        /// </summary>
        [SerializeField]
        [Tooltip("敌人生成最近距离")]
        private float mGenerateDistanceMin = 1f;

        /// <summary>
        /// 私有的 敌人生成最远距离 属性
        /// </summary>
        [SerializeField]
        [Tooltip("敌人生成最远距离")]
        private float mGenerateDistanceMax = 5f;

        /// <summary>
        /// 私有的 Player 角色
        /// </summary>
        private GameObject player = null;

        void Start()
        {
            // 获取 Player
            player = Player.Instance.gameObject;

        }

        private void Update()
        {
            // 计时器逐帧更新
            mGenerateTimer += Time.deltaTime;
            // 如果计时器大于等于生成时间间隔
            if (mGenerateTimer >= mGenerateInterval)
            {
                // 重置计时器
                mGenerateTimer = 0f;
                // 生成敌人
                GenerateEnemy();
            }

        }


        /// <summary>
        /// 生成敌人
        /// </summary>
        private void GenerateEnemy()
        {
            // 获取 Player 的位置
            var playerPosition = player.transform.position;

            // 随机方向
            var randomDirection = Random.insideUnitCircle.normalized;
            // 随机距离
            var randomDistance = Random.Range(mGenerateDistanceMin, mGenerateDistanceMax);
            // 计算生成位置
            var randomPosition = playerPosition + new Vector3(randomDirection.x, randomDirection.y, 0) * randomDistance;

            // 生成敌人
            Enemy.Instantiate().Position(randomPosition).Show();
        }

    }
}
