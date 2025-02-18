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
using System.Diagnostics;
using System.Threading.Tasks;

namespace AlexisUI.ContentHandling;
public class FileManagement : FileManagementInterface
{
    private readonly ProjectValidationInterface _projectValidate = new ProjectValidation();
    public ObservableCollection<FileItem>? Folders { get; set; }
    private readonly string _metadataDirectory = Path.Combine(FileConstants.HomePath, "Metadata");

    #region Add Project Types
    private ObservableCollection<FileItem> AddProjectTypes()
    {
        ObservableCollection<FileItem> selectedFiles = new ObservableCollection<FileItem>();
        ObservableCollection<FileItem> files = new ObservableCollection<FileItem>
        {
            new FileItem("First Person Game", "C:\\Users\\susan\\OneDrive\\Pictures\\Screenshots\\Screenshot (10).png"),
            new FileItem("2D Game", "C:\\Users\\susan\\OneDrive\\Pictures\\Screenshots\\Screenshot (16).png"),
            new FileItem("3D Game", "C:\\Users\\susan\\OneDrive\\Pictures\\Screenshots\\Screenshot (25).png"),
        };
        return files;
    }
    #endregion Add Project Types

    #region Add Files to a Folder Tree
    public void AddFilesToTrees(ref TreeView tree, ObservableCollection<FileItem> fileSet)
    {
        Folders = fileSet;
        tree.Items.Clear();
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
            tree.Items.Add(folderItem);
        }
    }

    public void AddProjectTypesToTree(ref TreeView tree)
    {
        AddFilesToTrees(ref tree, AddProjectTypes());
    }
    #endregion Add Files to a Folder Tree

    #region Create Project
    public async Task<string> CreateProject(string templateType, string projName, string projPath)
    {
        string projLoc = "";
        try { 
            if (await _projectValidate.ValidateProjectCreation(projName, projPath))
            {
                DirectoryInfo projDir = Directory.CreateDirectory(Path.Combine(projPath, projName));
                if (templateType == "First Person Game")
                {
                    var sourceImage = "C:\\Users\\susan\\OneDrive\\Pictures\\Screenshots\\" + FileConstants.FPSScreenshot;
                    string newImage = Path.Combine(projDir.FullName, Path.GetFileName(sourceImage));
                    File.Copy(sourceImage, newImage, overwrite: true);
                    projLoc = CreateProjectAsAlx(projName, projDir);
                }
                else if (templateType == "3D Game")
                {
                    var sourceImage = "C:\\Users\\susan\\OneDrive\\Pictures\\Screenshots\\" + FileConstants.ThreeDScreenshot;
                    string newImage = Path.Combine(projDir.FullName, Path.GetFileName(sourceImage));
                    File.Copy(sourceImage, newImage, overwrite: true);
                    projLoc = CreateProjectAsAlx(projName, projDir);
                }
                else if (templateType == "2D Game")
                {
                    var sourceImage = "C:\\Users\\susan\\OneDrive\\Pictures\\Screenshots\\" + FileConstants.TwoDScreenshot;
                    string newImage = Path.Combine(projDir.FullName, Path.GetFileName(sourceImage));
                    File.Copy(sourceImage, newImage, overwrite: true);
                    projLoc = CreateProjectAsAlx(projName, projDir);
                }
            }
        }
        catch(Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return projLoc;
    }

    public string CreateProjectAsAlx(string projName, DirectoryInfo projDir)
    {
        string projFile = Path.Combine(projDir.FullName, projName + ".alx");
        Project project = new Project(projName, projFile);
        Serializer.SaveFile<Project>(project, projFile);
        ProjectMetadata projectMetadata = new ProjectMetadata(projName, projFile);
        string metadataPath = CreateMetadataFile(projName, projFile);
        return projFile;
    }

    public string CreateMetadataFile(string projName, string projPath)
    {
        string metadataPath = "";
        try
        {
            if (!Directory.Exists(_metadataDirectory))
            {
                DirectoryInfo dirInfo = Directory.CreateDirectory(_metadataDirectory);
            }
            metadataPath = Path.Combine(_metadataDirectory, projName + ".alxn");
            ProjectMetadata projectMetadata = new ProjectMetadata(projName, projPath);
            Serializer.SaveFile<ProjectMetadata>(projectMetadata, metadataPath);
        }
        catch (Exception ex)
        {
            Debug.Write(ex.ToString());
        }
        return metadataPath;
    }
    #endregion Create Project
}
