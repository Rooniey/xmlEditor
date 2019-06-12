using Microsoft.Win32;

namespace GUI.ViewUtility
{
    public static class FileSystemHelper
    {
        public static string GetXmlFilePath()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".xml";
            dlg.Filter = "xml (.xml)|*.xml";
            bool result = dlg.ShowDialog() ?? false;
            return result ? dlg.FileName : "";
        }

    }
}
