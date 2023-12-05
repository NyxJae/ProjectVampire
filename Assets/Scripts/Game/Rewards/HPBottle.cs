using UnityEngine;
using QFramework;

namespace ProjectVampire
{
	public partial class HPBottle : ViewController
	{
		// 私有的 回复血量 属性
		[SerializeField]
		private int mRecoverHP = 25;
		
		// 公开的 获取血瓶 方法
		public int GetHPBottle()
		{
			ActionKit.Sequence()
				.Callback(() =>
				{
					// 飞向玩家
					transform.position = Vector3.MoveTowards(transform.position, Player.Instance.transform.position, 5f * Time.deltaTime);
					// 播放音效
					//AudioKit.PlaySound("Exp");
				})
				.Callback(() =>
				{
					// 销毁自身
					Destroy(gameObject);
				}).Start(this);
			// 返回经验值
			return mRecoverHP;
		}
	}
}
