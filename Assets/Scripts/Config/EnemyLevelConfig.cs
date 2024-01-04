using System;
using UnityEngine;

// 分组详情类，包含每个组的波次信息和其他设置
[Serializable]
public class GroupDetails
{
    public string groupName; // 组名
    public bool groupEnabled; // 组是否开启
    public WaveDetails[] waves; // 每组里的波次设置
}

// 每个波次的详细设置类
[Serializable]
public class WaveDetails
{
    [Tooltip("波次使用的敌人预制体")] public GameObject enemyPrefab; // 敌人预制体

    [Tooltip("波次持续时间")] public float duration; // 持续时间

    [Tooltip("敌人生成间隔时间")] public float spawnInterval; // 生成间隔

    [Tooltip("敌人属性系数")] public float attributeMultiplier; // 敌人属性系数
}

// 定义一个ScriptableObject类，用于在Unity编辑器中创建和配置敌人波次的分组设置
[CreateAssetMenu(fileName = "EnemyLevelConfig", menuName = "ScriptableObjects/EnemyLevelConfig", order = 1)]
public class EnemyLevelConfig : ScriptableObject
{
    [Tooltip("敌人波次的分组设置")] public GroupDetails[] groups; // 存储所有组的设置
}