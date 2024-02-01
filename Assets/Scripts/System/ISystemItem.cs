namespace ProjectVampire
{
    public interface ISystemItem
    {
        // 成就完成
        public void Trigger();
        public void Save(SaveUtility saveUtility);
    }
}