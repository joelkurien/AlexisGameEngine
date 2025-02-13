using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AlexisUI.EngineUI
{
    [DataContract]
    public class Project
    {
        [DataMember]
        public string ProjectName { get; private set; }
        
        [DataMember]
        public string ProjectPath { get; private set; }

        private ObservableCollection<Scene> _scenes;

        [DataMember]
        public ObservableCollection<Scene> Scenes { get; private set; }

        public Project(string projPath)
        {
            ProjectPath = projPath;
        }
    }
}
