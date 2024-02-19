using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Sample.App.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Sample.App;

public sealed partial class EditPersonDialog : ContentDialog
{
    public EditPersonViewModel EditPersonViewModel { get; set; }

    public EditPersonDialog(EditPersonViewModel editPersonViewModel, XamlRoot xamlRoot)
    {
        InitializeComponent();
        EditPersonViewModel = editPersonViewModel;
        EditPersonViewModel.DialogHandler = new DialogHandler(this);
        XamlRoot = xamlRoot;
    }

    private void OnClickClosed(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        EditPersonViewModel?.DialogHandler?.Close();
    }
}
