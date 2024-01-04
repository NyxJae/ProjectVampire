using QFramework;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ProjectVampire
{
    public class EnemyGenerator : ViewController, ISingleton
    {
        [Tooltip("敌人生成最小距离")] public float minSpawnDistance; // 敌人生成的最小距离
        [Tooltip("敌人生成最大距离")] public float maxSpawnDistance; // 敌人生成的最大距离
        [SerializeField] [Tooltip("组配置列表")] private EnemyLevelConfig groupConfig; // 组配置列表

        /// <summary>
        ///     在场地人数
        /// </summary>
        public int EnemyCount;

        /// <summary>
        ///     当前波次的索引
        /// </summary>
        private int currentGroupIndex, currentWaveIndex;

        /// <summary>
        ///     标志位，表示winPanel是否已被打开
        /// </summary>
        private bool isWinPanelOpened;

        /// <summary>
        ///     玩家角色
        /// </summary>
        private GameObject player;

        /// <summary>
        ///     敌人生成的计时器
        /// </summary>
        private float spawnTimer;

        /// <summary>
        ///     波次的计时器
        /// </summary>
        private float waveTimer;

        // 公开的 静态 实例 属性
        public static EnemyGenerator Instance => MonoSingletonProperty<EnemyGenerator>.Instance;

        private void Start()
        {
            // 获取玩家角色实例
            player = Player.Instance.gameObject;
        }

        private void Update()
        {
            // 如果还有未完成的组
            if (currentGroupIndex < groupConfig.groups.Length)
            {
                // 如果当前组是开启状态
                if (groupConfig.groups[currentGroupIndex].groupEnabled)
                    UpdateWave();
                else
                    // 如果当前组被禁用，则跳过这个组
                    currentGroupIndex++;
            }
            else if (!AreEnemiesAlive())
            {
                if (!isWinPanelOpened) // 检查winPanel是否已经被打开
                {
                    // 播放胜利音效
                    AudioKit.PlaySound("GamePass");
                    // 如果所有波次都已完成，且场上没有敌人，游戏胜利
                    UIKit.OpenPanel<UIWinPanel>();
                    // 时间暂停
                    Time.timeScale = 0f;
                    isWinPanelOpened = true; // 设置标志位，表示winPanel已被打开
                }
            }
        }

        public void OnSingletonInit()
        {
        }

        /// <summary>
        ///     更新波次
        /// </summary>
        private void UpdateWave()
        {
            // 更新波次的计时器
            waveTimer += Time.deltaTime;
            // 获取当前组的当前波次的配置
            var currentWave = groupConfig.groups[currentGroupIndex].waves[currentWaveIndex];

            // 如果当前波次的时间未结束
            if (waveTimer < currentWave.duration)
            {
                // 更新敌人生成的计时器
                spawnTimer += Time.deltaTime;

                // 如果到达生成间隔时间
                if (spawnTimer >= currentWave.spawnInterval)
                {
                    // 重置生成计时器
                    spawnTimer = 0f;
                    // 生成敌人
                    GenerateEnemy(currentWave);
                }
            }
            else
            {
                // 波次结束，切换到下一波次
                currentWaveIndex++;
                // 如果当前组的所有波次都结束了
                if (currentWaveIndex >= groupConfig.groups[currentGroupIndex].waves.Length)
                {
                    // 切换到下一组
                    currentGroupIndex++;
                    // 重置波次索引
                    currentWaveIndex = 0;
                }

                waveTimer = 0f;
                spawnTimer = 0f;
            }
        }

        /// <summary>
        ///     生成敌人
        /// </summary>
        /// <param name="waveDetails">波次配置</param>
        private void GenerateEnemy(WaveDetails waveDetails)
        {
            // 获取玩家位置
            var playerPosition = player.transform.position;
            // 随机方向
            var randomDirection = Random.insideUnitCircle.normalized;
            // 随机距离，根据波次配置的最小和最大生成距离
            var randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
            // 计算生成位置
            var randomPosition = playerPosition + new Vector3(randomDirection.x, randomDirection.y, 0) * randomDistance;
            // 生成敌人
            var enemyInstance = waveDetails.enemyPrefab.InstantiateWithParent(transform)
                .Position(randomPosition)
                .Rotation(Quaternion.identity)
                .Show();

            // 应用属性系数
            AdjustEnemyAttributes(enemyInstance, waveDetails.attributeMultiplier);
        }

        /// <summary>
        ///     调整敌人属性
        /// </summary>
        /// <param name="enemy">敌人实例</param>
        /// <param name="multiplier">属性系数</param>
        private void AdjustEnemyAttributes(GameObject enemy, float multiplier)
        {
            var enemyScript = enemy.GetComponent<IEnemy>();
            enemyScript.AdjustAttributes(multiplier);
        }

        /// <summary>
        ///     检测场上是否还有敌人
        /// </summary>
        /// <returns> 如果场上还有敌人，返回 true，否则返回 false </returns>
        private bool AreEnemiesAlive()
        {
            return EnemyCount > 0;
        }
    }
}