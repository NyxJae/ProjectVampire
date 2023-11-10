using UnityEngine;
using QFramework;

namespace ProjectVampire
{
    public partial class UIController : ViewController
    {
        /// <summary>
        /// 脚本被启用时的回调函数
        /// </summary>
        void Start()
        {
            UIKit.OpenPanel<gamePanel>();

        }

        /// <summary>
        /// 脚本被禁用时的回调函数
        /// </summary>
        private void OnDisable()
        {
            UIKit.ClosePanel<gamePanel>();
        }
    }
}
