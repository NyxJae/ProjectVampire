namespace ProjectVampire
{
    public interface IEnemy
    {
        /// <summary>
        ///     血量
        /// </summary>
        float Health { get; set; }

        /// <summary>
        ///     受伤
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="changeDuration"></param>
        public void TakeDamage(float damage, float changeDuration = 0.1f);
    }
}