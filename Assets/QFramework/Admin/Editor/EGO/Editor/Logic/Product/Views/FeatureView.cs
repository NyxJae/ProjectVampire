using System;
using EGO.Framework;
using UnityEngine;

namespace EGO.Logic
{
    public class FeatureView : VerticalLayout
    {
        private readonly Feature mFeature;

        VerticalLayout mChildrensParent = new VerticalLayout();

        /// <summary>
        /// 缩进值
        /// </summary>
        public int IndentValue = 0;


        private TreeNode mTreeNode = null;

        public FeatureView(Feature feature,Action onDelete, int indentValue = 0)
        {
            mFeature = feature;
            IndentValue = indentValue;

            mTreeNode = new TreeNode(false, " " + mFeature.Name, IndentValue * 15)
                .Add2FirstLine(new ImageButtonView("add", () =>
                    {
                        this.PushCommand(() => { OpenFeatureEditor(); });
                    })
                    .Width(25)
                    .Height(25)
                    .Color(Color.yellow))
                .Add2FirstLine(new ImageButtonView("edit", () => { this.PushCommand(() => { OpenFeatureEditor(feature); }); })
                    .Width(25)
                    .Height(25)
                    .Color(Color.black))
                .Add2FirstLine(new ImageButtonView("delete", () =>
                    {
                        RemoveFromParent();
                        onDelete();
                    })
                    .Width(25)
                    .Height(25)
                    .Color(Color.red))
                .Add2Spread(mChildrensParent)
                .FontSize(12)
                .FontBold()
                .AddTo(this);


            Refresh();
        }


        void OpenFeatureEditor(Feature feature = null)
        {
            var featureName = feature != null ? feature.Name : string.Empty;
            var featureDescription = feature != null ? feature.Description : string.Empty;


            var featureEditor = Window.CreateSubWindow("编辑 Feature");

            new LabelView("功能名:")
                .AddTo(featureEditor)
                .FontBold()
                .TextMiddleCenter()
                .FontSize(15);

            new TextView(featureName)
                .FontSize(15)
                .Height(20)
                .AddTo(featureEditor)
                .Content.Bind(content=>featureName = content);
            
            new LabelView("描述:")
                .AddTo(featureEditor)
                .FontBold()
                .TextMiddleCenter()
                .FontSize(15);

            new TextAreaView(featureDescription)
                .AddTo(featureEditor)
                .FontSize(15)
                .ExpandHeight()
                .Content.Bind(content=>featureDescription = content);
            
                
            new ButtonView("保存", () =>
                {
                    if (feature == null)
                    {
                        mFeature.Children.Add(new Feature()
                        {
                            Name = featureName,
                            Description =  featureDescription
                        });
                    }
                    else
                    {
                        feature.Name = featureName;
                        feature.Description = featureDescription;
                    }
                    
                    Model.Save();
                    featureEditor.Close();
                    RefreshNextFrame();
                })
                
                .AddTo(featureEditor)
                .FontBold()
                .FontSize(EGOTheme.TEXT_BUTTON_SIZE);
            
            featureEditor.Show();
        }
        

        protected override void OnRefresh()
        {
            mChildrensParent.Clear();

            mTreeNode.Content = " " + mFeature.Name;
            
            mFeature.Children.ForEach(feature =>
            {
                new FeatureView(feature, () =>
                    {
                        this.PushCommand(() =>
                        {
                            mFeature.Children.Remove(feature);
                            Model.Save();
                        });
                    }, IndentValue + 1).AddTo(mChildrensParent);
            });
        }
    }
}