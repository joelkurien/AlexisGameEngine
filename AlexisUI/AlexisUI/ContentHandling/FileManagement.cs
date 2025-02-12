using System;
using AlexisUI.DataTypes;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using System.Collections.Generic;
using System.Linq;
using AlexisUI.ContentHandling.Interfaces;

namespace AlexisUI.ContentHandling;
public class FileManagement : FileManagementInterface
{
    public ObservableCollection<FileItem>? Folders { get; set; }
    public FileManagement() {}

    private List<ObservableCollection<FileItem>> addProjectTypes()
    {
        ObservableCollection<FileItem> selectedFiles = new ObservableCollection<FileItem>();
        ObservableCollection<FileItem> files = new ObservableCollection<FileItem>
        {
            new FileItem("First Person Game", "C:\\Users\\susan\\OneDrive\\Pictures\\Screenshots\\Screenshot (10).png"),
            new FileItem("2D Game", "C:\\Users\\susan\\OneDrive\\Pictures\\Screenshots\\Screenshot (16).png"),
            new FileItem("3D Game", "C:\\Users\\susan\\OneDrive\\Pictures\\Screenshots\\Screenshot (25).png"),
        };

        var moth = files.Last().Files?.Last();
        if (moth != null) selectedFiles.Add(moth);
        return new List<ObservableCollection<FileItem>> { files, selectedFiles };
    }

    public void addProjectTypesToTrees(ref TreeView treeExplorer)
    {
        var fileSets = addProjectTypes();
        Folders = fileSets[0];
        foreach (var folder in Folders)
        {
            var folderItem = new TreeViewItem
            {
                Header = folder.FileName,
                Tag = folder
            };
            if (folder.Files != null)
            {
                foreach (var file in folder.Files)
                {
                    var fileItem = new TreeViewItem
                    {
                        Header = file.FileName,
                        Tag = file
                    };
                    folderItem.Items.Add(fileItem);
                }
            }
            treeExplorer.Items.Add(folderItem);
        }
    }

    public void SaveAsAlxFile()
    {

    }
}
