using Microsoft.UI.Xaml.Controls;

namespace Sample.App;

public class DialogHandler
{
    private readonly ContentDialog _dialog;
    private bool _allowClose;

    public DialogHandler(ContentDialog dialog)
    {
        _dialog = dialog;
        _dialog.Closing += (_, e) => e.Cancel = !_allowClose;
    }

    public void Close()
    {
        _allowClose = true;
        _dialog.Hide();
    }
}
