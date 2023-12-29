using System.Linq;
using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    // todo: 飞刀能力 根据指定间隔时间 向最近的敌人发射飞刀 打到敌人 飞刀消失 飞的太远也消失 
    public partial class KnifeAbility : ViewController, IController
    {
        // 飞刀的攻击力
        [SerializeField] private float mAttack = 2f;

        // 飞刀的攻击间隔
        [SerializeField] private float mAttackRate = 1f;

        // 飞刀的飞行速度
        private readonly float mSpeed = 10f;

        // 飞刀的最远飞行距离
        private float mMaxDistance = 20f;

        // 时间计时器
        private float mTimer;

        public EnemyGenerator enemyGenerator { get; set; }

        private void Start()
        {
            // 获取敌人生成器
            enemyGenerator = EnemyGenerator.Instance;
        }


        private void Update()
        {
            // 计时器逐帧增加
            mTimer += Time.deltaTime;
            // 如果计时器大于攻击间隔期间
            if (mTimer >= mAttackRate)
            {
                // 重置计时器
                mTimer = 0;
                // 获取敌人生成器的所有子对象,tag为Enemy的
                var enemyList = enemyGenerator.gameObject.GetComponentsInChildren<Transform>()
                    .Where(child => child.CompareTag("Enemy"))
                    .ToArray();
                // 如果敌人列表不为空
                if (enemyList.Length > 0)
                {
                    // 根据敌人的位置,排序
                    enemyList = enemyList
                        .OrderBy(enemy => Vector3.Distance(enemy.position, transform.position))
                        .ToArray();
                    // 获取最近的敌人
                    var nearestEnemy = enemyList[0];
                    // 实例化飞刀
                    Knife.InstantiateWithParent(transform)
                        .Position(transform.position)
                        .Show()
                        .Self(self =>
                        {
                            // 旋转z轴 使飞刀朝向敌人
                            Vector2 direction = nearestEnemy.position - transform.position;
                            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                            self.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                            // 向最近的敌人发射飞刀,设置飞刀的飞行速度
                            self.Rigidbody2D.velocity =
                                direction.normalized * mSpeed;
                            self.HitBox.OnTriggerEnter2DEvent(Other =>
                            {
                                // 如果碰到的是敌人
                                if (!Other.transform.parent.CompareTag("Enemy")) return; // 发送攻击命令
                                this.SendCommand(new AttackEnemyCommand(Other.gameObject, mAttack));
                                // 销毁飞刀
                                Destroy(self.gameObject);
                            });
                            // 延时3秒后销毁飞刀
                            ActionKit.Delay(2, () => Destroy(self.gameObject));
                        });
                }
            }
        }

        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }
    }
}