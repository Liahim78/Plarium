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
        public DirectoryInfo directoryValue;
        public DirectoryTree perent = null;
        public List<DirectoryTree> listSubDirectories=new List<DirectoryTree> ();
        public FileInfo [] listFiles;

        public DirectoryTree(DirectoryInfo directoryValue)
        {
            this.directoryValue = directoryValue;
            foreach (var item in directoryValue.GetDirectories())
            {
                DirectoryTree temp = new DirectoryTree(item);
                temp.perent = this;
                listSubDirectories.Add(temp);
                
            }
            listFiles = directoryValue.GetFiles();
        }
    }
}
