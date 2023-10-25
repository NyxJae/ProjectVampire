using System;
using System.Collections.Generic;
using System.Linq;
using QFramework;
using UnityEngine;

namespace UTGM
{
    [DisallowMultipleComponent]
    public class DeclareObject : MonoBehaviour
    {
        public string[] ClassNames;

        public string ID;

        private void Start()
        {
            AttachRules(this);
        }

        public static void AttachRules(DeclareObject declareObject)
        {
            foreach (var className in declareObject.ClassNames)
            {
                foreach (var declareRuleItem in DeclareRuleTable.ClassNameIndex.Get(className))
                {
                    declareRuleItem.OnRule(declareObject);
                }
            }
        }

        public static DeclareRuleTable DeclareRuleTable = new DeclareRuleTable();
        
        public static IUnRegister RegisterClassRule(string className, Action<DeclareObject> onRule)
        {
            var declareRuleItem = new DeclareRuleItem()
            {
                ClassName = className,
                OnRule = onRule
            };
                
            DeclareRuleTable.Add(declareRuleItem);

            return new CustomUnRegister(() =>
            {
                DeclareRuleTable.Remove(declareRuleItem);
            });
        }
    }

    public class DeclareRuleTable : Table<DeclareRuleItem>
    {
        public TableIndex<string, DeclareRuleItem> ClassNameIndex =
            new TableIndex<string, DeclareRuleItem>(item => item.ClassName);
        protected override void OnAdd(DeclareRuleItem item)
        {
            ClassNameIndex.Add(item);
        }

        protected override void OnRemove(DeclareRuleItem item)
        {
            ClassNameIndex.Remove(item);
        }

        protected override void OnClear()
        {
            ClassNameIndex.Clear();
        }

        public override IEnumerator<DeclareRuleItem> GetEnumerator()
        {
            return ClassNameIndex.Dictionary.Values.SelectMany(d => d).GetEnumerator();
        }

        protected override void OnDispose()
        {
        }
    }

    public class DeclareRuleItem
    {
        public string ClassName;
        public Action<DeclareObject> OnRule;
    }
}
