using AlexisUI.DataTypes;
using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexisUI.ContentHandling.Interfaces
{
    public interface FileManagementInterface
    {
        public void AddFilesToTrees(ref TreeView tree, ObservableCollection<FileItem> fileSet);
        public Task<string> CreateProject(string templateType, string projName, string projPath);
        public void AddProjectTypesToTree(ref TreeView tree);
    }
}
