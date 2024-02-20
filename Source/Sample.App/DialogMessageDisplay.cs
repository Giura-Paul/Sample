using Microsoft.UI.Xaml;
using Windows.UI.Popups;
using WinRT.Interop;

namespace Sample.App.ViewModels;

public static class DialogMessageDisplay
{
    public static async Task ShowMessageDialogAsync(string title, string message)
    {
        nint handle = WinRT.Interop.WindowNative.GetWindowHandle(((App)Application.Current).MainWindow);
        var dialog = new MessageDialog(message, title);
        InitializeWithWindow.Initialize(dialog, handle);

        await dialog.ShowAsync();
    }
}
