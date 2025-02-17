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
    public bool isNewProjClicked = false;
    private bool isOldProjClicked = true;

    public ProjectAccess()
    {
        InitializeComponent();
        this.DataContext = this;

        OpenProjectWindow();
    }

    private void OpenProjectWindow()
    {
        var oldProj = this.FindControl<Border>("openOldProject");
        var newProj = this.FindControl<Border>("openNewProject");
        if (isOldProjClicked)
        {
            oldProj.Width = 800;
            newProj.Width = 0;
            openProject.OpenExistingProject();
        }
        isNewProjClicked = false;
        HandleVisibility();
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
                    createProject.NewProjectFunction();
                }
                isOldProjClicked = false;
                HandleVisibility();
            }
            else
            {
                if (!isOldProjClicked)
                {
                    isOldProjClicked = true;
                }
                OpenProjectWindow();
            }
        }
    }

    private void HandleVisibility()
    {
        var tb1 = createProject.FindControl<TextBox>("fileName");
        var tb2 = createProject.FindControl<TextBox>("dirPath");
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
}