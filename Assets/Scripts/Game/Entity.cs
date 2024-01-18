using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public abstract class Entity : ViewController
    {
        // 待子类实现的hitbox属性
        protected abstract Collider2D HitBoxCollider2D { get; }

        // 在视野外就把hitbox设置为不可见
        private void OnBecameInvisible()
        {
            HitBoxCollider2D.enabled = false;
        }

        // 在视野内就把hitbox设置为可见
        private void OnBecameVisible()
        {
            HitBoxCollider2D.enabled = true;
        }
    }
}