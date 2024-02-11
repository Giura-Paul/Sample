using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Sample.App.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Sample.App
{
    public sealed partial class EditPersonDialog : ContentDialog
    {
        public EditPersonViewModel EditPersonViewModel { get; set; }

        public EditPersonDialog(EditPersonViewModel editPersonViewModel)
        {
            InitializeComponent();
            EditPersonViewModel = editPersonViewModel;
            EditPersonViewModel.DialogHandler = new DialogHandler(this);
        }
    }
}
