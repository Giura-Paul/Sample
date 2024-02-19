using Microsoft.UI.Xaml;
using Windows.UI.Popups;
using WinRT.Interop;

namespace Sample.App.ViewModels;

public static class DialogMessageDisplay
{
    public static Task ShowMessageDialogAsync(string message)
    {
        nint handle = WinRT.Interop.WindowNative.GetWindowHandle(((App)Application.Current).MainWindow);
        var dialog = new MessageDialog(message);
        InitializeWithWindow.Initialize(dialog, handle);

        return Task.CompletedTask;
    }
}
