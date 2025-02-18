using AlexisUI.ContentHandling;
using AlexisUI.ContentHandling.Interfaces;
using AlexisUI.DataTypes;
using AlexisUI.EngineUI;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;

namespace AlexisUI;

public partial class OpenProject : UserControl
{
    private readonly string metadataDir = Path.Combine(FileConstants.HomePath, "Metadata");
    private readonly FileManagementInterface _fileManagement = new FileManagement();

    private string _projectFile;
    public OpenProject()
    {
        InitializeComponent();
        _projectFile = "";
    }

    public void OpenExistingProject()
    {
        #region Add Project Files
        var projExplorer = this.FindControl<TreeView>("projectExplorer");
        if (projExplorer != null)
            AddProjectFilesToTrees(ref projExplorer);
        #endregion Add Project Files
    }

    private void DisplaySelectedOpenProject(object? sender, SelectionChangedEventArgs e)
    {
        try
        {
            if (sender is TreeView treeExplorer
            && treeExplorer.SelectedItem is TreeViewItem treeViewItem
            && treeViewItem.Tag is FileItem selectedProject)
            {
                if (selectedProject != null && !selectedProject.isFolder)
                {
                    string? projDir = Path.GetDirectoryName(Path.GetFullPath(selectedProject.FilePath));
                    if (!string.IsNullOrEmpty(projDir))
                    {
                        var projFiles = Directory.GetFiles(projDir);
                        var screenshotFile = "";
                        foreach (var projFile in projFiles)
                        {
                            if (projFile != null && Path.GetExtension(projFile) != ".alx")
                            {
                                string imageName = Path.GetFileName(projFile);
                                screenshotFile += imageName == FileConstants.FPSScreenshot || imageName == FileConstants.ThreeDScreenshot || imageName == FileConstants.TwoDScreenshot ? projFile : "";
                            }
                            else if(!string.IsNullOrEmpty(projFile) && Path.GetExtension(projFile) == ".alx")
                            {
                                _projectFile = projFile;
                            }
                        }
                        ProjectValidation.isProjTypeSelected = true;
                        var projPreview = this.FindControl<Image>("openProjImage");
                        if (projPreview != null)
                        {
                            projPreview.Source = new Bitmap(screenshotFile);
                            projPreview.IsVisible = true;
                        }
                    }
                }
            }
        }
        catch(Exception ex)
        {
            Debug.WriteLine(ex);
        }
    }

    private void AddProjectFilesToTrees(ref TreeView projExplorer)
    {
        var projectFilePaths = GetAllProjectAlxFiles();
        if (projectFilePaths != null && projectFilePaths.Count > 0) {
            _fileManagement.AddFilesToTrees(ref projExplorer, projectFilePaths);
        }
    }

    private ObservableCollection<FileItem> GetAllProjectAlxFiles()
    {
        ObservableCollection<FileItem> projectFilePaths = new ObservableCollection<FileItem>();
        try
        {
            if (Directory.Exists(metadataDir))
            {
                var metaDataFiles = Directory.GetFiles(metadataDir);
                foreach (var metadata in metaDataFiles)
                {
                    var metadataContent = Serializer.ReadMetadataFile(metadata);
                    if (metadataContent != null && metadataContent.ContainsKey("Project") && metadataContent.ContainsKey("ProjectPath"))
                    {
                        projectFilePaths.Add(new FileItem(metadataContent["Project"], metadataContent["ProjectPath"]));
                    }
                }
            }
        }
        catch(Exception ex)
        {
            Debug.Write(ex.ToString());
        }
        return projectFilePaths;
    }

    private void OpenSelectedProject(object? sender, RoutedEventArgs e)
    {
        Project project = Serializer.DeserializeXML<Project>(_projectFile);
        Debug.WriteLine(project.ProjectName);
    }
}