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
        /// 私有的 血量 属性
        /// </summary>
        [SerializeField]
        private int mHealth = 100;
        /// <summary>
        /// 公开的 血量 属性
        /// </summary>
        public int Health
        {
            get { return mHealth; }
            set { mHealth = value; }
        }

        /// <summary>
        /// 私有的 被击扣血值 属性
        /// </summary>
        private int mDamage = 1;

        /// <summary>
        /// 私有的 当前经验值上限 属性
        /// </summary>
        [SerializeField]
        private int mExpValueMax = 2;

        /// <summary>
        /// 公开的 当前经验值上限 属性
        /// </summary>
        public int ExpValueMax
        {
            get { return mExpValueMax; }
            set { mExpValueMax = value; }
        }


        // 公开的 静态 实例 属性
        public static Player Instance
        {
            get { return MonoSingletonProperty<Player>.Instance; }
        }



        /// <summary>
        /// 角色开始时的回调函数
        /// </summary>
        private void Start()
        {
            // 给HurtBox被触碰时, 触发的事件添加回调函数(受伤),并设置自动销毁
            HurtBox.OnTriggerEnter2DEvent(Collider2D => OnDamage(mDamage)).UnRegisterWhenGameObjectDestroyed(gameObject);
            // 给经验值增加事件添加升级回调函数
            Global.Exp.RegisterWithInitValue(newVlaue =>
            {
                // 升级
                if (newVlaue >= mExpValueMax)
                {
                    Global.Level.Value += 1;
                    Global.Exp.Value = 0;
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

        // 私有 主角受伤 方法
        private void OnDamage(int damage)
        {
            // 血量减少
            mHealth -= damage;
            // 如果血量小于等于0
            if (mHealth <= 0)
            {
                // 角色死亡
                this.DestroyGameObjGracefully();
                // 打开结束界面
                UIKit.OpenPanel<endPanel>();
            }
        }

        public void OnSingletonInit()
        {

        }
    }
}