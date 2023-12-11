using UnityEngine;
using QFramework;

namespace ProjectVampire
{
    public partial class PowerUpManager : ViewController, ISingleton
    {
        // 公开的 获取实例 方法
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
            if (random <= Global.DropExpRate.Value)
            {
                // 实例化经验球
                ExpBall.InstantiateWithParent(transform).Position(gameObject.transform.position + offset).Show();

            }
            // 随机生成坐标偏移量
            offset = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
            // 随机数
            random = Random.Range(0f, 1f);
            // 如果随机数小于掉落金币几率
            if (random <= Global.DropCoinRate.Value)
            {
                // 实例化金币
                CoinBall.InstantiateWithParent(transform).Position(gameObject.transform.position + offset).Show();
            }
            // 随机生成坐标偏移量
            offset = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
            // 随机数
            random = Random.Range(0f, 1f);
            // 如果随机数小于掉落血瓶几率
            if (random <= Global.DropHPBottleRate.Value)
            {
                // 实例化血瓶
                HPBottle.InstantiateWithParent(transform).Position(gameObject.transform.position + offset).Show();
            }
            // 随机生成坐标偏移量
            offset = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
            // 随机数
            random = Random.Range(0f, 1f);
            // 如果随机数小于掉落炸弹几率
            if (random <= Global.DropBombRate.Value)
            {
                // 实例化炸弹
                Bomb.InstantiateWithParent(transform).Position(gameObject.transform.position + offset).Show();
            }
            // 随机生成坐标偏移量
            offset = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
            // 随机数
            random = Random.Range(0f, 1f);
            // 如果随机数小于掉落吸铁石几率
            if (random <= Global.DropMagnetRate.Value)
            {
                // 实例化吸铁石
                Magnet.InstantiateWithParent(transform).Position(gameObject.transform.position + offset).Show();
            }
        }

        public void OnSingletonInit()
        {
            
        }
    }
}
