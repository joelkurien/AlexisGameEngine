using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexisUI.ContentHandling.Interfaces
{
    public interface FileManagementInterface
    {
        public void AddProjectTypesToTrees(ref TreeView treeExplorer);
        public void CreateProject(string templateType, string projName, string projPath);
    }
}
