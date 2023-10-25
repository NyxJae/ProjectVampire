using EGO.Constant;
using EGO.Framework;
using EGO.Framework.Util;
using EGO.Logic;
using EGO.V1;
using UnityEngine;

namespace EGO.ViewController
{
    public class CategoryListView : VerticalLayout
    {
        private readonly ILayout mCategoryContainer = new ScrollLayout();
        
        public CategoryListView()
        {
            new SpaceView(10).AddTo(this);
            
            new ButtonView("+", () =>
            {
                this.PushCommand(() =>
                {
                    OpenCategoryEditor();    
                });   
            }).AddTo(this)
                .FontColor(Color.white)
                .Color(Color.yellow)
                .Height(30)
                .FontSize(20);
            
            mCategoryContainer.AddTo(this);
            
            RegisterEvent(GlobalEvents.OnTodoListMenuClicked, key =>
            {
                if (key == Key)
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

        public const string Key = nameof(CategoryListView);

        protected override void OnRefresh()
        {
            mCategoryContainer.Clear();
            
            foreach (var category in Model.Categories)
            {
                var horLayout = new HorizontalLayout().AddTo(mCategoryContainer);

                new CategoryComponent(category).AddTo(horLayout);
                
                new FlexibaleSpaceView().AddTo(horLayout);

                new ImageButtonView("edit", () =>
                    {
                        this.PushCommand(() => { OpenCategoryEditor(category); });
                    })
                    .Width(25)
                    .Height(25)
                    .Color(Color.black)
                    .AddTo(horLayout);

                new ImageButtonView("delete", () =>
                    {
                        Model.RemoveCategory(category);
                        Model.Save();
                        
                        RefreshNextFrame();

                    }).Width(25).Height(25).Color(Color.red)
                    .AddTo(horLayout);
            }
        }

        private SubWindow mSubWindow = null;

        private void OpenCategoryEditor(Category category = null)
        {
            mSubWindow = Window.CreateSubWindow("分类编辑");
            
            var verLayout = new VerticalLayout("box").AddTo(mSubWindow);

            new LabelView("名字").AddTo(verLayout).FontSize(15).FontBold();

            var name = category?.Name ?? string.Empty;
            var color = category?.Color.ToColor() ?? Color.black;

            new TextAreaView(name).AddTo(verLayout)
                .FontSize(EGOTheme.INPUT_TEXT_SIZE)
                .Content.Bind(newName => name = newName);

            new LabelView("颜色").AddTo(verLayout).FontSize(15).FontBold();

            new ColorView(color).AddTo(verLayout)
                .ExpandHeight()
                .Color.Bind(newColor => color = newColor);

            if (category == null)
            {
                new ButtonView("添加", () =>
                    {
                        Debug.Log($"名字:{name} 颜色:{color}");
                        Debug.Log("添加 点击");

                        Model.CreateCategory(name, color);
                        Model.Save();

                        mSubWindow.Close();
                        mSubWindow = null;

                        Refresh();
                    })
                    .FontBold()
                    .FontSize(EGOTheme.TEXT_BUTTON_SIZE)
                    .AddTo(verLayout);
            }
            else
            {
                new ButtonView("保存", () =>
                    {
                        category.Name = name;
                        category.Color = color.ToText();

                        Model.Save();

                        mSubWindow.Close();
                        mSubWindow = null;
                        
                        RefreshNextFrame();
                    })
                    .FontBold()
                    .FontSize(EGOTheme.TEXT_BUTTON_SIZE)
                    .AddTo(verLayout);     
            }
            
            mSubWindow.Show();
        }

        private void CloseCategoryEditor()
        {
            if (mSubWindow != null)
            {
                mSubWindow.Close();
                mSubWindow = null;
            }
        }

        protected override void OnHide()
        {
            CloseCategoryEditor();
        }
    }

}