using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AlexisUI.EngineUI
{
    [Serializable]
    public class Project
    {
        [XmlElement("Projct")]
        public string ProjectName { get; set; }
        
        [XmlElement("Project Location")]
        public string ProjectPath { get; set; }

        private ObservableCollection<Scene> _scenes = new ObservableCollection<Scene>();

        [XmlElement("Scenes")]
        public ObservableCollection<Scene> Scenes { get { return _scenes; } set { _scenes = value; } }

        public Project() { }
        public Project(string projName, string projPath)
        {
            ProjectName = projName;
            ProjectPath = projPath;
            _scenes.Add(new Scene(this, "Base Scene"));
        }
    }
}
