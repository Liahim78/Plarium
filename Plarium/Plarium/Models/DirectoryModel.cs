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
        DirectoryTree myTree;
        public void GetInfoDirectory(string directory, List<string> listSubDirectories, List<string> listFiles, string infoDirectory)
        {
            DirectoryInfo myDir = new DirectoryInfo(@directory);
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
            myTree = new DirectoryTree(myDir);
        }
    }
}
