using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AlexisUI.EngineUI
{
    [Serializable]
    public class Scene
    {
        [XmlIgnore]
        public Project Project { get; set; }

        [XmlElement("SceneName")]
        public string SceneName { get; set; }
        
        public Scene() { }
        public Scene(Project project, string name)
        {
            Project = project;
            SceneName = name;
        }
    }
}
