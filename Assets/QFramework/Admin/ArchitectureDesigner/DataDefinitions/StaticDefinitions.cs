namespace QFramework.Pro
{
    public enum StaticDefinitions
    {
        None,
        Static,
        Const
    }

    public static class StaticDefinitionsExtension
    {
        public static string ToCode(this StaticDefinitions self)
        {
            if (self == StaticDefinitions.Static)
            {
                return "static ";
            }

            if (self == StaticDefinitions.Const)
            {
                return "const ";
            }
            return "";

        }
    }
}