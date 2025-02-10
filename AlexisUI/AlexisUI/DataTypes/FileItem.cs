using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexisUI.DataTypes
{
    public partial class FileItem
    {

        public bool isFolder = false;
        public ObservableCollection<FileItem>? Files { get; set; }
        public string FileName { get; }
        public string FilePath { get; }

        public FileItem(string fileName, string filePath) {
            FileName = fileName;
            FilePath = filePath;
        }

        public FileItem(string folderName, ObservableCollection<FileItem> files)
        {
            FileName = folderName;
            //FilePath = folderPath;
            Files = files;
            isFolder = true;
        }
    }
}
