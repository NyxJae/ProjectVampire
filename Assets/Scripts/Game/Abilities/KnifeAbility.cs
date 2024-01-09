using System.Linq;
using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public partial class KnifeAbility : ViewController, IController
    {
        // 新增属性：可发射的飞刀数量
        [SerializeField] private float mAttack = 2f;

        [SerializeField] private float mAttackRate = 1f;

        // 新增属性：可发射的飞刀数量
        [SerializeField] private int mKnifeCount = 1;
        private readonly float mSpeed = 10f;
        private float mMaxDistance = 20f;
        private float mTimer;

        // 获取和设置飞刀攻击力
        public float Attack
        {
            get => mAttack;
            set => mAttack = value;
        }

        // 获取和设置飞刀的攻击间隔
        public float AttackRate
        {
            get => mAttackRate;
            set => mAttackRate = value;
        }

        // 获取和设置可以发射的飞刀数量
        public int KnifeCount
        {
            get => mKnifeCount;
            set => mKnifeCount = Mathf.Max(1, value); // 确保至少发射一个飞刀
        }

        public EnemyGenerator enemyGenerator { get; set; }

        private void Start()
        {
            // 获取敌人生成器
            enemyGenerator = EnemyGenerator.Instance;
        }

        private void Update()
        {
            // 更新计时器
            mTimer += Time.deltaTime;
            // 如果计时器大于攻击间隔
            if (mTimer >= mAttackRate)
            {
                // 重置计时器
                mTimer = 0;
                // 获取所有敌人，并按距离排序
                var enemyList = enemyGenerator.gameObject.GetComponentsInChildren<Transform>()
                    .Where(child => child.CompareTag("Enemy"))
                    .OrderBy(enemy => Vector3.Distance(enemy.position, transform.position))
                    .ToArray();

                // 获取最近的几个敌人，数量由mKnifeCount决定
                var nearestEnemies = enemyList.Take(mKnifeCount).ToArray();

                // 对每个最近的敌人发射一个飞刀
                foreach (var enemy in nearestEnemies)
                    // 对每个最近的敌人发射一个飞刀
                    FireKnife(enemy);
            }
        }

        public IArchitecture GetArchitecture()
        {
            // 返回全局接口
            return Global.Interface;
        }

        private void FireKnife(Transform target)
        {
            // 创建一个飞刀，并设置其父物体为transform
            Knife.InstantiateWithParent(transform)
                // 设置飞刀的位置为transform的位置
                .Position(transform.position)
                // 显示飞刀
                .Show()
                // 设置自毁函数
                .Self(self =>
                {
                    // 计算飞刀的目标方向
                    Vector2 direction = target.position - transform.position;
                    // 计算飞刀的旋转角度
                    var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    // 设置飞刀的旋转角度
                    self.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    // 设置飞刀的速度
                    self.Rigidbody2D.velocity = direction.normalized * mSpeed;
                    // 设置触发器
                    self.HitBox.OnTriggerEnter2DEvent(Other =>
                    {
                        // 如果触发器不是敌人，则返回
                        if (!Other.transform.parent.CompareTag("Enemy")) return;
                        // 向敌人发送攻击命令
                        this.SendCommand(new AttackEnemyCommand(Other.gameObject, mAttack));
                        // 销毁飞刀
                        Destroy(self.gameObject);
                    });
                    // 设置自毁时间
                    ActionKit.Delay(2, () => Destroy(self.gameObject));
                });
        }
    }
}