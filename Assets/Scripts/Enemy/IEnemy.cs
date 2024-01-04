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
        ///     受伤
        /// </summary>
        /// <param name="damage">伤害值</param>
        /// <param name="changeDuration">改变持续时间</param>
        void TakeDamage(float damage, float changeDuration = 0.1f);

        /// <summary>
        ///     调整敌人属性
        /// </summary>
        /// <param name="multiplier">属性调整系数</param>
        void AdjustAttributes(float multiplier);
    }
}