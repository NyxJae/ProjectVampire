using QFramework;

namespace ProjectVampire
{
    public class ResetModelCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.GetModel<PlayerModel>().Reset();
            this.GetModel<GlobalModel>().Reset();
        }
    }
}