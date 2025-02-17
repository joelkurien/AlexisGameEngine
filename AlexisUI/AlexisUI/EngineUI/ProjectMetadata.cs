using Avalonia.Automation.Peers;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AlexisUI.EngineUI
{
    [Serializable]
    public class ProjectMetadata
    {
        [XmlElement("Project")]
        public string ProjectName { get; set; }

        [XmlElement("ProjectPath")]
        public string ProjectPath { get; set; }

        [XmlElement("LastUpdate")]
        public DateTime LastUpdate { get; set; }

        public ProjectMetadata() { }
        public ProjectMetadata(string projectName, string projectPath)
        {
            ProjectName = projectName;
            ProjectPath = projectPath;
            LastUpdate = DateTime.Now;
        }

        public ProjectMetadata(string projctName, string projectPath, DateTime lastUpdate)
        {
            ProjectName = projctName;
            ProjectPath = projectPath;
            LastUpdate = lastUpdate;
        }
    }
}
