using EGO.Framework;

namespace EGO.Logic
{
    public class VersionView : VerticalLayout
    {
        private LabelView mMagorView;

        private LabelView mMiddleView;

        private LabelView mSmallView;

        
        public Version Version = new Version(0, 0, 0);

        public VersionView(Version version = null)
        {

            Version = version ?? Version; 

            var firstLine = new HorizontalLayout().AddTo(this);

            new SpaceView(90).AddTo(firstLine);
            new ButtonView("+", () =>
            {
                Version.Major++;
                mMagorView.Content = $"v{Version.Major}";

            }).Width(20).Height(20).FontSize(10).FontNormal().AddTo(firstLine);
            new SpaceView(7).AddTo(firstLine);
            new ButtonView("+", () =>
            {
                Version.Middle++;
                mMiddleView.Content = $".{Version.Middle}";

            }).Width(20).Height(20).FontSize(10).FontNormal().AddTo(firstLine);
            new SpaceView(7).AddTo(firstLine);
            new ButtonView("+", () =>
            {
                Version.Small++;
                mSmallView.Content = $".{Version.Small}";
            }).Width(20).Height(20).FontSize(10).FontNormal().AddTo(firstLine);

            var secondLine = new HorizontalLayout().AddTo(this);

            new LabelView("版本号:").FontBold().FontSize(20).Width(80).Height(35).TextMiddleCenter().AddTo(secondLine);
            mMagorView = new LabelView("v" + Version.Major).FontSize(25).FontBold().Width(35).AddTo(secondLine);
            mMiddleView = new LabelView("." + Version.Middle).FontSize(25).FontBold().Width(35).AddTo(secondLine);
            mSmallView = new LabelView("." + Version.Small).FontSize(25).FontBold().Width(35).AddTo(secondLine);

            var thirdLine = new HorizontalLayout().AddTo(this);

            new SpaceView(90).AddTo(thirdLine);
            new ButtonView("-", () =>
            {
                Version.Major--;
                mMagorView.Content = $"v{Version.Major}";
            }).Width(20).Height(20).FontSize(10).FontNormal().AddTo(thirdLine);
            new SpaceView(7).AddTo(thirdLine);
            new ButtonView("-", () =>
            {
                Version.Middle--;
                mMiddleView.Content = $".{Version.Middle}";

            }).Width(20).Height(20).FontSize(10).FontNormal().AddTo(thirdLine);
            new SpaceView(7).AddTo(thirdLine);
            new ButtonView("-", () =>
            {
                Version.Small--;
                mSmallView.Content = $".{Version.Small}";
            }).Width(20).Height(20).FontSize(10).FontNormal().AddTo(thirdLine);

        }

    }
}