using System.Linq;
using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public partial class KnifeAbility : ViewController, IController
    {
        [SerializeField] private float mAttack = 2f;
        [SerializeField] private float mAttackRate = 1f;
        [SerializeField] private int mKnifeCount = 1; // 可发射的飞刀数量
        public int KnifePierceCount = 1; // 飞刀可穿透敌人数
        private readonly float mSpeed = 10f; // 飞刀速度
        private float mMaxDistance = 20f; // 飞刀最大飞行距离
        private float mTimer; // 攻击计时器

        public float Attack
        {
            get => mAttack;
            set => mAttack = value;
        }

        public float AttackRate
        {
            get => mAttackRate;
            set => mAttackRate = value;
        }

        public int KnifeCount
        {
            get => mKnifeCount;
            set => mKnifeCount = Mathf.Max(1, value); // 确保至少能发射一个飞刀
        }

        public EnemyGenerator enemyGenerator { get; set; }

        private void Start()
        {
            enemyGenerator = EnemyGenerator.Instance; // 获取敌人生成器实例
        }

        private void Update()
        {
            mTimer += Time.deltaTime; // 计时器累加
            if (mTimer >= mAttackRate) // 攻击间隔检查
            {
                mTimer = 0; // 重置计时器
                var enemyList = enemyGenerator.gameObject.GetComponentsInChildren<Transform>()
                    .Where(child => child.CompareTag("Enemy"))
                    .OrderBy(enemy => Vector3.Distance(enemy.position, transform.position))
                    .ToArray();

                var nearestEnemies = enemyList.Take(mKnifeCount).ToArray(); // 获取最近的敌人

                // 发射飞刀
                foreach (var enemy in nearestEnemies) FireKnife(enemy); // 对每个最近的敌人发射飞刀
            }
        }

        public IArchitecture GetArchitecture()
        {
            return Global.Interface; // 返回全局接口
        }

        private void FireKnife(Transform target)
        {
            Knife.InstantiateWithParent(transform) // 创建飞刀
                .Position(transform.position) // 设置飞刀位置
                .Show() // 显示飞刀
                .Self(self =>
                {
                    Vector2 direction = target.position - transform.position; // 计算目标方向
                    var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 计算旋转角度
                    self.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); // 设置旋转
                    self.Rigidbody2D.velocity = direction.normalized * mSpeed; // 设置速度

                    var piercedCount = 0; // 穿透的敌人数量计数器

                    // 设置触发器，处理穿透逻辑
                    self.HitBox.OnTriggerEnter2DEvent(Other =>
                    {
                        if (!Other.transform.parent.CompareTag("Enemy")) return;
                        this.SendCommand(new AttackEnemyCommand(Other.gameObject, mAttack)); // 攻击敌人

                        piercedCount++; // 穿透数量增加
                        if (piercedCount >= KnifePierceCount) Destroy(self.gameObject); // 如果穿透数量达到上限，销毁飞刀
                    });

                    // 设置自毁时间，以防飞刀没有击中任何目标
                    ActionKit.Delay(2, () => Destroy(self.gameObject));
                });
        }
    }
}