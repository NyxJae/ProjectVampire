using QFramework;
using UnityEngine.SceneManagement;

namespace ProjectVampire
{
    public partial class UIController : ViewController
    {
        private void Awake()
        {
            // TODO: 重构初始化数据位置
            // 初始化数据
            Global.InitData();
        }

        /// <summary>
        ///     脚本被启用时的回调函数
        /// </summary>
        private void Start()
        {
            if (SceneManager.GetActiveScene().name == "BeginScene")
            {
                UIKit.OpenPanel<UIBeginPanel>();
                UIKit.ClosePanel<UIGamePanel>();
            }
            else if (SceneManager.GetActiveScene().name == "GameScene")
            {
                UIKit.OpenPanel<UIGamePanel>();
                UIKit.ClosePanel<UIBeginPanel>();
            }
        }
    }
}