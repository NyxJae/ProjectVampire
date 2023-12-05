using UnityEngine;
using QFramework;

namespace ProjectVampire
{
    public partial class ExpBall : ViewController
    {
        /// <summary>
        ///  私有的 经验值 属性
        /// </summary>
        private int mExp = 1;

        /// <summary>
        /// 公开 获取经验值 方法
        /// </summary>
        /// <returns></returns>
        public int GetExp()
        {
            ActionKit.Sequence()
                .Callback(() =>
                {
                    // 飞向玩家
                    transform.position = Vector3.MoveTowards(transform.position, Player.Instance.transform.position, 5f * Time.deltaTime);
                    // 播放音效
                    AudioKit.PlaySound("Exp");
                })
                .Callback(() =>
                {
                    // 销毁自身
                    Destroy(gameObject);
                }).Start(this);
            // 返回经验值
            return mExp;
        }


    }
}
