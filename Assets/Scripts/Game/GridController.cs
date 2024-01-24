using QFramework;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ProjectVampire
{
    public partial class GridController : ViewController
    {
        // 成员变量
        // 九个tilemap 分别是九宫格
        private Tilemap _centerTilemap;
        private Tilemap _downTilemap;
        private Tilemap _leftDownTilemap;
        private Tilemap _leftTilemap;
        private Tilemap _leftUpTilemap;

        // 主角所在的格子x
        private int _playerGridX;

        // 主角所在的格子y
        private int _playerGridY;
        private Tilemap _rightDownTilemap;
        private Tilemap _rightTilemap;
        private Tilemap _rightUpTilemap;
        private Vector3Int _size;

        // 计时器
        private float _timeSinceLastUpdate;
        private Tilemap _upTilemap;

        private void Awake()
        {
            // 压缩边界 使得tilemap的边界紧贴tile
            Tilemap.CompressBounds();
            _size = Tilemap.size;
        }

        private void Start()
        {
            // 创建九宫格tilemap
            CreatTliemaps();
            // 更新九宫格tilemap的位置
            UpdateTilemaps();
        }

        private void Update()
        {
            // 每过一秒钟更新一次九宫格tilemap的位置
            _timeSinceLastUpdate += Time.deltaTime;
            if (_timeSinceLastUpdate >= 1.0f)
            {
                // 获取主角所在位置 转换成格子坐标
                var playerGridPos = Tilemap.layoutGrid.WorldToCell(Player.Instance.transform.position);
                // 更新主角所在的格子x
                _playerGridX = playerGridPos.x / _size.x;
                // 更新主角所在的格子y
                _playerGridY = playerGridPos.y / _size.y;
                // 更新九宫格tilemap的位置
                UpdateTilemaps();
                _timeSinceLastUpdate = 0.0f;
            }
        }

        /// <summary>
        ///     创建九宫格tilemap
        /// </summary>
        private void CreatTliemaps()
        {
            // 实例化九宫格tilemap
            _centerTilemap = Tilemap;
            _leftTilemap = Tilemap.InstantiateWithParent(this);
            _rightTilemap = Tilemap.InstantiateWithParent(this);
            _upTilemap = Tilemap.InstantiateWithParent(this);
            _downTilemap = Tilemap.InstantiateWithParent(this);
            _leftUpTilemap = Tilemap.InstantiateWithParent(this);
            _leftDownTilemap = Tilemap.InstantiateWithParent(this);
            _rightUpTilemap = Tilemap.InstantiateWithParent(this);
            _rightDownTilemap = Tilemap.InstantiateWithParent(this);
        }

        // 更新九宫格tilemap的位置
        private void UpdateTilemaps()
        {
            _centerTilemap.Position(new Vector3((_playerGridX + 0) * _size.x, (_playerGridY + 0) * _size.y, 0));
            _leftTilemap.Position(new Vector3((_playerGridX - 1) * _size.x, (_playerGridY + 0) * _size.y, 0));
            _rightTilemap.Position(new Vector3((_playerGridX + 1) * _size.x, (_playerGridY + 0) * _size.y, 0));
            _upTilemap.Position(new Vector3((_playerGridX + 0) * _size.x, (_playerGridY + 1) * _size.y, 0));
            _downTilemap.Position(new Vector3((_playerGridX + 0) * _size.x, (_playerGridY - 1) * _size.y, 0));
            _leftUpTilemap.Position(new Vector3((_playerGridX - 1) * _size.x, (_playerGridY + 1) * _size.y, 0));
            _leftDownTilemap.Position(new Vector3((_playerGridX - 1) * _size.x, (_playerGridY - 1) * _size.y, 0));
            _rightUpTilemap.Position(new Vector3((_playerGridX + 1) * _size.x, (_playerGridY + 1) * _size.y, 0));
            _rightDownTilemap.Position(new Vector3((_playerGridX + 1) * _size.x, (_playerGridY - 1) * _size.y, 0));
        }
    }
}