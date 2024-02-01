using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public partial class PowerUpManager : ViewController, ISingleton, IController
    {
        // 增加一个计数器追踪场上的炸弹数量
        private int bombCount;

        // 公开的 获取实例 方法
        public static PowerUpManager Instance => MonoSingletonProperty<PowerUpManager>.Instance;

        // globalModel
        private GlobalModel GlobalModel => this.GetModel<GlobalModel>();

        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }

        public void OnSingletonInit()
        {
            // Nothing needed here
        }

        /// <summary>
        ///     公开 掉落宝箱方法
        /// </summary>
        /// <param name="enemy"></param>
        public void DropTreasureChest(GameObject enemy)
        {
            TreasureChest.InstantiateWithParent(transform).Position(enemy.transform.position + GetRandomOffset())
                .Show(); // 实例化宝箱
        }

        /// <summary>
        ///     公开 掉落奖励方法
        /// </summary>
        /// <param name="gameObject"></param>
        public void DroReward(GameObject enemy)
        {
            // 以下方法抽象为一个新的方法 DropItem
            DropItem(ExpBall, GlobalModel.DropExpRate.Value, enemy);
            DropItem(CoinBall, GlobalModel.DropCoinRate.Value, enemy);
            DropItem(HPBottle, GlobalModel.DropHPBottleRate.Value, enemy);
            DropItem(Magnet, GlobalModel.DropMagnetRate.Value, enemy);

            // 使用bombCount来控制炸弹的生成
            if (bombCount < 2)
            {
                var random = Random.Range(0f, 1f);
                if (random <= GlobalModel.DropBombRate.Value)
                {
                    Bomb.InstantiateWithParent(transform).Position(enemy.transform.position + GetRandomOffset()).Show();
                    bombCount++; // 炸弹数量增加
                }
            }
        }

        // 抽象生成物品的方法来避免代码重复
        private void DropItem(Transform reward, float dropRate, GameObject enemy)
        {
            var random = Random.Range(0f, 1f);
            if (random <= dropRate)
                reward.InstantiateWithParent(transform).Position(enemy.transform.position + GetRandomOffset()).Show();
        }

        // 抽象随机坐标偏移量的生成
        private Vector3 GetRandomOffset()
        {
            return new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
        }

        // 公开的方法，用来减少计数器的值
        public void DecreaseBombCount()
        {
            if (bombCount > 0)
                bombCount--;
        }
    }
}