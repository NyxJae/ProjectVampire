using EGO.Framework;
using EGO.Framework.Util;
using EGO.V1;

namespace EGO.ViewController
{
    public class CategoryComponent : View
    {
        private BoxView mBoxView { get; set; }

        public CategoryComponent(Category category)
        {
            Data = category;
        }

        public Category Data
        {
            set
            {
                if (value != null)
                    mBoxView = new BoxView(value.Name).Color(value.Color.ToColor()); 
            }
        }

        protected override void OnGUI()
        {
            mBoxView?.DrawGUI();
        }
    }
}