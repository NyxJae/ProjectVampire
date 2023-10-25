#if UNITY_EDITOR
using System.IO;
using System.Text;

namespace QFramework.Pro
{
    public class SystemCodeTemplate
    {
        public static void Write(ArchitectureGraph architectureGraph, SystemNode systemNode)
        {
            var folderPath = (architectureGraph.ScriptsFolderPath + "/System/" + systemNode.ClassName).CreateDirIfNotExists();
            var filePath = folderPath + $"/{systemNode.ClassName}.cs";

            var rootCode = new RootCode()
                .Using("QFramework")
                .Using("System")
                .EmptyLine()
                .Namespace(architectureGraph.Namespace,
                    (ns) =>
                    {
                        if (systemNode.WithInterface)
                        {
                            ns.CustomScope($"public partial interface I{systemNode.ClassName} : ISystem", false,
                                (interfaceScope) => { });
                        }

                        ns.EmptyLine();
                        ns.Class(systemNode.ClassName, "AbstractSystem", systemNode.WithInterface , false, classScope =>
                        {

                            classScope.CustomScope("protected override void OnInit()", false,
                                method => { method.EmptyLine(); });
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