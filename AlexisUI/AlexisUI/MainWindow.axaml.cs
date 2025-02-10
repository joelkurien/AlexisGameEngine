using AlexisUI.DataTypes;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AlexisUI;

public partial class MainWindow : Window
{
    private bool isNewProjClicked = false;
    private bool isOldProjClicked = false;

    public MainWindow()
    {
        InitializeComponent();
        this.DataContext = this;
    }

    private async void OnButtonClick(object sender, RoutedEventArgs e)
    {
        Button btn = (Button)sender;
        var newProj = this.FindControl<Border>("openNewProject");
        var oldProj = this.FindControl<Border>("openOldProject");
        if (btn.Name == "newProject")
        {
            if (!isNewProjClicked)
            {
                isNewProjClicked = true;
                oldProj.Width = 0;
                newProj.Width = 800;
                newProjectFunction();
            }
            isOldProjClicked = false;
        } 
        else
        {
            if (!isOldProjClicked)
            {
                isOldProjClicked = true;
                oldProj.Width = 800;
                newProj.Width = 0;

                var projectDialog = new ProjectPathDialog();
                await projectDialog.ShowDialog(this);
            }
            isNewProjClicked = false;
        }
    }

    private void newProjectFunction()
    {
        #region Add Default Files
        var fileManagement = new FileManagement(this);
        var treeExplorer = this.FindControl<TreeView>("treeExplorer");
        fileManagement.addFolderTrees(ref treeExplorer);
        #endregion Add Default Files
    }

    private void DisplaySelectedFile(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is TreeView treeExplorer 
            && treeExplorer.SelectedItem is TreeViewItem treeViewItem
            && treeViewItem.Tag is FileItem selectedFile)
        {
            if (selectedFile != null && !selectedFile.isFolder)
            {
                var ideViewer = this.FindControl<Image>("ideViewer");
                if (ideViewer != null)
                {
                    ideViewer.Source = new Bitmap(selectedFile.FilePath);
                    ideViewer.IsVisible = true;
                }
            }
        }
    }
}