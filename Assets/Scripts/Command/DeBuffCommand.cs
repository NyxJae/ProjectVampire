using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public class DeBuffCommand : AbstractCommand
    {
        //执行者
        private readonly GameObject mAbility;

        // 被执行的DeBuff
        private readonly DeBuffType mDeBuffType;

        // 被执行的对象
        private readonly GameObject mEnemy;

        //重写构造函数
        public DeBuffCommand(GameObject enemy, DeBuffType deBuffType, GameObject ability = null)
        {
            mEnemy = enemy.transform.parent.gameObject;
            mDeBuffType = deBuffType;
            mAbility = ability;
        }

        protected override void OnExecute()
        {
            if (!mEnemy.CompareTag("Enemy")) return;
            // 根据debuff类型应用不同的效果
            ViewController enemy = null;
            Vector3 enemyPos;
            switch (mDeBuffType)
            {
                case DeBuffType.None:
                    break;
                case DeBuffType.Slow:
                    // 减速效果，减少一半的速度 1秒后恢复
                    mEnemy.GetComponent<IEnemy>().Speed /= 2;
                    enemy = mEnemy.GetComponent<ViewController>();
                    ActionKit.Delay(1f, () => mEnemy.GetComponent<IEnemy>().Speed *= 2).Start(enemy);
                    break;
                case DeBuffType.WeakAttack:
                    // 攻击力减弱，减少一半的攻击力 1秒后恢复
                    mEnemy.GetComponent<IEnemy>().Attack /= 2;
                    enemy = mEnemy.GetComponent<ViewController>();
                    ActionKit.Delay(1f, () => mEnemy.GetComponent<IEnemy>().Attack *= 2).Start(enemy);
                    break;
                case DeBuffType.KnockBack:
                    // 击退效果，向后击退一段距离
                    enemyPos = mEnemy.transform.position;
                    var knockBackDirection =
                        (enemyPos - Player.Instance.transform.position).normalized;
                    var knockBackDistance = 2f; // 设定一个击退距离
                    enemyPos += knockBackDirection * knockBackDistance; // 计算新的位置
                    // 移动到吸引的位置
                    mEnemy.transform.position = enemyPos; // 如果没有Rigidbody2D组件，直接设置transform的位置
                    break;
                case DeBuffType.Burn:
                    // 灼烧效果
                    enemy = mEnemy.GetComponent<ViewController>();
                    ActionKit.Repeat(5)
                        .Condition(() => enemy != null)
                        .Callback(() => { this.SendCommand(new AttackEnemyCommand(mEnemy, 0.5f)); })
                        .Delay(0.5f)
                        .Start(enemy);
                    break;
                case DeBuffType.Attract:
                    // 吸引效果
                    ActionKit.Repeat(60)
                        .Condition(() => mEnemy != null)
                        .Callback(() => { mEnemy.transform.position = mAbility.transform.position; })
                        .DelayFrame(1)
                        .Start(mAbility.GetComponent<ViewController>());
                    break;
            }
        }
    }
}