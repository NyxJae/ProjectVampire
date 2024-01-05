using QFramework;
using UnityEngine;
// 使用QFramework框架
// 使用Unity引擎的命名空间
using Random = UnityEngine.Random; // 使用UnityEngine下的Random类来避免与其他命名空间下的Random类冲突

// 定义命名空间ProjectVampire
namespace ProjectVampire
{
    // 敌人生成器类，继承自ViewController并实现ISingleton接口确保为单例
    public class EnemyGenerator : ViewController, ISingleton
    {
        [Tooltip("敌人生成最小距离")] public float minSpawnDistance; // 表示敌人生成时与玩家的最小距离
        [Tooltip("敌人生成最大距离")] public float maxSpawnDistance; // 表示敌人生成时与玩家的最大距离

        [SerializeField] [Tooltip("敌人配置")] private EnemyLevelConfig levelConfig; // 敌人波次配置文件的引用

        public int enemyCount; // 表示场上当前的敌人数量

        private int _currentGroupIndex; // 当前正在处理的组索引
        private int _currentWaveIndex; // 当前正在处理的波次索引
        private bool _isWinPanelOpened; // 是否已经打开胜利面板的标志

        private GameObject _player; // 玩家对象的引用
        private float _spawnTimer; // 敌人生成的计时器
        private float _waveTimer; // 波次的计时器

        // 单例模式的静态实例访问器
        public static EnemyGenerator Instance => MonoSingletonProperty<EnemyGenerator>.Instance;

        // 当对象启用时，Start方法会被调用一次
        private void Start()
        {
            _player = Player.Instance.gameObject; // 获取并存储玩家对象的引用
        }

        // 每一帧调用一次Update方法
        private void Update()
        {
            // 如果当前组索引未超过组的总数
            if (_currentGroupIndex < levelConfig.groups.Length)
            {
                // 如果当前波次索引未超过当前组波次的总数
                if (_currentWaveIndex < levelConfig.groups[_currentGroupIndex].waves.Length)
                {
                    UpdateWave(); // 更新波次
                }
                else
                {
                    // 当前组的波次已完成，开始下一个组
                    _currentGroupIndex++; // 组索引增加
                    _currentWaveIndex = 0; // 波次索引重置
                }
            }
            // 如果所有组的波次都已完成，并且场上没有敌人
            else if (!AreEnemiesAlive() && !_isWinPanelOpened)
            {
                AudioKit.PlaySound("GamePass"); // 播放胜利音效
                UIKit.OpenPanel<UIWinPanel>(); // 打开胜利面板
                Time.timeScale = 0f; // 游戏时间暂停
                _isWinPanelOpened = true; // 设置胜利面板已打开的标志
            }
        }

        // 单例初始化方法，当单例被创建时调用
        public void OnSingletonInit()
        {
            // 用于初始化单例，此处暂无代码
        }

        // 更新波次的方法
        private void UpdateWave()
        {
            _waveTimer += Time.deltaTime; // 波次计时器累加时间

            // 确保当前组索引在有效范围内
            if (_currentGroupIndex < levelConfig.groups.Length)
            {
                var currentWave = levelConfig.groups[_currentGroupIndex].waves[_currentWaveIndex]; // 获取当前波次的配置信息

                // 如果当前波次的时间未结束，并且波次是激活状态
                if (_waveTimer < currentWave.duration && currentWave.waveEnabled)
                {
                    _spawnTimer += Time.deltaTime; // 敌人生成计时器累加时间

                    // 如果达到了敌人生成间隔时间
                    if (_spawnTimer >= currentWave.spawnInterval)
                    {
                        _spawnTimer = 0f; // 重置敌人生成计时器
                        GenerateEnemy(currentWave); // 生成敌人
                    }
                }
                else
                {
                    _currentWaveIndex++; // 波次索引增加，准备进入下一波次
                    _waveTimer = 0f; // 波次计时器重置
                    _spawnTimer = 0f; // 敌人生成计时器重置

                    // 如果当前组的所有波次都已完成
                    if (_currentWaveIndex >= levelConfig.groups[_currentGroupIndex].waves.Length)
                    {
                        _currentGroupIndex++; // 组索引增加，准备进入下一组
                        _currentWaveIndex = 0; // 波次索引重置
                    }
                }
            }
        }

        // 生成敌人的方法
        private void GenerateEnemy(WaveDetails waveDetails)
        {
            var playerPosition = _player.transform.position; // 获取玩家位置
            var randomDirection = Random.insideUnitCircle.normalized; // 随机生成一个方向
            var randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance); // 随机生成一个距离
            var randomPosition =
                playerPosition + new Vector3(randomDirection.x, randomDirection.y) * randomDistance; // 计算生成位置
            var enemyInstance = Instantiate(waveDetails.enemyPrefab, randomPosition, Quaternion.identity); // 实例化敌人预制体
            AdjustEnemyAttributes(enemyInstance, waveDetails.healthMultiplier, waveDetails.speedMultiplier); // 调整敌人的属性
            enemyInstance.transform.SetParent(transform); // 设置敌人的父对象
            enemyInstance.SetActive(true); // 激活敌人对象
            enemyCount++; // 敌人数量增加
        }

        // 调整敌人属性的方法
        private void AdjustEnemyAttributes(GameObject enemy, float healthMultiplier, float speedMultiplier)
        {
            var enemyScript = enemy.GetComponent<IEnemy>(); // 获取敌人的接口脚本
            enemyScript.AdjustAttributes(healthMultiplier, speedMultiplier); // 调整敌人属性
        }

        // 检查场上是否还有敌人的方法
        private bool AreEnemiesAlive()
        {
            return enemyCount > 0; // 如果敌人数量大于0，则返回true，表示还有敌人存活
        }
    }
}