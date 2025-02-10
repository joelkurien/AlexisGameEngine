using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace AlexisUI;

public partial class ProjectPathDialog : Window
{
    public ProjectPathDialog()
    {
        InitializeComponent();
    }

    private void OnCancel(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}