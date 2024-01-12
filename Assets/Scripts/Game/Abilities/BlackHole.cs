using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public partial class BlackHole : ViewController, IController
    {
        // 定义黑洞的属性
        [Tooltip("黑洞存在的时长")] public float duration = 5f; // 黑洞存在的时长

        [Tooltip("黑洞的攻击力")] public float attack = 10f; // 黑洞的攻击力

        [Tooltip("黑洞的大小")] public float size = 1f; // 黑洞的大小

        [Tooltip("黑洞的移动速度")] public float moveSpeed = 1f; // 移动速度

        private float currentDuration; // 当前已存在的时长

        private Camera mainCamera; // 存储对主相机的引用，以便重用
        private Vector2 moveDirection; // 移动方向

        private void Start()
        {
            mainCamera = Camera.main; // 获取主相机的引用
            // 初始化黑洞的大小
            transform.localScale = Vector3.one * size;
            // 随机一个初始方向
            moveDirection = Random.insideUnitCircle.normalized;
            // 初始化存在的时长
            currentDuration = duration;
            HitBox.OnTriggerEnter2DEvent(other =>
            {
                // 发送攻击命令
                this.SendCommand(new AttackEnemyCommand(other.gameObject, attack));
                // 发送吸引DeBuff命令
                this.SendCommand(new DeBuffCommand(other.gameObject, DeBuffType.Attract, gameObject));
            }).UnRegisterWhenGameObjectDestroyed(this);
        }

        private void Update()
        {
            // 移动黑洞
            transform.Translate(moveDirection * (moveSpeed * Time.deltaTime));

            // 确保黑洞始终在相机视野内
            BounceOffScreenEdges();

            // 更新黑洞的存在时间
            currentDuration -= Time.deltaTime;
            if (currentDuration <= 0) Destroy(gameObject); // 时间到了销毁黑洞
        }


        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }

        private void BounceOffScreenEdges()
        {
            // 获取相机的边界
            var minScreenBounds = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
            var maxScreenBounds = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

            // 获取黑洞的半径（如果是球体的话）
            var radius = size * 0.5f;

            // 检查并处理边界反弹
            if (transform.position.x - radius <= minScreenBounds.x ||
                transform.position.x + radius >= maxScreenBounds.x)
            {
                moveDirection.x = -moveDirection.x; // 水平方向反转移动方向
                transform.position = new Vector3(
                    Mathf.Clamp(transform.position.x, minScreenBounds.x + radius, maxScreenBounds.x - radius),
                    transform.position.y,
                    transform.position.z);
            }

            if (transform.position.y - radius <= minScreenBounds.y ||
                transform.position.y + radius >= maxScreenBounds.y)
            {
                moveDirection.y = -moveDirection.y; // 垂直方向反转移动方向
                transform.position = new Vector3(
                    transform.position.x,
                    Mathf.Clamp(transform.position.y, minScreenBounds.y + radius, maxScreenBounds.y - radius),
                    transform.position.z);
            }
        }
    }
}