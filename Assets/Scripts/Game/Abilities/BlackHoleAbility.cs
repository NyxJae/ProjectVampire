using System.Collections;
using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public partial class BlackHoleAbility : ViewController
    {
        [Tooltip("黑洞的最大等级")] private const int MaxLevel = 10; // 黑洞的最大等级
        [SerializeField] [Tooltip("黑洞的数量")] private int count = 1; // 黑洞的等级
        [SerializeField] [Tooltip("黑洞的攻击力")] private float attack = 10f; // 黑洞的攻击力
        [SerializeField] [Tooltip("黑洞存在的时长")] private float duration = 5f; // 黑洞存在的时长
        [SerializeField] [Tooltip("黑洞的大小")] private float size = 1f; // 黑洞的大小
        [SerializeField] [Tooltip("黑洞的冷却时间")] private float cooldown = 3f; // 黑洞的冷却时间
        [SerializeField] [Tooltip("黑洞的移动速度")] private float moveSpeed = 1f; // 移动速度

        private float cooldownTimer; // 冷却计时器

        public int Count
        {
            get => count;
            set => count = value;
        }

        public float MoveSpeed
        {
            get => moveSpeed;
            set => moveSpeed = value;
        }

        public float Cooldown
        {
            get => cooldown;
            set => cooldown = value;
        }

        public float Attack
        {
            get => attack;
            set => attack = value;
        }

        public float Size
        {
            get => size;
            set => size = value;
        }

        public float Duration
        {
            get => duration;
            set => duration = value;
        }

        private void Start()
        {
            // 启动黑洞产生的逻辑
            StartCoroutine(SpawnBlackHoles());
        }

        private IEnumerator SpawnBlackHoles()
        {
            while (true) // 无限循环
            {
                // 限制等级不超过最大等级
                var spawnCount = Mathf.Clamp(count, 1, MaxLevel);

                // 产生黑洞
                for (var i = 0; i < spawnCount; i++)
                    BlackHole.Instantiate().Position(transform.position).Parent(null).Show().Self(
                        self =>
                        {
                            // 获取黑洞的组件
                            var blackHoleAbility = self.GetComponent<BlackHole>();
                            // 设置黑洞的属性
                            blackHoleAbility.duration = duration;
                            blackHoleAbility.attack = attack;
                            blackHoleAbility.size = size;
                            blackHoleAbility.moveSpeed = moveSpeed;
                        });

                // 等待冷却时间
                yield return new WaitForSeconds(duration + cooldown);
            }
        }
    }
}