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
                // 如果碰撞到的是经验球
                if (other.gameObject.CompareTag("ExpBall"))
                {
                    // 日志
                    Debug.Log("捡到经验球");
                    // 给玩家增加经验值
                    Global.Exp.Value += other.GetComponentsInParent<ExpBall>()[0].GetExp();
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
    }
}
