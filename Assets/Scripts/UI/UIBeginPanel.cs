using QFramework;

namespace ProjectVampire
{
    public class UIBeginPanelData : UIPanelData
    {
    }

    public partial class UIBeginPanel : UIPanel, IController
    {
        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIBeginPanelData ?? new UIBeginPanelData();
            // 注册 BtnBegin 的点击事件 
            BtnBegin.onClick.AddListener(() =>
            {
                // 关闭 Begin 界面
                UIKit.ClosePanel<UIBeginPanel>();
                // 发送开始游戏命令
                this.SendCommand<BeginGameCommand>();
            });
            // 注册 BtnReward 的点击事件
            BtnReward.onClick.AddListener(() =>
            {
                // 打开奖励界面
                UIKit.OpenPanel<UIRewardPanel>();
            });
            // 注册 BtnAchievement 的点击事件
            AchievementPanel.Hide();
            BtnAchievement.onClick.AddListener(() => { AchievementPanel.Show(); });
        }

        protected override void OnOpen(IUIData uiData = null)
        {
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