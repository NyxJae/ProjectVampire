using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public partial class FloatingText : ViewController, ISingleton
    {
        /// <summary>
        ///     公开的 静态 实例 属性
        /// </summary>
        public static FloatingText Instance => MonoSingletonProperty<FloatingText>.Instance;

        private void Start()
        {
            Text.Hide();
        }


        public void OnSingletonInit()
        {
        }

        /// <summary>
        ///     公开的 显示浮动文字 方法
        /// </summary>
        /// <param name="text">要显示的文字</param>
        /// <param name="position">应显示的位置</param>
        public void Play(string text, Vector3 position)
        {
            //TODO:实例化并显示浮动文字,设置父物体,设置文字与位置,并设置向上平滑移动动画与平滑放大动画.最后平滑渐隐,并销毁自身
            // 实例化浮动文字
            Text.InstantiateWithParent(this)
                .Self(t =>
                {
                    // 设置文字
                    t.text = text;
                    // 设置动画队列
                    ActionKit.Sequence()
                        .Lerp(0, 1, 1f, p =>
                        {
                            t.PositionX(position.x)
                                .PositionY(position.y + p);
                        })
                        .Delay(0.5f)
                        .Lerp(1, 0, 0.5f, p => { t.alpha = p; }, () => { t.DestroySelf(); })
                        .Start(t);
                })
                .Show();
        }
    }
}