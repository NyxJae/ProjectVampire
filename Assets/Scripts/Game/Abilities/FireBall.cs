using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public partial class FireBall : ViewController, IController
    {
        /// <summary>
        ///     私有的 攻击力 属性 在 Inspector 中显示
        /// </summary>
        [SerializeField] private float mAttack = 2f;

        /// <summary>
        ///     攻击力属性
        /// </summary>
        public float Attack
        {
            get => mAttack;
            set => mAttack = value;
        }

        private void Start()
        {
            HitBox.OnTriggerEnter2DEvent(Other =>
            {
                // 发送攻击命令
                this.SendCommand(new AttackEnemyCommand(Other.gameObject, mAttack));
                // 发送DeBuff命令
                this.SendCommand(new DeBuffCommand(Other.gameObject, DeBuffType.Burn));
            });
        }

        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }
    }
}