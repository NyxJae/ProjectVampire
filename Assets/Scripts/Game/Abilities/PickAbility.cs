using UnityEngine;
using QFramework;

namespace ProjectVampire
{
    public partial class PickAbility : ViewController
    {
        void Start()
        {
            // 注册碰撞到经验球的回调函数
            PickRange.OnTriggerEnter2DEvent(other =>
            {
                // 如果碰撞到的是奖励球
                if (other.transform.parent.CompareTag("RewardBall"))
                {
                    // 获取经验球的组件
                    var expBalls = other.GetComponentsInParent<ExpBall>();
                    if (expBalls != null && expBalls.Length > 0)
                    {
                        // 日志
                        Debug.Log("碰撞到经验球");
                        // 给玩家增加经验值
                        Global.Exp.Value += expBalls[0].GetExp();
                    }
                    // 获取金币球的组件
                    var coinBalls = other.GetComponentsInParent<CoinBall>();
                    if (coinBalls != null && coinBalls.Length > 0)
                    {
                        // 日志
                        Debug.Log("碰撞到金币球");
                        // 给玩家增加金币
                        Global.Coin.Value += coinBalls[0].GetCoin();
                    }
                }


            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
    }
}
