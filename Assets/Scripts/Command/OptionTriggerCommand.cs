using QAssetBundle;
using QFramework;

namespace ProjectVampire
{
    public class OptionTriggerCommand : AbstractCommand
    {
        private readonly ISystemItem item;
        private readonly SaveUtility saveUtility;

        /// <summary>
        ///     选项升级命令
        /// </summary>
        /// <param name="item"> </param>
        /// <param name="saveUtility"></param>
        public OptionTriggerCommand(ISystemItem item, SaveUtility saveUtility)
        {
            this.item = item;
            this.saveUtility = saveUtility;
        }


        protected override void OnExecute()
        {
            item.Trigger();
            item.Save(saveUtility);
            AudioKit.PlaySound(Sfx.ABILITYLEVELUP);
        }
    }
}