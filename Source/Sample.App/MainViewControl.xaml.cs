using Microsoft.UI.Xaml.Controls;
using Sample.App.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Sample.App;

public sealed partial class MainViewControl : UserControl, IXamlRootProvider
{
    public MainViewModel MainViewModel { get; set; }

    public MainViewControl()
    {
        InitializeComponent();
        MainViewModel = new MainViewModel(this);
        Loaded += (s, e) => MainViewModel.LoadPersonsCommand.Execute(null);
    }
}
