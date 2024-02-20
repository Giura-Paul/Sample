using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Sample.App.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Sample.App;
public sealed partial class AddPersonDialog : ContentDialog
{
    public AddPersonViewModel AddPersonViewModel { get; set; }

    public AddPersonDialog(AddPersonViewModel addPersonViewModel, XamlRoot xamlRoot)
    {
        InitializeComponent();
        AddPersonViewModel = addPersonViewModel;
        AddPersonViewModel.DialogHandler = new DialogHandler(this);
        XamlRoot = xamlRoot;
    }
}
