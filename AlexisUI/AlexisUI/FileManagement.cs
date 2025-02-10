using System;
using AlexisUI.DataTypes;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using System.Collections.Generic;
using System.Linq;

namespace AlexisUI;
public class FileManagement
{
	public ObservableCollection<FileItem>? Folders { get; set; }
    private MainWindow mainWindow;
    public FileManagement(MainWindow mainWindow)
	{
        this.mainWindow = mainWindow;
	}

    private List<ObservableCollection<FileItem>> addDefaultFiles()
    {
        ObservableCollection<FileItem> selectedFiles = new ObservableCollection<FileItem>();
        ObservableCollection<FileItem> files = new ObservableCollection<FileItem>
        {
            new FileItem("Scene1", "C:\\Users\\susan\\OneDrive\\Pictures\\Screenshots\\Screenshot (10).png"),
            new FileItem("Scene2", "C:\\Users\\susan\\OneDrive\\Pictures\\Screenshots\\Screenshot (16).png"),
            new FileItem("Scene Set", new ObservableCollection<FileItem>
            {
                new FileItem("Scene3", "C:\\Users\\susan\\OneDrive\\Pictures\\Screenshots\\Screenshot (25).png"),
                new FileItem("Scene4", "C:\\Users\\susan\\OneDrive\\Pictures\\Screenshots\\Screenshot (26).png")
            })
        };

        var moth = files.Last().Files?.Last();
        if (moth != null) selectedFiles.Add(moth);
        return new List<ObservableCollection<FileItem>> { files, selectedFiles };
    }

    public void addFolderTrees(ref TreeView treeExplorer)
    {
        var fileSets = addDefaultFiles();
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
}
