using System;
using AlexisUI.DataTypes;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using System.Collections.Generic;
using System.Linq;
using AlexisUI.ContentHandling.Interfaces;
using System.IO;
using System.Text.Json;
using AlexisUI.EngineUI;
using System.Security;

namespace AlexisUI.ContentHandling;
public class FileManagement : FileManagementInterface
{
    private readonly ProjectValidationInterface _projectValidate = new ProjectValidation();
    public ObservableCollection<FileItem>? Folders { get; set; }
    public FileManagement() {}

    private List<ObservableCollection<FileItem>> AddProjectTypes()
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

    public void AddProjectTypesToTrees(ref TreeView treeExplorer)
    {
        var fileSets = AddProjectTypes();
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

    public async void CreateProject(string templateType, string projName, string projPath)
    {
        if(await _projectValidate.ValidateProjectCreation(projName, projPath))
        {
            DirectoryInfo projDir = Directory.CreateDirectory(Path.Combine(projPath, projName));
            switch (templateType)
            {
                case "First Person Game":
                    var sourceImage = "C:\\Users\\susan\\OneDrive\\Pictures\\Screenshots\\Screenshot (10).png";
                    string newImage = Path.Combine(projDir.FullName, Path.GetFileName(sourceImage));
                    File.Copy(sourceImage, newImage, overwrite: true);
                    CreateProjectAsAlx(projName, projDir);
                    break;
                case "2D Game":
                    break;
                case "3D Game":
                    break;
            }
        }
    }

    public void CreateProjectAsAlx(string projName, DirectoryInfo projDir)
    {
        string projFile = Path.Combine(projDir.FullName, projName + ".alx");
        Project project = new Project(projFile);
        string projJson = JsonSerializer.Serialize(project, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(projFile, projJson);
    }

}
