using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace QFramework
{
    [System.Serializable]
    public class WallGrab2D
    {
        public bool Enabled = true;
        
        [Header("Config")]
        public Rigidbody2D Rigidbody2D;

        public Func<bool> AndCondition = () => true;

        public Func<bool> IsGround = () => throw new Exception("WallGrab2D.IsGround not set yet");

        public Func<float> HorizontalInput = () => throw new Exception("WallGrab2D.HorizontalInput not set yet");
        public Func<bool> JumpButtonDown = () => throw new Exception("WallGrab2D.JumpButtonDown not set yet");

        public float WallJumpDuration = 0.2f;
        [Header("State")]
        public bool OnWall = false;

        public bool WallJumping = false;


        [FormerlySerializedAs("OnWallGrab")] [Header("Events")] 
        public UnityEvent OnWallStart = new UnityEvent();
        public UnityEvent OnWallStop = new UnityEvent();
        public UnityEvent OnWallJumpStart = new UnityEvent();
        public UnityEvent<float> OnWallJumpUpdate;
        public UnityEvent OnWallJumpStop = new UnityEvent();
        
        private float mWallJumpStartTime = 0;
        private float mPlayerDirectionOnWall = 1;
        private float mWallJumpDirection = 1;
        
        private float mCachedGravityScale = 0;

        
        public void TouchWall()
        {
            if (!Enabled) return;
            if (!AndCondition()) return;
                
            if (!OnWall)
            {
                var horizontalInput = HorizontalInput();
                    
                // 方向相同
                if (horizontalInput * Rigidbody2D.transform.localScale.x > 0)
                {
                    // 不是落地
                    if (!IsGround())
                    {
                        // 触发挂墙

                        OnWall = true;

                        OnWallStart?.Invoke();
                        
                        mCachedGravityScale = Rigidbody2D.gravityScale;
                        Rigidbody2D.gravityScale = 0;
                        Rigidbody2D.velocity = Vector2.zero;

                        mPlayerDirectionOnWall = Rigidbody2D.transform.localScale.x;
                    }
                }
            }
        }
        
        
        public void Update()
        {
            if (!Enabled) return;
            if (!AndCondition()) return;

            if (OnWall)
            {
                var horizontalInput = HorizontalInput();
                
                // 方向不相同
                if (horizontalInput * mPlayerDirectionOnWall < 0)
                {
                    // 取消挂墙

                    OnWall = false;
                    
                    Rigidbody2D.gravityScale = mCachedGravityScale;

                    OnWallStop?.Invoke();
                }


                if (JumpButtonDown())
                {
                    OnWall = false;
                    WallJumping = true;
                    mWallJumpStartTime = Time.time;
                    
                    Rigidbody2D.gravityScale = mCachedGravityScale;
                    
                    // 给一个反方向的速度
                    // 转向
                    Rigidbody2D.LocalScaleX(-Rigidbody2D.transform.localScale.x);

                    mWallJumpDirection = Mathf.Sign(Rigidbody2D.transform.localScale.x);

                    
                    OnWallStop?.Invoke();
                    OnWallJumpStart?.Invoke();
                    
                }
                
            }

            if (WallJumping)
            {
                OnWallJumpUpdate?.Invoke(mWallJumpDirection * 2);
                
                // 持续 0.3 秒
                if (Time.time - mWallJumpStartTime >= WallJumpDuration)
                {
                    WallJumping = false;
                    
                    OnWallJumpStop?.Invoke();
                    // GetComponent<PlayerMovement>().ControlledHorizontalInput = 0;

                    // 给转向的方向一个横向的速度
                    // 这个需要持续一段时间
                }
            } 
        }
        


    }
}