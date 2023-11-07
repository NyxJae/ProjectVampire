using UnityEngine;
using QFramework;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

namespace ProjectVampire
{
    public partial class Player : ViewController
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
        /// 角色开始时的回调函数
        /// </summary>
        private void Start()
        {
            // 给HurtBox被触碰时, 触发的事件添加回调函数,并设置自动销毁
            HurtBox.OnTriggerEnter2DEvent(Collider2D => OnDamage(10)).UnRegisterWhenGameObjectDestroyed(gameObject);
        }


        /// <summary>
        /// 逐帧更新的回调函数
        /// </summary>
        private void Update()
        {



        }

        /// <summary>
        /// 绘制GUI的回调函数
        /// </summary>
        private void OnGUI()
        {
            // 显示血量
            GUI.Label(new Rect(0, 0, 100, 100), "Health:" + mHealth);
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
            }
        }

    }
}