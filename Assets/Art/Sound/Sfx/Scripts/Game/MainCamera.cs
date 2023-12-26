using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    // MainCamera 类，用于处理摄像机跟随玩家的逻辑
    public partial class MainCamera : ViewController, ISingleton
    {
        /// <summary>
        ///     玩家的 Transform 组件。
        /// </summary>
        [SerializeField] private Transform mPlayer;

        /// <summary>
        ///     平滑移动时间。
        /// </summary>
        [SerializeField] private float smoothTime = 0.3f;

        /// <summary>
        ///     初始震动幅度。
        /// </summary>
        private readonly float initialShakeMagnitude = 0.7f;

        /// <summary>
        ///     震动持续时间，单位秒。
        /// </summary>
        private readonly float shakeDuration = 0.5f;

        /// <summary>
        ///     表示是否正在震动的标志。
        /// </summary>
        private bool isShaking;

        /// <summary>
        ///     摄像机与玩家的偏移量。
        /// </summary>
        private Vector3 mCameraOffset;

        /// <summary>
        ///     用于平滑移动的速度变量。
        /// </summary>
        private Vector3 mVelocity = Vector3.zero;

        /// <summary>
        ///     震动计时器。
        /// </summary>
        private float shakeTimer;


        /// <summary>
        ///     摄像机实例
        /// </summary>
        public static MainCamera Instance => MonoSingletonProperty<MainCamera>.Instance;

        // Start 方法初始化玩家位置和摄像机偏移量
        private void Start()
        {
            mPlayer = Player.Instance.transform;
            mCameraOffset = transform.position - mPlayer.position;
        }

        // LateUpdate 方法更新摄像机位置，使其平滑地跟随玩家
        private void LateUpdate()
        {
            var targetPosition = mPlayer.position + mCameraOffset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref mVelocity, smoothTime);

            // 摄像机震动逻辑
            if (isShaking) ShakeCamera();
        }

        public void OnSingletonInit()
        {
        }


        /// <summary>
        ///     公共方法 Shake，触发摄像机震动效果
        /// </summary>
        public void Shake()
        {
            isShaking = true;
            shakeTimer = shakeDuration;
        }

        // 震动逻辑
        private void ShakeCamera()
        {
            var originalPosition = transform.position; // 原始位置
            shakeTimer -= Time.deltaTime; // 更新震动时间
            // 如果震动时间大于 0 且时间流速大于 0
            if (shakeTimer > 0 && Time.timeScale > 0)
            {
                // 插值计算当前震动幅度
                var currentShakeMagnitude =
                    Mathf.Lerp(initialShakeMagnitude, 0, (shakeDuration - shakeTimer) / shakeDuration);
                // 随机改变位置产生震动
                var x = originalPosition.x + Random.Range(-1f, 1f) * currentShakeMagnitude;
                var y = originalPosition.y + Random.Range(-1f, 1f) * currentShakeMagnitude;
                transform.position = new Vector3(x, y, originalPosition.z);
            }
            else
            {
                // 震动结束，返回原始位置
                transform.position = originalPosition;
                isShaking = false;
            }
        }
    }
}