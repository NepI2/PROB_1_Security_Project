using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Layout.HelpersClasses
{
    public static class Helpers
    {
        public static bool CheckingKeyName(string name)
        {
            if (name == null || string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                return false;
            else
                return true;
        }
        public static string SelectingFolder(string defaultPath, string type)
        {
            //var dialog = new System.Windows.Forms.FolderBrowserDialog();
            //dialog.SelectedPath = defaultPath;
            //dialog.Description = type;
            //System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            //if (result == System.Windows.Forms.DialogResult.OK)
            //{
            //    return dialog.SelectedPath;
            //}
            //else
            //{
            //    return null;
            //}

            using (var fbd = new System.Windows.Forms.FolderBrowserDialog())
            {

                fbd.Description = "Select a folder";
                fbd.ShowNewFolderButton = true;
                System.Windows.Forms.DialogResult result = fbd.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        return fbd.SelectedPath;
                    }
                else
                    {
                        return null;
                    }
                //fbd.Description = "Select the folder where you want to save the cyphertext";
                //System.Windows.Forms.DialogResult result = fbd.ShowDialog();

                //if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                //{
                //    return fbd.SelectedPath;
                //}
                //else
                //{
                //    return null;
                //}
            }
        }
        public static string SelectFile(string defaultPath, string type)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.SelectedPath = defaultPath;
            dialog.Description = type;
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                return dialog.SelectedPath;
            }
            else
            {
                return null;
            }
        }
        public static bool SavingFile(this string element, string pathForSaving, string extention)
        {

            return false;
        }
        public static string Base64Encoding(string element)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(element));
        }

        public static IEnumerable<string> GetFilesNames(string path)
        {
            string[] files = new string[0];
            if (Directory.Exists(path))
            {
                string[] fileNames = Directory.GetFiles(path).Select(file => Path.GetFileName(file)).ToArray();
                return fileNames;
            }

            return files;
        }

        public static void RefreshListBox(ListBox list, IEnumerable<string> items)
        {
            list.ItemsSource = null;
            list.ItemsSource = items;
        }
    }
}
