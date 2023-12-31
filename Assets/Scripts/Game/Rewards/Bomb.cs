using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public partial class Bomb : ViewController
    {
        private void Start()
        {
            HitBox.OnTriggerStay2DEvent(other =>
            {
                // 如果碰撞器的父物体的名字为PickAbility
                if (other.transform.parent.name == "PickAbility")
                    // 获取炸弹方法
                    GetBomb();
            }).UnRegisterWhenGameObjectDestroyed(this);
        }

        // 公开的 获取炸弹 方法
        public void GetBomb()
        {
            // 播放音效
            AudioKit.PlaySound("Bomb");
            // 震动摄像机
            MainCamera.Instance.Shake();
            // 找到所有Enemy,不排序,不包含隐藏的
            var enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            // 遍历所有敌人
            foreach (var enemy in enemies)
                // 减少敌人血量
                enemy.TakeDamage(99999);
            // 销毁自身
            Destroy(gameObject);
        }
    }
}