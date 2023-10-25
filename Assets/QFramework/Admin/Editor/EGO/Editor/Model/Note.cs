using System;

namespace EGO.V1
{
    [Serializable]
    public class Note
    {
        public string Id;

        public string Content;

        public Note()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}