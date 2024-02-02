namespace ProjectVampire
{
    public static class Config
    {
        // GlobalModel 默认值
        public static float DefaultDropBombRate = 0.01f; // 默认炸弹掉落几率
        public static float DefaultDropCoinRate = 0.01f; // 默认金币掉落几率
        public static float DefaultDropExpRate = 0.05f; // 默认经验掉落几率
        public static float DefaultDropHPBottleRate = 0.01f; // 默认血瓶掉落几率
        public static float DefaultDropMagnetRate = 0.01f; // 默认吸铁石掉落几率

        // PlayerModel 默认值
        public static float DefaultCriticalMultiplier = 1.5f; // 默认暴击伤害倍数
        public static float DefaultCriticalRate = 0.1f; // 默认暴击率
        public static float DefaultExp = 0f; // 默认经验
        public static int DefaultLevel = 1; // 默认等级
        public static float DefaultMaxExp = 10f; // 默认最大经验值
        public static float DefaultMaxHealth = 10f; // 默认最大血量
    }
}