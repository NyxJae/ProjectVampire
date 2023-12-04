using UnityEngine;
using QFramework;

namespace ProjectVampire
{
    public partial class CoinBall : ViewController
    {
        void Start()
        {
            // Code Here
        }
        /// <summary>
        /// 公开 获取金币 方法
        /// </summary>
        /// <returns></returns>
        public int GetCoin()
        {
            ActionKit.Sequence()
                .Callback(() =>
                {
                    // 飞向玩家
                    transform.position = Vector3.MoveTowards(transform.position, Player.Instance.transform.position, 5f * Time.deltaTime);
                    // 播放音效
                    AudioKit.PlaySound("Coin");
                })
                .Callback(() =>
                {
                    // 销毁自身
                    Destroy(gameObject);
                }).Start(this);
            // 返回经验值
            return 1;
        }

    }
}
