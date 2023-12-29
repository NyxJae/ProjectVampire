using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public partial class HPBottle : ViewController
    {
        // 私有的 回复血量 属性
        [SerializeField] private int mRecoverHP = 25;

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


        // 公开的 获取血瓶 方法
        public void GetHPBottle()
        {
            // 播放音效
            AudioKit.PlaySound("Hp");
            Global.Health.Value += mRecoverHP;
            // 如果血量大于最大血量
            if (Global.Health.Value > Global.MaxHealth.Value)
                // 血量等于最大血量
                Global.Health.Value = Global.MaxHealth.Value;
            // 销毁自身
            Destroy(gameObject);
        }
    }
}