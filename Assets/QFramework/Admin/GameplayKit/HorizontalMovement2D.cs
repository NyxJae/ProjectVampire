using System;
using UnityEngine;
using UnityEngine.Events;

namespace QFramework
{
    [Serializable]
    public class HorizontalMovement2D
    {
        public bool Enabled = true;
        
        [Header("Config")]
        public Rigidbody2D Rigidbody2D;

        public float Speed = 8;

        public Func<float> HorizontalInput = ()=> throw new Exception("HorizontalMovement2D.HorizontalInput Not Set Yet");

        public float CurrentHorizontalSpeed = 0;

        // 有个移动方向要处理
        public float MovementDirection = 1;

        // 自动移动的方向（风力等、摩擦力）
        public float AutoHorizontalMovement = 0;

        // 自动物理速度
        public float AutoPhysicsSpeed = 0;

        [Header("Events")] public UnityEvent OnMovementStart = new UnityEvent();
        public UnityEvent OnMovementStop = new UnityEvent();
        
        /// <summary>
        /// 横向移动状态
        /// </summary>
        public enum States
        {
            /// <summary>
            /// 停止状态
            /// </summary>
            Stop,

            /// <summary>
            /// 加速状态
            /// </summary>
            Increase,

            /// <summary>
            /// 最大速度
            /// </summary>
            MaxSpeed,

            /// <summary>
            /// 减速状态
            /// </summary>
            Decrease,
        }


        public States HorizontalMovementState = States.Stop;

        public float SpeedIncreaseRate = 0.8f;
        public float SpeedDecreaseRate = 0.8f;
        
        private float mHorizontalInput = 0f;

        public void Update()
        {
            if (!Enabled) return;
            
            mHorizontalInput = HorizontalInput() + AutoHorizontalMovement;
              
            if (mHorizontalInput > 0)
            {
                MovementDirection = 1;

                Speed = MovementDirection * Mathf.Abs(Speed);
            }
            else if (mHorizontalInput < 0)
            {
                MovementDirection = -1;
                Speed = MovementDirection * Mathf.Abs(Speed);
            }
            
            if (mHorizontalInput * Rigidbody2D.transform.localScale.x < 0)
            {
                Rigidbody2D.LocalScaleX(-Rigidbody2D.transform.localScale.x);
            }

            // 停止/减速->加速
            if (mHorizontalInput != 0 && (HorizontalMovementState == States.Stop ||
                                          HorizontalMovementState == States.Decrease))
            {
                HorizontalMovementState = States.Increase;

                CurrentHorizontalSpeed = 0;
            }
            // 加速->最大速度
            else if (mHorizontalInput != 0 && HorizontalMovementState == States.Increase)
            {
                CurrentHorizontalSpeed = Mathf.Lerp(CurrentHorizontalSpeed, Speed, SpeedIncreaseRate);
                
                if (Mathf.Abs(CurrentHorizontalSpeed - Speed) < 0.1f)
                {
                    CurrentHorizontalSpeed = Speed;

                    HorizontalMovementState = States.MaxSpeed;
                }
            } // 最大速度
            else if (mHorizontalInput != 0 && HorizontalMovementState == States.MaxSpeed)
            {
                CurrentHorizontalSpeed = Speed;
            }
            // 最大速度/加速->减速
            else if (mHorizontalInput == 0 && (HorizontalMovementState == States.MaxSpeed ||
                                               HorizontalMovementState == States.Increase))
            {
                HorizontalMovementState = States.Decrease;
            }
            // 减速->停止
            else if (mHorizontalInput == 0 && HorizontalMovementState == States.Decrease)
            {
                CurrentHorizontalSpeed = Mathf.Lerp(CurrentHorizontalSpeed, 0, SpeedDecreaseRate);

                if (Mathf.Abs(CurrentHorizontalSpeed) < 0.1f)
                {
                    CurrentHorizontalSpeed = 0;

                    HorizontalMovementState = States.Stop;
                }
            }
        }

        public void FixedUpdate()
        {
            if (!Enabled) return;
            
            // 横向移动的核心代码
            Rigidbody2D.velocity = new Vector2(CurrentHorizontalSpeed + AutoPhysicsSpeed, Rigidbody2D.velocity.y);
        }

    }
}