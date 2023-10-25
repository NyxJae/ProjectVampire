#if UNITY_EDITOR
using System.IO;

using System.Text;
using QFramework.Pro;

namespace QFramework
{
    public class ArchitectureDesignerCodeTemplate
    {
        public static void Write(ArchitectureGraph architectureGraph)
        {
            var folderPath = architectureGraph.ScriptsFolderPath.CreateDirIfNotExists();
            var filePath = folderPath + $"/{architectureGraph.ProjectName}.Designer.cs";

            var rootCode = new RootCode()
                .Using("QFramework")
                .Using("System")
                .EmptyLine()
                .Namespace(architectureGraph.Namespace,
                    (ns) =>
                    {
                        ns.Class(architectureGraph.ProjectName, $"Architecture<{architectureGraph.ProjectName}>", true, false, classScope =>
                        {
                            foreach (var bindablePropertyNode in architectureGraph.BindableProperties)
                            {
                                classScope.Custom(bindablePropertyNode.ToCode());
                            }

                            classScope.EmptyLine();
                            classScope.CustomScope("protected override void Init()", false, method =>
                            {
                                
                                foreach (var systemNode in architectureGraph.Systems)
                                {
                                                                    
                                    if (systemNode.WithInterface)
                                    {
                                        method.Custom(
                                            $"this.RegisterSystem<I{systemNode.ClassName}>(new {systemNode.ClassName}());");
                                    }
                                    else
                                    {
                                        method.Custom($"this.RegisterSystem(new {systemNode.ClassName}());");

                                    }
                                }
                                
                                foreach (var modelInfo in architectureGraph.Models)
                                {
                                    if (modelInfo.WithInterface)
                                    {
                                        method.Custom(
                                            $"this.RegisterModel<I{modelInfo.ClassName}>(new {modelInfo.ClassName}());");
                                    }
                                    else
                                    {
                                        method.Custom($"this.RegisterModel(new {modelInfo.ClassName}());");

                                    }
                                }

                                method.Custom("OnInit();");
                            });
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