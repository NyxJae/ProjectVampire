using QFramework;
using UnityEngine.SceneManagement;

namespace ProjectVampire
{
    public class BeginGameCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            // 重置各项属性
            this.SendCommand(new ResetModelCommand());
            // 重置经验升级系统
            this.GetSystem<ExpUpgradeSystem>().ResetAll();
            // 重新加载场景
            SceneManager.LoadScene("GameScene");
        }
    }
}