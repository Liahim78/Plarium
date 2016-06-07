using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Plarium.Models
{
    class DirectoryModel
    {
        DirectoryTree myTree, selectTree;
        DirectoryInfo myDir;
        XmlTextWriter xmlWriter;
        /// <summary>
        /// Получение информации о главной дирректории и создание дерева.
        /// </summary>
        /// <param name="directory">полный адресс дирректории</param>
        /// <param name="listSubDirectories">параметр для передачи списка поддиректорий данной дирректории</param>
        /// <param name="listFiles">параметр для передачи списка файлов данной дирректории</param>
        /// <param name="infoDirectory">параметр для передачи информации о данной дирректории</param>
        public void GetInfoDirectoryFull(string directory, List<string> listSubDirectories, List<string> listFiles,ref string infoDirectory)
        {
            myDir = new DirectoryInfo(@directory);
            GetInfoDirectory(listSubDirectories, listFiles, out infoDirectory);
            myTree = new DirectoryTree(myDir);
            selectTree = myTree;
        }
        /// <summary>
        /// Инофрмация о дирректории myDir
        /// </summary>
        /// <param name="listSubDirectories">параметр для передачи списка поддиректорий данной дирректории</param>
        /// <param name="listFiles">параметр для передачи списка файлов данной дирректории</param>
        /// <param name="infoDirectory">параметр для передачи информации о данной дирректории</param>
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
        /// <summary>
        /// Информация о поддиректории
        /// </summary>
        /// <param name="index">индекс поддиректории</param>
        /// <param name="listSubDirectories">параметр для передачи списка поддиректорий данной дирректории</param>
        /// <param name="listFiles">параметр для передачи списка файлов данной дирректории</param>
        /// <param name="infoDirectory">параметр для передачи информации о данной дирректории</param>
        public void SelectSubDirectory(int index, List<string> listSubDirectories, List<string> listFiles, ref string infoDirectory)
        {
            selectTree = selectTree.listSubDirectories[index];
            myDir = selectTree.directoryValue;
            GetInfoDirectory(listSubDirectories, listFiles, out infoDirectory);
        }
        /// <summary>
        /// Возвращение к родительской дирректориии
        /// </summary>
        /// <param name="listSubDirectories">параметр для передачи списка поддиректорий данной дирректории</param>
        /// <param name="listFiles">параметр для передачи списка файлов данной дирректории</param>
        /// <param name="infoDirectory">параметр для передачи информации о данной дирректории</param>
        /// <returns>Возвращает значение true, если у выбранной дирректории больше нет родителя</returns>
        public bool BackDirectory(List<string> listSubDirectories, List<string> listFiles, ref string infoDirectory)
        {
            selectTree = selectTree.perent;
            myDir = selectTree.directoryValue;
            GetInfoDirectory(listSubDirectories, listFiles, out infoDirectory);
            return selectTree.perent == null;
        }
        /// <summary>
        /// Возвращение к главной дирректориии
        /// </summary>
        /// <param name="listSubDirectories">параметр для передачи списка поддиректорий данной дирректории</param>
        /// <param name="listFiles">параметр для передачи списка файлов данной дирректории</param>
        /// <param name="infoDirectory">параметр для передачи информации о данной дирректории</param>
        public void HomeDirectory(List<string> listSubDirectories, List<string> listFiles, ref string infoDirectory)
        {
            selectTree = myTree;
            myDir = selectTree.directoryValue;
            GetInfoDirectory(listSubDirectories, listFiles, out infoDirectory);
        }

        public void ChooseFile(int indexFile, ref string infoFile)
        {
            GetInfoFile(selectTree.listFiles[indexFile],out infoFile);
        }
        private void GetInfoFile(FileInfo myFile, out string infoFile)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Название - ").Append(myFile.Name).Append("\n");
            builder.Append("Дата создания - ").Append(myFile.CreationTime).Append("\n");
            builder.Append("Дата модификации - ").Append(myFile.LastWriteTime).Append("\n");
            builder.Append("Дата последнего доступа - ").Append(myFile.LastAccessTime).Append("\n");
            builder.Append("Атрибуты - ").Append(myFile.Attributes).Append("\n");
            infoFile = builder.ToString();
        }
        #region WriteXML
        public void DoXML(string xmlName)
        {
            xmlWriter = new XmlTextWriter(xmlName+".xml", null)
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

        private void WriteXML(DirectoryTree selectedTree)
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

        public void DoSelectXML(string xmlName)
        {
            xmlWriter = new XmlTextWriter(xmlName + ".xml", null)
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

        private void WriteFileInXML(FileInfo item)
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
        }
        #endregion
    }
}
