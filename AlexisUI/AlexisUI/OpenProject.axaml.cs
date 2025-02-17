using AlexisUI.ContentHandling;
using AlexisUI.ContentHandling.Interfaces;
using AlexisUI.DataTypes;
using AlexisUI.EngineUI;
using Avalonia;
using Avalonia.Controls;
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
    public OpenProject()
    {
        InitializeComponent();
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
        if (sender is TreeView treeExplorer
            && treeExplorer.SelectedItem is TreeViewItem treeViewItem
            && treeViewItem.Tag is FileItem selectedProject)
        {
            if (selectedProject != null && !selectedProject.isFolder)
            {
                ProjectValidation.isProjTypeSelected = true;
                var projPreview = this.FindControl<Image>("openProjImage");
                if (projPreview != null)
                {
                    projPreview.Source = new Bitmap(selectedProject.FilePath);
                    projPreview.IsVisible = true;
                }
            }
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


}