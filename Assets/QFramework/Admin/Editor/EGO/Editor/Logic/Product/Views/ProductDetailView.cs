using System;
using System.Linq;
using EGO.Constant;
using EGO.Framework;
using EGO.V1;
using UnityEngine;

namespace EGO.Logic
{
    public class ProductDetailView : VerticalLayout
    {
        public Product Product { get; }

        
        private VerticalLayout mProductVersionsParent;
        private VerticalLayout mFeaturesParent;
        
        public ProductDetailView(Product product)
        {
            Product = product;

            new LabelView(" 描述:" + product.Description)
                .FontBold()
                .FontSize(15)
                .TextMiddleLeft()
                .AddTo(new HorizontalLayout("box").AddTo(this));
            
            var firstLine = new HorizontalLayout("box").AddTo(this);

            new LabelView(" 功能:")
                .FontBold()
                .FontSize(15)
                .TextMiddleLeft()
                .AddTo(firstLine);

            new ImageButtonView("add", () =>
            {
                product.Features.Add(new Feature()
                {
                    Name = "测试功能",
                    Description = "这是一个测试功能"
                });
                
                Model.Save();
                
                RefreshNextFrame();
            })
                .Width(25)
                .Height(25)
                .Color(Color.yellow)
                .FontBold()
                .AddTo(firstLine);
            
            mFeaturesParent = new VerticalLayout("box").AddTo(this);
            
            var thirdLine = new HorizontalLayout("box").AddTo(this);
            
            
            new LabelView(" 版本:")
                .FontBold()
                .FontSize(15)
                .TextMiddleLeft()
                .AddTo(thirdLine);
            
            new ButtonView("创建版本", () =>
            {
                this.PushCommand(() => OpenVersionEditor());
            })
                .Width(80)
                .FontSize(EGOTheme.TEXT_BUTTON_SIZE)
                .FontBold()
                .AddTo(thirdLine);
            
            mProductVersionsParent = new VerticalLayout("box").AddTo(this);

            this.RegisterEvent(GlobalEvents.OnTopBarMenuClicked, key =>
            {
                if (key != ProductListView.KEY)
                {
                    Hide();
                }
                else
                {
                    Show();
                }
            });
            
            Refresh();
        }

        protected override void OnRefresh()
        {
            mFeaturesParent.Clear();

            Product.Features.ForEach(feature =>
            {
                new FeatureView(feature, () =>
                        {
                            this.PushCommand(() =>
                            {
                                Product.Features.Remove(feature);
                                Model.Save();
                            });
                        })
                        {VerticalStyle = "box"}
                    .AddTo(mFeaturesParent);
            });

            mProductVersionsParent.Clear();

            foreach (var productVersion in Product.Versions
                .OrderByDescending(vertion => vertion.Version.VersionNumber))
            {
                new ProductVersionView(productVersion,
                        () => { this.PushCommand(() => { OpenVersionEditor(productVersion); }); })
                    .AddTo(mProductVersionsParent);
            }
        }

        void OpenVersionEditor(ProductVersion productVersion = null)
        {
            var versionEditor = Window.CreateSubWindow(productVersion == null ? "版本创建" : "版本编辑");

            var versionView = new VersionView( productVersion?.Version)
                .AddTo(versionEditor);

            var versionName = productVersion?.Name ?? string.Empty;

            TodoState versionState = productVersion == null ? TodoState.NotStart : productVersion.State;
            
            var secondLine = new HorizontalLayout().AddTo(versionEditor);
            new LabelView("版本名:").FontSize(20).Height(35).FontBold().TextMiddleCenter().Width(80).AddTo(secondLine);
            new TextView(versionName).FontSize(15).TextMiddleLeft().Height(35).AddTo(secondLine)
                .Content.Bind(name=>versionName = name);

            new EnumPopupView<TodoState>(versionState).AddTo(versionEditor)
                .ValueProperty.Bind(value=>versionState = value);

            new ButtonView("保存", () =>
            {
                if (productVersion == null)
                {
                    Product.Versions.Add(new ProductVersion()
                    {
                        Name = versionName,
                        Version = versionView.Version,
                        State = versionState
                    });
                }
                else
                {
                    productVersion.Name = versionName;
                    productVersion.Version = versionView.Version;
                    productVersion.State = versionState;
                }

                Model.Save();

                this.PushCommand(() => { versionEditor.Close(); });
                
                RefreshNextFrame();
                
            }).FontSize(EGOTheme.TEXT_BUTTON_SIZE).FontBold().AddTo(versionEditor);
            
            versionEditor.Show();
        }
        
    }
}