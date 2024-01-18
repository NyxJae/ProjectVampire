using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public partial class ExpBall : Entity
    {
        /// <summary>
        ///     私有的 经验值 属性
        /// </summary>
        private readonly int mExp = 1;


        protected override Collider2D HitBoxCollider2D => HitBox;


        private void Start()
        {
            HitBox.OnTriggerStay2DEvent(other =>
            {
                // 如果碰撞器的父物体的名字为PickAbility
                if (other.transform.parent.name == "PickAbility")
                    // 获取 经验值 方法
                    GetExp();
            }).UnRegisterWhenGameObjectDestroyed(this);
        }


        /// <summary>
        ///     公开 获取经验值 方法
        /// </summary>
        /// <returns></returns>
        public void GetExp()
        {
            // 播放音效
            AudioKit.PlaySound("Exp");
            // 销毁自己
            Destroy(gameObject);
            // 获取经验值
            Global.Exp.Value += mExp;
        }
    }
}