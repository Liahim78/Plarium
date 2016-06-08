using Microsoft.Win32;
using Plarium.Comands;
using Plarium.Models;
using Plarium.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Plarium.ViewModels
{
    class DirectoryViewModel: ViewModelIBase 
    {
        #region Properties
        DirectoryModel myModel = new DirectoryModel();
        Thread writeMainXML = new Thread(new ParameterizedThreadStart(DirectoryModel.DoXML));
        Thread writeSelectXML = new Thread(new ParameterizedThreadStart(DirectoryModel.DoSelectXML));
        private DelegateCommand<string> chooseButtonPressCommand;// delegate for the method main directory
        private DelegateCommand<string> chooseSubDirectoryComand;// delegate to go to the selected directory
        private DelegateCommand<string> chooseFileComand;//delegate to display information about a file
        private DelegateCommand<string> backCommand; // delegate to return to the parent directory
        private DelegateCommand<string> homeCommand; // delegate to return to the home directory
        private DelegateCommand<string> doXMLCommand;//  delegate to create an XML file
        private DelegateCommand<string> doSelectXMLCommand;//  delegate to create an XML file of the selected directory
        private int indexSubDir;
        private int indexFile;
        /// <summary>
        /// The index of the selected item from the ListBox, which stores files
        /// </summary>
        public int IndexFile
        {
            get { return indexFile; }
            set { indexFile = value; OnPropertyChanged("IndexFile"); }
        }
        /// <summary>
        /// The index of the selected element ListBox, which stores the subdirectory
        /// </summary>
        public int IndexSubDir
        {
            get { return indexSubDir; }
            set { indexSubDir = value; OnPropertyChanged("IndexSubDir"); }
        }
        List<string> listSubDirectory=new List<string> ();
        /// <summary>
        /// The list of directories that will be displayed in the ListBox
        /// </summary>
        public List<string> ListSubDirectory
        {
            get { return listSubDirectory; }
            set { listSubDirectory = value; OnPropertyChanged("ListSubDirectory"); }
        }
        List<string> listFile=new List<string> ();
        /// <summary>
        /// The list of files that will be displayed in the ListBox
        /// </summary>
        public List<string> ListFile
        {
            get { return listFile; }
            set { listFile = value; OnPropertyChanged("ListFile"); }
        }
        string infoDirectory = "";
        /// <summary>
        /// Information about the current directory
        /// </summary>
        public string InfoDirectory
        {
            get { return infoDirectory; }
            set { infoDirectory = value; OnPropertyChanged("InfoDirectory"); }
        }
        Visibility visibilityAll = Visibility.Collapsed;
        /// <summary>
        /// Property is responsible for mapping objects in the window
        /// </summary>
        public Visibility VisibilityAll
        {
            get { return visibilityAll; }
            set { visibilityAll = value; OnPropertyChanged("VisibilityAll"); }
        }
        Visibility visibilityImage = Visibility.Collapsed;
        /// <summary>
        /// The property is responsible for the display of pictures in the window
        /// </summary>
        public Visibility VisibilityImage
        {
            get { return visibilityImage; }
            set { visibilityImage = value; OnPropertyChanged("VisibilityImage"); }
        }
        #endregion

        #region Comands

        
        public ICommand ChooseButtonPressCommand
        {
            get
            {
                if (chooseButtonPressCommand == null)
                {
                    chooseButtonPressCommand = new DelegateCommand<string>(
                        ChooseButtonPress, (string button) => { return true; });
                }
                return chooseButtonPressCommand;
            }
        }
        public ICommand ChooseSubDirectoryComand
        {
            get
            {
                if (chooseSubDirectoryComand == null)
                {
                    chooseSubDirectoryComand = new DelegateCommand<string>(
                        ChooseSubDirectory, (string button) => { return true; });
                }
                return chooseSubDirectoryComand;
            }
        }
        public ICommand BackCommand
        {
            get
            {
                if (backCommand == null)
                {
                    backCommand = new DelegateCommand<string>(
                        BackDirectory, (string button) => { return true; });
                }
                return backCommand;
            }
        }
        public ICommand HomeCommand
        {
            get
            {
                if (homeCommand == null)
                {
                    homeCommand = new DelegateCommand<string>(
                        HomeDirectory, (string button) => { return true; });
                }
                return homeCommand;
            }
        }
        public ICommand ChooseFileComand
        {
            get
            {
                if (chooseFileComand == null)
                {
                    chooseFileComand = new DelegateCommand<string>(
                        ChooseFile, (string button) => { return true; });
                }
                return chooseFileComand;
            }
        }
        public ICommand DoXMLCommand
        {
            get
            {
                if (doXMLCommand == null)
                {
                    doXMLCommand = new DelegateCommand<string>(
                        DoXML, (string button) => { return true; });
                }
                return doXMLCommand;
            }
        }
        public ICommand DoSlectXMLCommand
        {
            get
            {
                if (doSelectXMLCommand == null)
                {
                    doSelectXMLCommand = new DelegateCommand<string>(
                        DoSelectXML, (string button) => { return true; });
                }
                return doSelectXMLCommand;
            }
        }

        
        #endregion

        #region Methods
        private void DoSelectXML(string obj)
        {
            SaveFileDialog _saveDialog = new SaveFileDialog();
            _saveDialog.Filter = "XML files (*.XML)|*.xml|All Files (*.*)|*.*";
            if (_saveDialog.ShowDialog() == true)
                writeSelectXML.Start(_saveDialog.FileName);
        }

        private void DoXML(string obj)
        {
            SaveFileDialog _saveDialog = new SaveFileDialog();
            _saveDialog.Filter = "XML files (*.XML)|*.xml|All Files (*.*)|*.*";
            if (_saveDialog.ShowDialog() == true)
                 writeMainXML.Start(_saveDialog.FileName);
        }

        private void ChooseFile(string obj)
        {
            string s = "";
            myModel.ChooseFile(indexFile, ref s);
            InfoDirectory = s;
        }

        private void HomeDirectory(string obj)
        {
            string s = "";
            List<string> listD = new List<string>();
            List<string> listF = new List<string>();
            myModel.HomeDirectory(listD, listF, ref s);
            ListSubDirectory = listD;
            ListFile = listF;
            InfoDirectory = s;
            VisibilityImage = Visibility.Collapsed;
        }

        private void BackDirectory(string obj)
        {
            string s = "";
            List<string> listD = new List<string>();
            List<string> listF = new List<string>();
            bool f = myModel.BackDirectory (listD, listF, ref s);
            ListSubDirectory = listD;
            ListFile = listF;
            InfoDirectory = s;
            if (f) VisibilityImage = Visibility.Collapsed;
        }

        private void ChooseSubDirectory(string obj)
        {
            string s = "";
            List<string> listD = new List<string>();
            List<string> listF = new List<string>();
            myModel.SelectSubDirectory(IndexSubDir, listD, listF, ref s);
            ListSubDirectory = listD;
            ListFile = listF;
            InfoDirectory = s;
            VisibilityImage = Visibility.Visible;

        }

        private void ChooseButtonPress(string obj)
        {
            ChooseDirectory myWindow = new ChooseDirectory();
            myWindow.ShowDialog();
            if ((bool)myWindow.DialogResult)
            {
                try
                {
                    string s = "";
                    List<string> listD = new List<string>();
                    List<string> listF = new List<string>();
                    myModel.GetInfoDirectoryFull(myWindow.Text.Text, listD, listF, ref s);
                    ListSubDirectory = listD;
                    ListFile = listF;
                    InfoDirectory = s;
                    VisibilityAll = Visibility.Visible;
                }
                catch (Exception)
                {
                    MessageBox.Show("Путь к папке указан не верно");
                }
            }

        }
        #endregion
    }
}
