using EGO.Constant;
using EGO.Framework;
using UnityEngine;

namespace EGO.Logic
{
    public class ProductListView : VerticalLayout
    {
        public const string KEY = nameof(ProductListView);


        private ILayout mProductsParent = new VerticalLayout();

        public ProductListView()
        {
            VerticalStyle = "box";

            new ButtonView("创建产品", () => { this.PushCommand(() => { OpenProductEditor(); }); }).FontBold()
                .FontSize(EGOTheme.TEXT_BUTTON_SIZE)
                .AddTo(this);


            mProductsParent.AddTo(new ScrollLayout().AddTo(this));

            RegisterEvent(GlobalEvents.OnTopBarMenuClicked, key =>
            {
                if (key == KEY)
                {
                    Show();
                }
                else
                {
                    Hide();
                }
            });


            Refresh();
        }


        protected override void OnRefresh()
        {
            mProductsParent.Clear();

            foreach (var product in Model.Products)
            {
                new ProductView(product, () =>
                    {
                        Model.DeleteProduct(product);
                        Model.Save();

                        RefreshNextFrame();

                    },
                    () => this.PushCommand(() => OpenProductEditor(product)),
                    () =>
                    {
                        this.PushCommand(() => { new ProductDetailView(product).AddTo(Parent); });

                        RemoveFromParent();
                    }
                ).AddTo(mProductsParent);
            }
        }


        void OpenProductEditor(Product product = null)
        {
            var productName = product?.Name ?? string.Empty;
            var productDescription = product?.Description ?? string.Empty;


            var productEditor = Window.CreateSubWindow("创建产品");

            new LabelView("名称:").TextMiddleCenter().FontBold().FontSize(20).AddTo(productEditor);

            new TextAreaView(productName)
                .Height(30)
                .FontSize(EGOTheme.INPUT_TEXT_SIZE)
                .AddTo(productEditor)
                .Content.Bind(value => productName = value);


            new LabelView("描述:").TextMiddleCenter().FontBold().FontSize(20).AddTo(productEditor);

            new TextAreaView(productDescription)
                .ExpandHeight()
                .FontSize(EGOTheme.INPUT_TEXT_SIZE)
                .AddTo(productEditor)
                .Content.Bind(value => productDescription = value);

            new ButtonView("保存", () =>
                {
                    if (string.IsNullOrWhiteSpace(productName) || string.IsNullOrWhiteSpace(productDescription))
                    {
                        return;
                    }

                    if (product == null)
                    {
                        Model.CreateProduct(productName, productDescription);
                    }
                    else
                    {
                        product.Name = productName;
                        product.Description = productDescription;
                    }

                    Model.Save();
                    productEditor.Close();

                    Refresh();
                }).FontBold()
                .FontSize(EGOTheme.TEXT_BUTTON_SIZE)
                .AddTo(productEditor);

            productEditor.Show();
        }
    }
}