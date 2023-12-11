using UnityEngine;
using QFramework;

namespace ProjectVampire
{
    public partial class CoinBall : ViewController
    {
        private void Start()
        {
            HitBox.OnTriggerStay2DEvent(other =>
            {
                // 如果碰撞器的父物体的名字为PickAbility
                if (other.transform.parent.name == "PickAbility")
                {
                    // 获取 金币 方法
                    GetCoin();
                }
				
            }).UnRegisterWhenGameObjectDestroyed(this);
        }
        /// <summary>
        /// 公开 获取金币 方法
        /// </summary>
        /// <returns></returns>
        public void GetCoin()
        { 
            // 播放音效
            AudioKit.PlaySound("Coin");
            Destroy(gameObject);
            Global.Coin.Value += 1;
        }

    }
}
