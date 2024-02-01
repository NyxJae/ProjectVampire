using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public partial class TreasureChest : Entity
    {
        protected override Collider2D HitBoxCollider2D => HitBox;

        private void Start()
        {
            HitBox.OnTriggerStay2DEvent(other =>
            {
                // 如果碰撞器的父物体的名字为PickAbility
                if (other.transform.parent.name == "PickAbility")
                {
                    TreasureChestRoot.OpenTreasureChestEvent.Trigger();
                    Destroy(gameObject);
                }
            }).UnRegisterWhenGameObjectDestroyed(this);
        }
    }
}