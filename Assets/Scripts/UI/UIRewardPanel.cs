using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectVampire
{
	public class UIRewardPanelData : UIPanelData
	{
	}
	public partial class UIRewardPanel : UIPanel
	{
		// 私有的 升级所需金币数量
		[SerializeField]
		private int mCoinUp = 5;
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIRewardPanelData ?? new UIRewardPanelData();

		}

		protected override void OnOpen(IUIData uiData = null)
		{
			// 显示金币数量
			TextCoin.text = $"金币:{Global.Coin.Value}";
			// 给金币增加事件添加显示回调函数
			Global.Coin.Register(newValue =>
			{
				TextCoin.text = $"金币:{newValue}";
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			// 注册 BtnClose 的点击事件
			BtnClose.onClick.AddListener(() =>
			{
				// 关闭当前界面
				UIKit.ClosePanel<UIRewardPanel>();
				// 打开 Begin 界面
				UIKit.OpenPanel<UIBeginPanel>();
			});
			// 注册 BtnCoinUp 的点击事件
			BtnCoinUp.onClick.AddListener(() =>
			{
				// 花金币,提升金币掉落几率
				// 如果金币够
				if (Global.Coin.Value >= mCoinUp)
				{
					// 消耗金币
					Global.Coin.Value -= mCoinUp;
					// 提升金币掉落几率
					Global.DropCoinRate.Value += 0.05f;
				}
			});
			// 注册 BtnExpUp 的点击事件
			BtnExpUp.onClick.AddListener(() =>
			{
				// 花金币,提升经验掉落几率
				// 如果金币够
				if (Global.Coin.Value >= mCoinUp)
				{
					// 消耗金币
					Global.Coin.Value -= mCoinUp;
					// 提升经验掉落几率
					Global.DropExpRate.Value += 0.05f;
				}
			});
		}

		protected override void OnShow()
		{
		}

		protected override void OnHide()
		{
		}

		protected override void OnClose()
		{
		}
	}
}
