#if UNITY_EDITOR
using System.IO;
using System.Text;

namespace QFramework.Pro
{
    public class SystemCodeDesignerTemplate
    {
        public static void Write(ArchitectureGraph architectureGraph, SystemNode systemNode)
        {
            var folderPath = (architectureGraph.ScriptsFolderPath + "/System/" + systemNode.ClassName).CreateDirIfNotExists();
            var filePath = folderPath + $"/{systemNode.ClassName}.Designer.cs";

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
                                (interfaceScope) =>
                                {
                                    foreach (var propertyInfo in systemNode.Properties)
                                    {
                                        interfaceScope.Custom(propertyInfo.Type.ToString().ToLower() + " " +
                                                              propertyInfo.Name +
                                                              " { get;set; }");
                                    }
                                });
                        }

                        ns.EmptyLine();
                        ns.Class(systemNode.ClassName, $"I{systemNode.ClassName}", true, false, classScope =>
                        {
                            foreach (var propertyInfo in systemNode.Properties)
                            {
                                classScope.Custom(propertyInfo.AccessType.ToString().ToLower() + " " +
                                                  propertyInfo.Type.ToString().ToLower() + " " + propertyInfo.Name +
                                                  " { get;set; }");
                            }
                        });
                    });

            var stringWriter = new StringBuilder();
            rootCode.Gen(new StringCodeWriter(stringWriter));

            using (var fileWriter = File.CreateText(filePath))
            {
                rootCode.Gen(new FileCodeWriter(fileWriter));
            }
        }
    }
}
#endif