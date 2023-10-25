using System;
using UnityEngine;
using UnityEngine.Events;

namespace QFramework
{
    [Serializable]
    public class Roll2D
    {
        public bool Enabled = true;
        
        [Header("Config")]
        public Rigidbody2D Rigidbody2D;
        
        

        public float Duration = 0.5f;

        public float Speed = 6f;
        
        public Func<bool> RollButtonDown = () => throw new Exception("Roll2D.RollButtonDown is not set yet");

        public Func<bool> AndCondition = () => true;

        public string RollTag = "Untagged";
        public string RollLayer = "WithoutEnemy";

        [Header("States")] public bool Rolling = false;

        [Header("Events")] 
        public UnityEvent OnRollStart = new UnityEvent();
        public UnityEvent OnRollFinish = new UnityEvent();
        public UnityEvent<float> OnRollUpdate;

        private float mRollStartTime = 0;

        public void Update()
        {
            if (!Enabled) return;

            if (RollButtonDown() && !Rolling && AndCondition())
            {
                Rolling = true;

                mRollStartTime = Time.time;

                OnRollStart?.Invoke();
                
                Rigidbody2D.gameObject.tag = RollTag;
                Rigidbody2D.gameObject.layer = LayerMask.NameToLayer(RollLayer);
            } else if (Rolling)
            {
                // 移动
                Rigidbody2D.velocity = new Vector2(Mathf.Sign(Rigidbody2D.transform.localScale.x) * Speed, Rigidbody2D.velocity.y);

                OnRollUpdate?.Invoke(mRollStartTime);


                if (Time.time > mRollStartTime + Duration)
                {
                    Rolling = false;

                    OnRollFinish?.Invoke();
                    
                    Rigidbody2D.gameObject.tag = "Player";
                    Rigidbody2D.gameObject.layer = LayerMask.NameToLayer("Default");
                } 
            }
        }
        

        
    }
}