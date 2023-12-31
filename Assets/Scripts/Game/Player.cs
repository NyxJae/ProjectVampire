using QFramework;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectVampire
{
    public partial class Player : ViewController, ISingleton
    {
        /// <summary>
        ///     私有的 移动速度系数 属性 在 Inspector 中显示
        /// </summary>
        [SerializeField] private float mSpeed = 5f;

        /// <summary>
        ///     私有的 被击扣血值 属性
        /// </summary>
        private readonly int mDamage = 1;

        /// <summary>
        ///     平滑时间，决定了移动到目标位置所需的时间，可以根据需要调整这个值
        /// </summary>
        private readonly float smoothTime = 0.5f;

        /// <summary>
        ///     私有的 移动速度系数 属性
        /// </summary>
        private Vector2 mMoveInput = Vector2.zero;

        /// <summary>
        ///     用于存储速度的变量，用于SmoothDamp方法
        /// </summary>
        private Vector3 velocity = Vector3.zero;


        // 公开的 静态 实例 属性
        public static Player Instance => MonoSingletonProperty<Player>.Instance;

        private void Awake()
        {
            // 时间恢复
            Time.timeScale = 1;
        }

        /// <summary>
        ///     角色开始时的回调函数
        /// </summary>
        private void Start()
        {
            // 给HurtBox被触碰时, 触发的事件添加回调函数(受伤),并设置自动销毁
            HurtBox.OnTriggerEnter2DEvent(Collider2D =>
                    {
                        // 如果碰撞的对象的父对象没有Enemy标签 则返回
                        if (!Collider2D.gameObject.transform.parent.CompareTag("Enemy")) return;
                        Global.Health.Value -= mDamage;
                    }
                )
                .UnRegisterWhenGameObjectDestroyed(gameObject);
            // 给血量增加事件添加死亡回调函数
            Global.Health.Register(newValue =>
            {
                HPValue.fillAmount = (float)newValue / Global.MaxHealth.Value;
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
            // 给血量最大值增加事件添加显示回调函数
            Global.MaxHealth.RegisterWithInitValue(newValue =>
                {
                    HPValue.fillAmount = (float)Global.Health.Value / newValue;
                })
                .UnRegisterWhenGameObjectDestroyed(gameObject);
            // 给经验值增加事件添加升级回调函数
            Global.Exp.Register(newValue =>
            {
                if (newValue >= Global.MaxHealth.Value)
                {
                    Global.Level.Value += 1;
                    Global.Exp.Value = 0;
                    // 最大经验值增加1.2倍再向下取整
                    Global.MaxExp.Value = (int)(Global.MaxExp.Value * 1.2f);
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        /// <summary>
        ///     逐帧更新的回调函数
        /// </summary>
        private void Update()
        {
        }


        /// <summary>
        ///     物理引擎的回调函数
        /// </summary>
        private void FixedUpdate()
        {
            // 目标位置 = 当前位置 + 基于输入方向的移动向量
            var targetPosition = transform.position + new Vector3(mMoveInput.x, mMoveInput.y) * mSpeed;


            // 使用SmoothDamp方法平滑过渡到目标位置
            // SmoothDamp会自动调整速度，根据smoothTime提供更自然的移动效果
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }

        public void OnSingletonInit()
        {
        }

        /// <summary>
        ///     公开的 input system 回调函数 OnMove
        /// </summary>
        public void OnMove(InputAction.CallbackContext context)
        {
            // 获取输入的值，转换为2D向量并标准化
            mMoveInput = context.ReadValue<Vector2>().normalized;
        }
    }
}