using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Plarium.Models
{
    class DirectoryModel
    {
        static DirectoryTree myTree, selectTree;
        static DirectoryInfo myDir;
        static XmlTextWriter xmlWriter;
        Thread CreateTree = new Thread(new ThreadStart(TreeCreate));
        /// <summary>
        /// Getting information about the main directory and the establishment of the tree.
        /// </summary>
        /// <param name="directory">full address directory</param>
        /// <param name="listSubDirectories">option for transmission of the list of directories directory</param>
        /// <param name="listFiles">option to transmit the list of the specified directory files</param>
        /// <param name="infoDirectory">parameter to pass information about this directory</param>
        public void GetInfoDirectoryFull(string directory, List<string> listSubDirectories, List<string> listFiles,ref string infoDirectory)
        {
            try
            {
                myDir = new DirectoryInfo(@directory);
                GetInfoDirectory(listSubDirectories, listFiles, out infoDirectory);
                CreateTree.Start();
            }
            catch
            {
                throw new Exception();
            }
        }
        /// <summary>
        /// Create tree
        /// </summary>
        private static void TreeCreate()
        {
            myTree = new DirectoryTree(myDir);
            selectTree = myTree;
        }
        /// <summary>
        /// Information about myDir directory
        /// </summary>
        /// <param name="listSubDirectories">option for transmission of the list of directories directory</param>
        /// <param name="listFiles">option to transmit the list of the specified directory files</param>
        /// <param name="infoDirectory">parameter to pass information about this directory</param>
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
            builder.Append("Владелец - ").Append(myDir.GetAccessControl(AccessControlSections.Owner).GetOwner(typeof(NTAccount))).Append("\n");
            WindowsIdentity wi = WindowsIdentity.GetCurrent();
            builder.Append("Пользователь - ").Append(wi.Name).Append("\n");
            infoDirectory = builder.ToString();
        }
        /// <summary>
        /// Information on the sub-directory
        /// </summary>
        /// <param name="index">subdirectories index</param>
        /// <param name="listSubDirectories">option for transmission of the list of directories directory</param>
        /// <param name="listFiles">option to transmit the list of the specified directory files</param>
        /// <param name="infoDirectory">parameter to pass information about this directory</param>
        public void SelectSubDirectory(int index, List<string> listSubDirectories, List<string> listFiles, ref string infoDirectory)
        {
            selectTree = selectTree.listSubDirectories[index];
            myDir = selectTree.directoryValue;
            GetInfoDirectory(listSubDirectories, listFiles, out infoDirectory);
        }
        /// <summary>
        /// Return to the parent directory
        /// </summary>
        /// <param name="listSubDirectories">option for transmission of the list of directories directory</param>
        /// <param name="listFiles">option to transmit the list of the specified directory files</param>
        /// <param name="infoDirectory">parameter to pass information about this directory</param>
        /// <returns>Returns true if the selected directory has not more the parent</returns>
        public bool BackDirectory(List<string> listSubDirectories, List<string> listFiles, ref string infoDirectory)
        {
            selectTree = selectTree.parent;
            myDir = selectTree.directoryValue;
            GetInfoDirectory(listSubDirectories, listFiles, out infoDirectory);
            return selectTree.parent == null;
        }
        /// <summary>
        /// Return to main directory
        /// </summary>
        /// <param name="listSubDirectories">option for transmission of the list of directories directory</param>
        /// <param name="listFiles">option to transmit the list of the specified directory files</param>
        /// <param name="infoDirectory">parameter to pass information about this directory</param>
        public void HomeDirectory(List<string> listSubDirectories, List<string> listFiles, ref string infoDirectory)
        {
            selectTree = myTree;
            myDir = selectTree.directoryValue;
            GetInfoDirectory(listSubDirectories, listFiles, out infoDirectory);
        }
        /// <summary>
        /// file selection
        /// </summary>
        /// <param name="indexFile">file index</param>
        /// <param name="infoFile">File information</param>
        public void ChooseFile(int indexFile, ref string infoFile)
        {
            GetInfoFile(selectTree.listFiles[indexFile],out infoFile);
        }
        /// <summary>
        /// It gives information about a file
        /// </summary>
        /// <param name="myFile">file</param>
        /// <param name="infoFile">File information</param>
        private void GetInfoFile(FileInfo myFile, out string infoFile)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Название - ").Append(myFile.Name).Append("\n");
            builder.Append("Дата создания - ").Append(myFile.CreationTime).Append("\n");
            builder.Append("Дата модификации - ").Append(myFile.LastWriteTime).Append("\n");
            builder.Append("Дата последнего доступа - ").Append(myFile.LastAccessTime).Append("\n");
            builder.Append("Атрибуты - ").Append(myFile.Attributes).Append("\n");
            builder.Append("Размер - ").Append(myFile.Length).Append("\n");
            builder.Append("Владелец - ").Append(myFile.GetAccessControl(AccessControlSections.Owner).GetOwner(typeof(NTAccount))).Append("\n");

            infoFile = builder.ToString();
        }
        #region WriteXML
        public static void DoXML(object xmlName)
        {
            xmlWriter = new XmlTextWriter(xmlName.ToString(), null)
            {
                Formatting = Formatting.Indented,
                IndentChar = '\t',
                Indentation = 1,
                QuoteChar = '\''
            };
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("ListOfDirectories");
            WriteXML(myTree);
            xmlWriter.WriteEndElement();
            xmlWriter.Close();
        }

        private static void WriteXML(DirectoryTree selectedTree)
        {
            xmlWriter.WriteStartElement("Directory");
            xmlWriter.WriteStartElement("Имя");
            xmlWriter.WriteString(selectedTree.directoryValue.Name);
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("ДатаСоздания");
            xmlWriter.WriteString(selectedTree.directoryValue.CreationTime.ToString());
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("ДатаМодификации");
            xmlWriter.WriteString(selectedTree.directoryValue.LastWriteTime.ToString());
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("ДатаПоследнегоДоступа");
            xmlWriter.WriteString(selectedTree.directoryValue.LastAccessTime.ToString());
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("Атрибуты");
            xmlWriter.WriteString(selectedTree.directoryValue.Attributes.ToString());
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("Владелец");
            xmlWriter.WriteString(myDir.GetAccessControl(AccessControlSections.Owner).GetOwner(typeof(NTAccount)).ToString());
            xmlWriter.WriteEndElement();
            foreach (var item in selectedTree.listSubDirectories)
            {
                WriteXML(item);
            }
            foreach (var item in selectedTree.listFiles)
            {
                xmlWriter.WriteStartElement("File");
                WriteFileInXML(item);
                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();

        }

        public static void DoSelectXML(object xmlName)
        {
            xmlWriter = new XmlTextWriter(xmlName.ToString(), null)
            {
                Formatting = Formatting.Indented,
                IndentChar = '\t',
                Indentation = 1,
                QuoteChar = '\''
            };
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("ListOfDirectories");
            WriteXML(selectTree);
            xmlWriter.WriteEndElement();
            xmlWriter.Close();
        }

        private static void WriteFileInXML(FileInfo item)
        {
            xmlWriter.WriteStartElement("Имя");
            xmlWriter.WriteString(item.Name);
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("ДатаСоздания");
            xmlWriter.WriteString(item.CreationTime.ToString());
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("ДатаМодификации");
            xmlWriter.WriteString(item.LastWriteTime.ToString());
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("ДатаПоследнегоДоступа");
            xmlWriter.WriteString(item.LastAccessTime.ToString());
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("Атрибуты");
            xmlWriter.WriteString(item.Attributes.ToString());
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("Размер");
            xmlWriter.WriteString(item.Length.ToString());
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("Владелец");
            xmlWriter.WriteString(item.GetAccessControl(AccessControlSections.Owner).GetOwner(typeof(NTAccount)).ToString());
            xmlWriter.WriteEndElement();
        }
        #endregion
    }
}
