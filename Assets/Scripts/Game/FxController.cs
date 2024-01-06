using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public partial class FxController : ViewController, ISingleton
    {
        // 公开的 静态 实例 属性
        public static FxController Instance => MonoSingletonProperty<FxController>.Instance;

        public void OnSingletonInit()
        {
        }

        // 公开的 Play()方法 参数为 sprite
        public void Play(SpriteRenderer sprite)
        {
            EnemyDieFx.Instantiate()
                .Position(sprite.Position())
                .LocalScale(sprite.Scale())
                .Parent(this)
                .Self(self => { self.sprite = sprite.sprite; })
                .Show();
        }
    }
}