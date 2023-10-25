#if UNITY_EDITOR
using System.IO;
using System.Text;

namespace QFramework.Pro
{
    public class SimpleClassNodeTemplate
    {
        public static void Write(ArchitectureGraph architectureGraph, ClassNode classNode)
        {
            var folderPath = (architectureGraph.ScriptsFolderPath).CreateDirIfNotExists();
            var filePath = folderPath + $"/{classNode.ClassName}.cs";

            var rootCode = new RootCode()
                .Using("QFramework")
                .Using("System")
                .EmptyLine()
                .Namespace(architectureGraph.Namespace,
                    (ns) =>
                    {
                        ns.EmptyLine();
                        ns.Class(classNode.ClassName, null, true , false, classScope =>
                        {
                            // classScope.CustomScope("protected override void OnInit()", false,
                                // method => { method.EmptyLine(); });
                        });
                    });

            var stringWriter = new StringBuilder();
            rootCode.Gen(new StringCodeWriter(stringWriter));

            if (!File.Exists(filePath))
            {
                using (var fileWriter = File.CreateText(filePath))
                {
                    rootCode.Gen(new FileCodeWriter(fileWriter));
                }
            }
        }
    }
}
#endif