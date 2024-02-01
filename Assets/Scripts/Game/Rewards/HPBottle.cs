using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public partial class HPBottle : Entity, IController
    {
        // 私有的 回复血量 属性
        [SerializeField] private int mRecoverHP = 25;

        protected override Collider2D HitBoxCollider2D => HitBox;

        // PlayerModel
        private PlayerModel PlayerModel => this.GetModel<PlayerModel>();

        private void Start()
        {
            HitBox.OnTriggerStay2DEvent(other =>
            {
                // 如果碰撞器的父物体的名字为PickAbility
                if (other.transform.parent.name == "PickAbility")
                    // 获取血瓶方法
                    GetHPBottle();
            }).UnRegisterWhenGameObjectDestroyed(this);
        }

        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }


        // 公开的 获取血瓶 方法
        public void GetHPBottle()
        {
            // 播放音效
            AudioKit.PlaySound("Hp");
            // 如果血量大于最大血量
            if (PlayerModel.Health.Value < PlayerModel.MaxHealth.Value)
            {
                PlayerModel.Health.Value += mRecoverHP;
                // 如果血量大于最大血量
                if (PlayerModel.Health.Value > PlayerModel.MaxHealth.Value)
                    // 血量等于最大血量
                    PlayerModel.Health.Value = PlayerModel.MaxHealth.Value;
                // 销毁自身
                Destroy(gameObject);
            }
        }
    }
}