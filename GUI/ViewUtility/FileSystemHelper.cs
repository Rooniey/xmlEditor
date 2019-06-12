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

        public static string GetXmlSaveFilePath()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".xml";
            bool result = sfd.ShowDialog() ?? false;
            return result ? sfd.FileName : "";
        }

        public static string GetSaveFilePath()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            bool result = sfd.ShowDialog() ?? false;
            return result ? sfd.FileName : "";
        }

    }
}
