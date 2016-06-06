using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plarium.Models
{
    class DirectoryModel
    {
        DirectoryTree myTree, selectTree;
        DirectoryInfo myDir;
        public void GetInfoDirectoryFull(string directory, List<string> listSubDirectories, List<string> listFiles,ref string infoDirectory)
        {
            myDir = new DirectoryInfo(@directory);
            GetInfoDirectory(listSubDirectories, listFiles, out infoDirectory);
            myTree = new DirectoryTree(myDir);
            selectTree = myTree;
        }

        private void GetInfoDirectory( List<string> listSubDirectories, List<string> listFiles, out string infoDirectory)
        {
            foreach (var item in myDir.GetDirectories())
            {
                listSubDirectories.Add(item.Name);
            }
            foreach (var item in myDir.GetFiles())
            {
                listFiles.Add(item.Name);
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("Название - ").Append(myDir.Name).Append("\n");
            builder.Append("Дата создания - ").Append(myDir.CreationTime).Append("\n");
            builder.Append("Дата модификации - ").Append(myDir.LastWriteTime).Append("\n");
            builder.Append("Дата последнего доступа - ").Append(myDir.LastAccessTime).Append("\n");
            builder.Append("Атрибуты - ").Append(myDir.Attributes).Append("\n");
            infoDirectory = builder.ToString();
        }
        public void SelectSubDirectory(int index, List<string> listSubDirectories, List<string> listFiles, ref string infoDirectory)
        {
            selectTree = selectTree.listSubDirectories[index];
            myDir = selectTree.directoryValue;
            GetInfoDirectory(listSubDirectories, listFiles, out infoDirectory);
        }
        public bool BackDirectory(List<string> listSubDirectories, List<string> listFiles, ref string infoDirectory)
        {
            selectTree = selectTree.perent;
            myDir = selectTree.directoryValue;
            GetInfoDirectory(listSubDirectories, listFiles, out infoDirectory);
            return selectTree.perent == null;
        }
    }
}
