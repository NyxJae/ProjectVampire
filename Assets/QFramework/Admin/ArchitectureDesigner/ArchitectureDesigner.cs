#if UNITY_EDITOR
using UnityEditor;

namespace QFramework.Pro
{
    public class ArchitectureDesigner : Architecture<ArchitectureDesigner>
    {
        public static void GenerateCode(ArchitectureGraph architectureGraph)
        {
            
            foreach (var system in architectureGraph.Systems)
            {
                SystemCodeTemplate.Write(architectureGraph, system);
                SystemCodeDesignerTemplate.Write(architectureGraph, system);
            }
            
            foreach (var model in architectureGraph.Models)
            {
                ModelCodeTemplate.Write(architectureGraph, model);
                ModelCodeDesignerTemplate.Write(architectureGraph, model);
            }

            foreach (var command in architectureGraph.Commands)
            {
                CommandCodeTemplate.Write(architectureGraph, command);
            }
            
            foreach (var simpleClassNode in architectureGraph.SimpleClassNodes)
            {
                SimpleClassNodeTemplate.Write(architectureGraph, simpleClassNode);
                SimpleClassNodeDesignerTemplate.Write(architectureGraph, simpleClassNode);
            }
            
            // 最后是 architecture
            ArchitectureCodeTemplate.Write(architectureGraph);
            ArchitectureDesignerCodeTemplate.Write(architectureGraph);
#if UNITY_EDITOR
            AssetDatabase.Refresh();
#endif
        }

        protected override void Init()
        {
        }
    }
}
#endif