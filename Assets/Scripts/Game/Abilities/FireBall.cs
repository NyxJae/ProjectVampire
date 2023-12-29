using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public partial class FireBall : ViewController, IController
    {
        // 转速
        /// <summary>
        ///     私有的 转速 属性 在 Inspector 中显示
        /// </summary>
        [SerializeField] private float mRotateSpeed = 10f;

        // 攻击力
        /// <summary>
        ///     私有的 攻击力 属性 在 Inspector 中显示
        /// </summary>
        [SerializeField] private float mAttack = 2f;

        private void Start()
        {
            HitBox.OnTriggerStay2DEvent(Other =>
            {
                // 发送攻击命令
                this.SendCommand(new AttackEnemyCommand(Other.gameObject, mAttack));
            });
        }

        private void Update()
        {
            // 根据转速,旋转z轴
            transform.Rotate(0, 0, mRotateSpeed * Time.deltaTime);
        }

        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }
    }
}