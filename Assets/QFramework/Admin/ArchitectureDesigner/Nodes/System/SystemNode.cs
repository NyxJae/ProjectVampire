#if UNITY_EDITOR

namespace QFramework.Pro
{
    [CreateNodeMenu("System")]
    public class SystemNode : ClassNode
    {
        public bool WithInterface = true;

        public override string ClassName => (Name != null && Name.EndsWith("System")) ? Name : Name + "System";

    }
}
#endif