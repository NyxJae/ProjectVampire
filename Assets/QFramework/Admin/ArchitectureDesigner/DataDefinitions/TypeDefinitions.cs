namespace QFramework.Pro
{
    public enum TypeDefinitions
    {
        Int = 0,
        Float = 2,
        Bool = 5,
        String = 1,
        Double = 3,
        Custom = 4,
    }
    
    public static class TypeDefinitionsExtension
    {
        public static string ToCode(this TypeDefinitions self)
        {
            if (self == TypeDefinitions.Int) return "int";
            if (self == TypeDefinitions.String) return "string";
            if (self == TypeDefinitions.Float) return "float";
            if (self == TypeDefinitions.Double) return "double";
            if (self == TypeDefinitions.Bool) return "bool";
            return "";
        }
    }
}