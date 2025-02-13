using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AlexisUI.EngineUI
{
    [DataContract]
    public class Scene
    {
        [DataMember]
        public string ProjectName { get; private set; }

        [DataMember]
        public string SceneName { get; private set; }

        [DataMember]
        public string ScenePath { get; private set; }

        public Scene()
        {
            
        }
    }
}
