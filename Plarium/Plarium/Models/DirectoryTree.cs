using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plarium.Models
{
    class DirectoryTree
    {
        DirectoryInfo directoryValue;
        List<DirectoryTree> listSubDirectories;
        FileInfo [] listFiles;

        public DirectoryTree(DirectoryInfo directoryValue)
        {
            this.directoryValue = directoryValue;
            foreach (var item in directoryValue.GetDirectories())
            {
                listSubDirectories.Add(new DirectoryTree(item));
            }
            listFiles = directoryValue.GetFiles();
        }
    }
}
