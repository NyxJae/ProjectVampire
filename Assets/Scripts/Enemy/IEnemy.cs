namespace ProjectVampire
{
    public interface IEnemy
    {
        /// <summary>
        ///     血量
        /// </summary>
        float Health { get; set; }

        /// <summary>
        ///     速度
        /// </summary>
        float Speed { get; set; }

        /// <summary>
        ///     攻击力
        /// </summary>
        float Attack { get; set; }

        /// <summary>
        ///     受伤
        /// </summary>
        /// <param name="damage">伤害值</param>
        void TakeDamage(float damage);

        /// <summary>
        ///     调整敌人属性
        /// </summary>
        /// <param name="healthMultiplier">生命系数</param>
        /// <param name="speedMultiplier">速度系数</param>
        void AdjustAttributes(float healthMultiplier, float speedMultiplier);
    }
}