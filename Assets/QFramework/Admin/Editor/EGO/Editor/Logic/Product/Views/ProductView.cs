using System;
using EGO.Framework;
using UnityEngine;

namespace EGO.Logic
{
    public class ProductView : VerticalLayout
    {
        public ProductView(Product product, Action onDelete, Action onEdit,Action onOpen)
        {
            VerticalStyle = "box";
            
            new TreeNode(false,product.Name)
                .FontSize(20)
                .FontBold()
                .Add2FirstLine(new ImageButtonView("edit", onEdit)
                    .Width(25)
                    .Height(25)
                    .Color(Color.black))
                .Add2FirstLine(new ImageButtonView("delete", onDelete).Width(25).Height(25).Color(Color.red))
                .Add2Spread(new ProductDetailView(product))
                .AddTo(this);
        }
    }
}