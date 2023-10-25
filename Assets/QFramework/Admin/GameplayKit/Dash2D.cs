using System;
using UnityEngine;
using UnityEngine.Events;

namespace QFramework
{
    [System.Serializable]
    public class Dash2D
    {
        public bool Enabled = false;
        
        
        [Header("Config")]
        public Rigidbody2D Rigidbody2D;

        public float Speed = 30;

        public float Duration = 0.2f;


        public Func<bool> IsGround = () => throw new Exception("Dash2D.IsGround Not Set Yet");

        public Func<bool> DashButtonDown = () => throw new Exception("Dash2D.DashButtonDown Not Set Yet");
        
        public Func<bool> AndDashCondition = () => true;
        
        [Header("Events")] public UnityEvent OnDashStart = new UnityEvent();
        public UnityEvent OnDashFinish = new UnityEvent();

        
        
        private bool mCanDash = true;

        private float mDashStartTime = 0;

        private bool mDashing = false;

        private float mCachedGravityScale = 0;

        public void Land()
        {
            mCanDash = true;
        }
        public void Update()
        {
            if (!Enabled) return;
            
            if (DashButtonDown() && !mDashing && mCanDash && AndDashCondition())
            {
                Rigidbody2D.velocity = Vector2.right * Speed * Mathf.Sign(Rigidbody2D.transform.localScale.x);

                OnDashStart?.Invoke();

                mDashStartTime = Time.time;

                mCachedGravityScale = Rigidbody2D.gravityScale;

                Rigidbody2D.gravityScale = 0;

                mDashing = true;

                mCanDash = false;
                    
                OnDashStart?.Invoke();
            }

            if (mDashing && mDashStartTime + Duration < Time.time)
            {
                mDashing = false;

                Rigidbody2D.gravityScale = mCachedGravityScale;

                OnDashFinish?.Invoke();
                    
                // 冲刺结束时，如果是落地状态，则重置

                if (IsGround())
                {
                    mCanDash = true;
                }
            }
            
        }
        
        
    }
}