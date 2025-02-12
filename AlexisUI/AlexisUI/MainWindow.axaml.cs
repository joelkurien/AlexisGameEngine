using AlexisUI.DataTypes;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace AlexisUI;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async Task openProject()
    {
        var projectAccess = new ProjectAccess();
        await projectAccess.ShowDialog(this);
    }
}