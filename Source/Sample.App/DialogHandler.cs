using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.App
{
    public class DialogHandler
    {
        private ContentDialog _dialog;
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
}
