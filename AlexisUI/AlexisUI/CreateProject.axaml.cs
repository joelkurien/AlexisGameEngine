using AlexisUI.ContentHandling;
using AlexisUI.ContentHandling.Interfaces;
using AlexisUI.DataTypes;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using System;
using System.Diagnostics;

namespace AlexisUI;

public partial class CreateProject : UserControl
{
    private readonly FileManagementInterface _fileManagement = new FileManagement();
    private readonly ProjectValidationInterface _projectValidation = new ProjectValidation();

    private static bool addProjTypes = true;
    public string? SelectedTemplate { get; private set; }

    public CreateProject()
    {
        InitializeComponent();
    }

    public void NewProjectFunction()
    {
        try
        {
            #region Add Default Files
            var treeExplorer = this.FindControl<TreeView>("treeExplorer");
            if (addProjTypes && treeExplorer != null)
                _fileManagement.AddProjectTypesToTree(ref treeExplorer);
            addProjTypes = false;
            #endregion Add Default Files
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }

    }

    private void DisplaySelectedProject(object? sender, SelectionChangedEventArgs e)
    {
        try
        {
            if (sender is TreeView treeExplorer
            && treeExplorer.SelectedItem is TreeViewItem treeViewItem
            && treeViewItem.Tag is FileItem selectedFile)
            {
                if (selectedFile != null && !selectedFile.isFolder)
                {
                    ProjectValidation.isProjTypeSelected = true;
                    SelectedTemplate = selectedFile.FileName;
                    var projPreview = this.FindControl<Image>("projPreview");
                    if (projPreview != null)
                    {
                        projPreview.Source = new Bitmap(selectedFile.FilePath);
                        projPreview.IsVisible = true;
                    }
                }
            }
        }
        catch (Exception ex) 
        { Debug.WriteLine(ex); }
    }

    private async void SaveProjectType(object? sender, RoutedEventArgs e)
    {
        var outcome = false;
        TextBox? fileName = this.FindControl<TextBox>("fileName");
        TextBox? dirPath = this.FindControl<TextBox>("dirPath");
        string projName = fileName != null && !string.IsNullOrEmpty(fileName.Text) ? fileName.Text : "";
        string projPath = dirPath != null && !string.IsNullOrEmpty(dirPath.Text) ? dirPath.Text : "";
        if (fileName != null && dirPath != null &&
            fileName.Text != null && dirPath.Text != null)
        {
            if (await _projectValidation.ValidateProjectCreation(fileName.Text, dirPath.Text))
            {
                outcome = true;
            }
        }

        #region Validation on Project Create
        var errorMsgBlock = this.FindControl<TextBlock>("CreateErrorMsg");
        if (!outcome)
        {
            errorMsgBlock.Text = "Unable to create file in this location. Please Try Again";
            errorMsgBlock.IsVisible = true;
        }
        else
        {
            errorMsgBlock.IsVisible = false;
        }
        #endregion Validation on Project Create

        if (SelectedTemplate != null)
        {
            _fileManagement.CreateProject(SelectedTemplate, projName, projPath);
        }
    }

}