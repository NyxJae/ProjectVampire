using UnityEngine;
using QFramework;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using ISingleton = QFramework.ISingleton;

namespace ProjectVampire
{
    public partial class Player : ViewController, ISingleton
    {
        /// <summary>
        /// 私有的 移动速度系数 属性 在 Inspector 中显示
        /// </summary>
        [SerializeField]
        private float mSpeed = 5f;

        ///  <summary>
        /// 私有的 移动速度系数 属性
        /// </summary>
        private Vector2 mMoveInput = Vector2.zero;

        /// <summary>
        /// 公开的 初始血量 属性
        /// </summary>
        [SerializeField]
        private int mHealth = 100;


        /// <summary>
        /// 私有的 血量 属性
        /// </summary>
        public BindableProperty<int> Health = new BindableProperty<int>(100);


        /// <summary>
        /// 私有的 被击扣血值 属性
        /// </summary>
        private int mDamage = 1;





        // 公开的 静态 实例 属性
        public static Player Instance
        {
            get { return MonoSingletonProperty<Player>.Instance; }
        }

        private void Awake()
        {
            // 时间恢复
            Time.timeScale = 1;
        }

        /// <summary>
        /// 角色开始时的回调函数
        /// </summary>
        private void Start()
        {
            // 给HurtBox被触碰时, 触发的事件添加回调函数(受伤),并设置自动销毁
            HurtBox.OnTriggerEnter2DEvent(Collider2D => Health.Value -= mDamage).UnRegisterWhenGameObjectDestroyed(gameObject);
            // 血量初始化
            Health.Value = mHealth;
            // 给血量增加事件添加死亡回调函数
            Health.Register(newValue =>
            {
                if (newValue <= 0)
                {
                    // 播放死亡音效
                    AudioKit.PlaySound("Die");
                    // 显示死亡面板
                    UIKit.OpenPanel<UIEndPanel>();
                    // 时间暂停
                    Time.timeScale = 0;


                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            // 给经验值增加事件添加升级回调函数
            Global.Exp.Register(newValue =>
            {
                if (newValue >= ExpToNextLevel())
                {
                    Global.Level.Value += 1;
                    Global.Exp.Value = 0;
                    // 时间暂停
                    Time.timeScale = 0;
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);


        }

        /// <summary>
        /// 逐帧更新的回调函数
        /// </summary>
        private void Update()
        {



        }



        /// <summary>
        /// 物理引擎的回调函数
        /// </summary>
        private void FixedUpdate()
        {
            // 根据移动速度,速度方向,时间,计算移动距离
            var moveDistance = mMoveInput * (mSpeed * Time.fixedDeltaTime);
            // 移动
            transform.Translate(moveDistance);

        }



        /// <summary>
        /// 公开的 input system 回调函数 OnMove
        ///  </summary>
        public void OnMove(InputAction.CallbackContext context)
        {
            // 获取输入的值
            mMoveInput = context.ReadValue<Vector2>();
        }

        // 计算距离下次升级经验值 方法
        public int ExpToNextLevel()
        {
            return 5 * Global.Level.Value;
        }


        public void OnSingletonInit()
        {

        }
    }
}