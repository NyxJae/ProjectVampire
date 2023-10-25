using System.Collections.Generic;
using System.Xml;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public enum TestEvents
    {
        A,
        B,
    }

    [System.Serializable]
    public class XMLTest
    {
        [SerializeField]
        public List<string> List = new List<string>(){"1","2","3"};
    }
    
    public class Playground
    {
        // A Test behaves as an ordinary method
        [Test]
        public void PlaygroundSimplePasses()
        {
            var filePath = Application.dataPath + "/EGO/Editor/Resources/config/questions.xml";

            var doc = new XmlDocument();
            doc.Load(filePath);
            
            var element =doc.DocumentElement;
            
            foreach (var childNode in element.ChildNodes)
            {
                var xmlElement = childNode as XmlElement;

                if (xmlElement.Name == "Question")
                {
                    foreach (XmlElement questionParam in xmlElement.ChildNodes)
                    {
                        Debug.Log(questionParam.Name);
                    }
                }
                else if (xmlElement.Name == "Choice")
                {
                    
                }
            }
            
        }
    }
}
