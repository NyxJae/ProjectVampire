using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public class AttackEnemyCommand : AbstractCommand
    {
        // 攻击力
        private readonly float mAttack;

        // 碰撞器
        private readonly GameObject mOther;

        // 重写构造函数
        public AttackEnemyCommand(GameObject other, float attack)
        {
            mOther = other;
            mAttack = attack;
        }

        protected override void OnExecute()
        {
            // 确认碰撞的对象的父对象是否有Enemy标签
            if (mOther == null) return;
            if (!mOther.transform.parent.CompareTag("Enemy")) return;
            // 使用GetComponentInParent来获取父对象上的Enemy组件
            var enemy = mOther.GetComponentInParent<IEnemy>();
            // 如果敌人组件不为空
            if (enemy != null)
            {
                // 播放攻击音效
                AudioKit.PlaySound("Hit");
                // 对敌人造成伤害
                enemy.TakeDamage(mAttack);
            }
        }
    }
}