using System;

namespace EGO.V1
{

    [Serializable]
    public class Category
    {
        public string Id = new Guid().ToString();
        
        public string Name;
        
        public string Color;
    }
}