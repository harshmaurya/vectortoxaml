using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace VectorToXamlConvertor.Services
{
    static class FileDialogService
    {
        internal static List<string> ShowFileDialog(bool allowMultiselection = false)
        {
            List<string> result = null;
            var dialog = new OpenFileDialog { Multiselect = allowMultiselection };
            result = dialog.ShowDialog() != false ? new List<string>(dialog.FileNames) : new List<string>();
            return result;
        }
    }
}
