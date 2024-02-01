using QAssetBundle;
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

        // globalModel
        private PlayerModel GlobalModel => this.GetModel<PlayerModel>();

        protected override void OnExecute()
        {
            if (mOther == null || mAttack <= 0) return;
            var enemy = mOther.GetComponentInParent<IEnemy>();
            if (enemy != null)
            {
                // 获取当前暴击率和暴击倍数
                var isCritical = Random.value < GlobalModel.CriticalRate.Value;
                var criticalMultiplier = GlobalModel.CriticalMultiplier.Value;

                AudioKit.PlaySound(Sfx.HIT);

                // 计算最终伤害
                var finalDamage = isCritical ? mAttack * criticalMultiplier : mAttack;
                enemy.TakeDamage(finalDamage, isCritical); // 传递暴击信息
            }
        }
    }
}