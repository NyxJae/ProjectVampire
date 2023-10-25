/****************************************************************************
 * Copyright (c) 2021.4 ~ 2022 liangxiegame MIT License
 * 
 * https://qframework.cn
 * https://github.com/liangxiegame/QFramework
 * https://gitee.com/liangxiegame/QFramework
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace QFramework
{
    public class EventBrowser : EasyEditorWindow
    {
        public static void Open(Action<Type> onTypeClick)
        {
            Create<EventBrowser>(true)
                .OnTypeClick(onTypeClick)
                .Show();
        }

        private Action<Type> mOnTypeClick;

        private EventBrowser OnTypeClick(Action<Type> onTypeClick)
        {
            mOnTypeClick = onTypeClick;
            return this;
        }

        public override void OnClose()
        {
        }

        public override void OnUpdate()
        {
        }

        private string mSearchContent = string.Empty;

        public override void OnGUI()
        {
            EditorGUI.BeginChangeCheck();
            mSearchContent = EditorGUILayout.TextField(mSearchContent);
            if (EditorGUI.EndChangeCheck())
            {
                if (string.IsNullOrEmpty(mSearchContent))
                {
                    AllEventViews.ForEach(a => a.Item2.Visible = true);
                }
                else
                {
                    AllEventViews.ForEach(a => a.Item2.Visible = a.Item1.ToLower().Contains(mSearchContent.ToLower()));
                }
            }


            base.OnGUI();
        }


        List<ActionTuple<string, IMGUIButton>> AllEventViews = new List<ActionTuple<string, IMGUIButton>>();

        protected override void Init()
        {
            var scroll = EasyIMGUI.Scroll();

            foreach (var group in EventTypeDB.GetAll()
                         .Where(t => t.HasAttribute<OnlyUsedByCodeAttribute>()).GroupBy(t =>
                         {
                             var attribute = t.GetAttribute<ActionGroupAttribute>();

                             return attribute != null ? attribute.GroupName : "未分组";
                         })
                         .OrderBy(g => g.Key == "未分组"))
            {
                var treeNode = new TreeNode(true, group.Key);

                foreach (var type in @group.OrderBy(t => t.Name))
                {
                    var actionType = type;
                    treeNode.Add2Spread(EasyIMGUI.Button()
                        .OnClick(() =>
                        {
                            mOnTypeClick(actionType);
                            Close();
                        })
                        .Text(type.Name)
                        .Self(button => AllEventViews.Add(new ActionTuple<string, IMGUIButton>(type.Name, button))));
                }

                scroll.AddChild(treeNode);
            }

            this.AddChild(scroll);
        }
    }
}