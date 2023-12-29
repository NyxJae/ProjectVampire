using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public partial class Magnet : ViewController
    {
        private void Start()
        {
            HitBox.OnTriggerStay2DEvent(other =>
            {
                // 如果碰撞器的父物体的名字包含PickAbility
                if (other.transform.parent.name.Contains("PickAbility"))
                    // 获取 磁铁 方法
                    GetMagnet();
            }).UnRegisterWhenGameObjectDestroyed(this);
        }

        // 公开的 获取磁铁的方法
        public void GetMagnet()
        {
            // 播放音效
            AudioKit.PlaySound("GetAllExp");
            // 获取所有奖励物
            var rewards = GameObject.FindGameObjectsWithTag("RewardBall");
            // 遍历所有奖励物
            foreach (var reward in rewards)
                // 如果奖励物的名字为ExpBall
                if (reward.name.Contains("ExpBall"))
                    // 注册经验球的移动方法
                    ActionKit.OnUpdate.Register(() =>
                    {
                        // 计算奖励物和玩家的距离
                        var distance = Vector3.Distance(reward.transform.position, Player.Instance.transform.position);
                        // 经验球向玩家移动
                        reward.transform.position = Vector3.MoveTowards(reward.transform.position,
                            Player.Instance.transform.position, distance * Time.deltaTime);
                    }).UnRegisterWhenGameObjectDestroyed(reward);

            // 销毁自身
            Destroy(gameObject);
        }
    }
}