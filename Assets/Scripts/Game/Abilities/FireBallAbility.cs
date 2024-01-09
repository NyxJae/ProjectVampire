using System;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace ProjectVampire
{
    public partial class FireBallAbility : ViewController, IController
    {
        // 转速
        /// <summary>
        ///     私有的 转速 属性 在 Inspector 中显示
        /// </summary>
        [SerializeField] private float mRotateSpeed = 10f;

        // 火球对象列表list
        private readonly List<FireBall> mFireBallList = new();

        // 火球数
        public BindableProperty<int> FireBallCount = new(1);

        // // 攻击力属性
        public float Attack { get; set; }

        private void Start()
        {
            // 生成8个火球,并设置火球的攻击力,并设置火球的父物体为火球能力,并设置火球的位置为火球能力的位置,添加到火球列表中
            for (var i = 0; i < 8; i++)
            {
                // 生成火球
                var fireBall = FireBall.Instantiate().Position(transform.position).Parent(transform).Hide();
                // 添加到火球列表中
                mFireBallList.Add(fireBall);
            }

            FireBallCount.RegisterWithInitValue(newValue =>
            {
                // 全部隐藏
                mFireBallList.ForEach(fireBall => fireBall.Hide());
                // 最大火球数为8
                newValue = Math.Min(newValue, 8);
                // 循环火球列表
                for (var i = 0; i < newValue; i++)
                {
                    // 显示火球
                    mFireBallList[i].Show();
                    // 设置火球的角度平均分布
                    var angle = i * (360f / newValue); // 计算每个火球的角度
                    mFireBallList[i].transform.rotation = Quaternion.Euler(0, 0, angle);
                    // 设置火球的攻击力
                    mFireBallList[i].Attack = Attack;
                }
            }).UnRegisterWhenGameObjectDestroyed(this);
        }

        private void Update()
        {
            // 根据转速,旋转z轴
            transform.Rotate(0, 0, mRotateSpeed * Time.deltaTime);
        }

        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }
    }
}