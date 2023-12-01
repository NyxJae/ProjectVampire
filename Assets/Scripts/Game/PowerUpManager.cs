using UnityEngine;
using QFramework;

namespace ProjectVampire
{
    public partial class PowerUpManager : ViewController, ISingleton
    {
        /// <summary>
        /// 私有的 经验掉落几率 属性
        /// </summary>
        [SerializeField]
        private float mDropExpRate = 0.5f;
        /// <summary>
        /// 共开的 经验掉落几率 属性
        /// </summary>
        public float DropExpRate
        {
            get { return mDropExpRate; }
            set { mDropExpRate = value; }
        }
        
        /// <summary>
        /// 私有的 金币掉落几率 属性
        /// </summary>
        [SerializeField]
        private float mDropCoinRate = 0.1f;
        /// <summary>
        /// 共开的 金币掉落几率 属性
        /// </summary>
        public float DropCoinRate
        {
            get { return mDropCoinRate; }
            set { mDropCoinRate = value; }
        }

        public void OnSingletonInit()
        {
        }

        // 公开的 静态 实例 属性
        public static PowerUpManager Instance
        {
            get { return MonoSingletonProperty<PowerUpManager>.Instance; }
        }

        /// <summary>
        /// 公开 掉落奖励方法
        /// </summary>
        /// <param name="gameObject"></param>
        public void DroReward(GameObject gameObject)
        {
            // 随机生成坐标偏移量
            Vector3 offset = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
            // 随机数
            float random = Random.Range(0f, 1f);
            // 如果随机数小于掉落经验几率
            if (random < mDropExpRate)
            {
                // 实例化经验球
                ExpBall.InstantiateWithParent(transform).Position(gameObject.transform.position + offset).Show();

            }
            // 随机生成坐标偏移量
            offset = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
            // 随机数
            random = Random.Range(0f, 1f);
            // 如果随机数小于掉落金币几率
            if (random < mDropCoinRate)
            {
                // 实例化金币
                CoinBall.InstantiateWithParent(transform).Position(gameObject.transform.position + offset).Show();
            }
        }
    }
}