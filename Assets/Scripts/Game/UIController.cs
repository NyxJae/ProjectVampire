using UnityEngine;
using QFramework;
using UnityEngine.SceneManagement;

namespace ProjectVampire
{
    public partial class UIController : ViewController
    {
        /// <summary>
        /// 脚本被启用时的回调函数
        /// </summary>
        void Start()
        {
            if (SceneManager.GetActiveScene().name == "BeginScene")
            {
                UIKit.OpenPanel<UIBeginPanel>();
            }
            else if (SceneManager.GetActiveScene().name == "GameScene")
            {
                UIKit.OpenPanel<UIGamePanel>();
            }
            // TODO: 重构初始化数据位置
            // 初始化数据
            Global.InitData();

        }

        /// <summary>
        /// 脚本被禁用时的回调函数
        /// </summary>
        private void OnDisable()
        {
            if (SceneManager.GetActiveScene().name == "BeginScene")
            {
                UIKit.ClosePanel<UIBeginPanel>();
            }
            else if (SceneManager.GetActiveScene().name == "GameScene")
            {
                UIKit.ClosePanel<UIGamePanel>();
            }
        }
    }
}
