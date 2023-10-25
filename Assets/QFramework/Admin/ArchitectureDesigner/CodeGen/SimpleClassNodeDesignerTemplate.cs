#if UNITY_EDITOR
using System.IO;
using System.Text;

namespace QFramework.Pro
{
    public class SimpleClassNodeDesignerTemplate
    {
           public static void Write(ArchitectureGraph architectureGraph, ClassNode classNode)
        {
            var folderPath = (architectureGraph.ScriptsFolderPath).CreateDirIfNotExists();
            var filePath = folderPath + $"/{classNode.ClassName}.Designer.cs";

            var rootCode = new RootCode()
                .Using("QFramework")
                .Using("System")
                .EmptyLine()
                .Namespace(architectureGraph.Namespace,
                    (ns) =>
                    {

                        ns.EmptyLine();
                        ns.Class(classNode.ClassName, null, true, false, classScope =>
                        {
                            foreach (var propertyInfo in classNode.Properties)
                            {
                                classScope.Custom(propertyInfo.ToCode());
                                // classScope.Custom(propertyInfo.AccessType.ToString().ToLower() + " " +
                                                  // propertyInfo.Type.ToString().ToLower() + " " + propertyInfo.Name +
                                                  // " { get;set; }");
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