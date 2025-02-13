using AlexisUI.DataTypes;
using Avalonia;
using AlexisUI.ContentHandling;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using AlexisUI.ContentHandling.Interfaces;

namespace AlexisUI;

public partial class ProjectAccess : Window
{
    private readonly FileManagementInterface _fileManagement = new FileManagement();
    private readonly ProjectValidationInterface _projectValidation = new ProjectValidation();

    private bool isNewProjClicked = false;
    private bool isOldProjClicked = false;
    private static bool addProjTypes = true;
    public string? SelectedTemplate { get; private set; }
    public ProjectAccess()
    {
        InitializeComponent();
        this.DataContext = this;
    }


    private void OnButtonClick(object sender, RoutedEventArgs e)
    {
        Button btn = (Button)sender;
        var newProj = this.FindControl<Border>("openNewProject");
        var oldProj = this.FindControl<Border>("openOldProject");
        if (newProj != null && oldProj != null)
        {
            if (btn.Name == "newProject")
            {
                if (!isNewProjClicked)
                {
                    isNewProjClicked = true;
                    oldProj.Width = 0;
                    newProj.Width = 800;
                    NewProjectFunction();
                }
                isOldProjClicked = false;
                HandleVisibility();
            }
            else
            {
                if (!isOldProjClicked)
                {
                    isOldProjClicked = true;
                    oldProj.Width = 800;
                    newProj.Width = 0;
                }
                isNewProjClicked = false;
                HandleVisibility();
            }
        }
    }

    private void NewProjectFunction()
    {
        #region Add Default Files
        var treeExplorer = this.FindControl<TreeView>("treeExplorer");
        if(addProjTypes && treeExplorer != null)
            _fileManagement.AddProjectTypesToTrees(ref treeExplorer);
        addProjTypes = false;
        #endregion Add Default Files
    }

    private void HandleVisibility()
    {
        var tb1 = this.FindControl<TextBox>("fileName");
        var tb2 = this.FindControl<TextBox>("dirPath");
        if (!isNewProjClicked)
        {
            tb1.IsVisible = false;
            tb2.IsVisible = false;
        }
        else
        {
            tb1.IsVisible = true;
            tb2.IsVisible = true;
        }
    }

    private void DisplaySelectedProject(object? sender, SelectionChangedEventArgs e)
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

    private async void SaveProjectType(object? sender, RoutedEventArgs e)
    {
        var outcome = false;
        TextBox? fileName = this.FindControl<TextBox>("fileName");
        TextBox? dirPath = this.FindControl<TextBox>("dirPath");
        string projName = fileName != null && !string.IsNullOrEmpty(fileName.Text) ? fileName.Text : "";
        string projPath = dirPath != null && !string.IsNullOrEmpty(dirPath.Text) ? dirPath.Text : "";
        if(fileName != null && dirPath != null &&
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