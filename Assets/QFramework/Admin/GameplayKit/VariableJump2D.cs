using System;
using UnityEngine;
using UnityEngine.Events;

namespace QFramework
{
    [System.Serializable]
    public class VariableJump2D
    {

        [Header("Config")]
        public Rigidbody2D Rigidbody2D;
        
        public float JumpSpeed = 15;
        
        public float MinJumpTime = 0.1f;

        public float MaxJumpTime = 0.3f;

        public float GravityMultiplier = 2.0f;

        public float FallMultiplier = 1.0f;

        public int MaxJumpCount = 1;

        public int InputBufferFrameCount = 10;

        // 土狼时间
        public int CoyoteFrameCount = 10;


        public float AutoPhysicSpeed = 0;
        
        public Func<bool> JumpButtonDown = () => throw new Exception("VariableJump.JumpButtonDown Not Set Yet");
        public Func<bool> JumpButtonUp = () => throw new Exception("VariableJump.JumpButtonUp Not Set Yet");
        public Func<bool> IsGround = () => throw new Exception("VariableJump.IsGround Not Set Yet");

        public Func<bool> OrJumpCondition = () => false;

        [Header("States")]
        public bool Enabled = true;

        public bool JumpPressed = false;
        
        public float CurrentJumpTime = 0f;

        public int CurrentJumpCount = 0;
        
        public States State = States.NotJump;

        public int JumpPressFrameCount = -1000;

        public int LeaveGroundFrame;

        [Header("Events")] public UnityEvent OnJump;
        public UnityEvent OnLand;

        
        bool mCanJump =>
            CurrentJumpCount == 0 && Time.frameCount - LeaveGroundFrame <= CoyoteFrameCount || // 10 帧 约 0.15 秒左右（60fps)
            IsGround() ||
            OrJumpCondition();
        
        
        public enum States
        {
            NotJump,
            MinJump,
            ControlJump,
        }



        public void StartJump()
        {
            OnJump?.Invoke();
            JumpPressed = true;
            CurrentJumpCount++;

            if (State == States.NotJump)
            {
                State = States.MinJump;
                CurrentJumpTime = 0;
            }
        }

        // 着陆
        public void Land()
        {
            OnLand?.Invoke();
            CurrentJumpCount = 0;

            // 当落地时判断是否缓存了输入

            if (Time.frameCount - JumpPressFrameCount <= 10)
            {
                // 触发起跳
                StartJump();
            }
        }

        // 离开地面
        public void LeaveGround()
        {
            LeaveGroundFrame = Time.frameCount;
        }
        
        public void Update()
        {
            if (!Enabled) return;
            
            if (JumpButtonDown())
            {
                // 记录跳跃键按下时的帧数
                JumpPressFrameCount = Time.frameCount;
            }
            
            if (JumpButtonDown() && mCanJump)
            {
                StartJump();
            }
            
            if (JumpButtonUp())
            {
                JumpPressed = false;
            }
            
            
            CurrentJumpTime += Time.deltaTime;
        }

        public void FixedUpdate()
        {
            if (!Enabled) return;
            
            if (State == States.MinJump)
            {
                Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x,  JumpSpeed + AutoPhysicSpeed);

                if (CurrentJumpTime >= MinJumpTime)
                {
                    State = States.ControlJump;
                }
            }
            else if (State == States.ControlJump)
            {
                Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, JumpSpeed + AutoPhysicSpeed);

                if (!JumpPressed || JumpPressed && CurrentJumpTime >= MaxJumpTime)
                {
                    State = States.NotJump;
                }
            }

            if (Rigidbody2D.velocity.y > 0 && State != States.NotJump)
            {
                var progress = CurrentJumpTime / MaxJumpTime;

                float jumpGravityMultiplier = GravityMultiplier;

                if (progress > 0.5f)
                {
                    jumpGravityMultiplier = GravityMultiplier * (1 - progress);
                }

                Rigidbody2D.velocity += Physics2D.gravity * jumpGravityMultiplier * Time.deltaTime;
            }
            else if (Rigidbody2D.velocity.y < 0)
            {
                Rigidbody2D.velocity += Physics2D.gravity * FallMultiplier * Time.deltaTime;
            }
        }
    }
}