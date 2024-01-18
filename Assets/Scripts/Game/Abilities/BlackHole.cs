using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public partial class BlackHole : ViewController, IController
    {
        [Tooltip("黑洞存在的时长")] public float duration = 5f; // 黑洞存在的时长
        [Tooltip("黑洞的攻击力")] public float attack = 10f; // 黑洞的攻击力
        [Tooltip("黑洞的大小")] public float size = 1f; // 黑洞的大小
        [Tooltip("黑洞的移动速度")] public float moveSpeed = 1f; // 移动速度
        [Tooltip("黑洞的旋转速度")] public float rotationSpeed = 90f; // 黑洞的旋转速度

        private float currentDuration; // 当前已存在的时长
        private Camera mainCamera; // 存储对主相机的引用，以便重用
        private Vector2 moveDirection; // 移动方向

        private void Start()
        {
            mainCamera = Camera.main; // 获取主相机的引用
            transform.localScale = Vector3.one * size; // 初始化黑洞的大小
            moveDirection = Random.insideUnitCircle.normalized; // 随机一个初始方向
            currentDuration = duration; // 初始化存在的时长
            HitBox.OnTriggerEnter2DEvent(other =>
            {
                this.SendCommand(new AttackEnemyCommand(other.gameObject, attack)); // 发送攻击命令
                this.SendCommand(new DeBuffCommand(other.gameObject, DeBuffType.Attract, gameObject)); // 发送吸引DeBuff命令
            }).UnRegisterWhenGameObjectDestroyed(this);
        }

        private void Update()
        {
            transform.Translate(moveDirection * (moveSpeed * Time.deltaTime)); // 移动黑洞

            BounceOffScreenEdges(); // 确保黑洞始终在相机视野内

            currentDuration -= Time.deltaTime; // 更新黑洞的存在时间
            if (currentDuration <= 0) Destroy(gameObject); // 时间到了销毁黑洞
        }

        public IArchitecture GetArchitecture()
        {
            return Global.Interface; // 返回全局接口
        }

        private void BounceOffScreenEdges()
        {
            var minScreenBounds = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)); // 获取相机的最小边界
            var maxScreenBounds = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0)); // 获取相机的最大边界

            var radius = size * 0.5f; // 获取黑洞的半径（如果是球体的话）

            // 检查并处理边界反弹
            var position = transform.position;
            if (transform.position.x - radius <= minScreenBounds.x ||
                transform.position.x + radius >= maxScreenBounds.x)
            {
                moveDirection.x = -moveDirection.x; // 水平方向反转移动方向
                position = new Vector3(
                    Mathf.Clamp(position.x, minScreenBounds.x + radius, maxScreenBounds.x - radius),
                    position.y,
                    position.z);
                transform.position = position;
            }

            if (transform.position.y - radius <= minScreenBounds.y ||
                transform.position.y + radius >= maxScreenBounds.y)
            {
                moveDirection.y = -moveDirection.y; // 垂直方向反转移动方向
                position = transform.position;
                position = new Vector3(
                    position.x,
                    Mathf.Clamp(position.y, minScreenBounds.y + radius, maxScreenBounds.y - radius),
                    position.z);
                transform.position = position;
            }
        }
    }
}