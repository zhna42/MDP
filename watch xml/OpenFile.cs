using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace watch_xml
{
    class OpenFile
    {
        public string open()
        {
            var dlg = new CommonOpenFileDialog();
            dlg.Title = "My Title";
            /*dlg.IsFolderPicker = true;
            dlg.AddToMostRecentlyUsedList = false;
            dlg.AllowNonFileSystemItems = true;
            dlg.EnsureFileExists = false;
            dlg.EnsurePathExists = true;
            dlg.EnsureReadOnly = false;
            dlg.EnsureValidNames = true;
            dlg.Multiselect = false;
            dlg.ShowPlacesList = true;*/
            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                return dlg.FileName;
            else
                return "";
        }
    }
}
