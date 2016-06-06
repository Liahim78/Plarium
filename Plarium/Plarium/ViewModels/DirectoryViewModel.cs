using Plarium.Comands;
using Plarium.Models;
using Plarium.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Plarium.ViewModels
{
    class DirectoryViewModel: ViewModelIBase 
    {
        DirectoryModel myModel = new DirectoryModel();
        private DelegateCommand<string> chooseButtonPressCommand;
        private DelegateCommand<string> chooseSubDirectoryComand;
        private DelegateCommand<string> chooseFileComand;
        private DelegateCommand<string> backCommand; 
        private DelegateCommand<string> homeCommand;
        private int indexSubDir;
        private int indexFile;
        public int IndexFile
        {
            get { return indexFile; }
            set { indexFile = value; OnPropertyChanged("IndexFile"); }
        }
        public int IndexSubDir
        {
            get { return indexSubDir; }
            set { indexSubDir = value; OnPropertyChanged("IndexSubDir"); }
        }
        List<string> listSubDirectory=new List<string> ();
        public List<string> ListSubDirectory
        {
            get { return listSubDirectory; }
            set { listSubDirectory = value; OnPropertyChanged("ListSubDirectory"); }
        }
        List<string> listFile=new List<string> ();
        public List<string> ListFile
        {
            get { return listFile; }
            set { listFile = value; OnPropertyChanged("ListFile"); }
        }
        string infoDirectory = "";
        public string InfoDirectory
        {
            get { return infoDirectory; }
            set { infoDirectory = value; OnPropertyChanged("InfoDirectory"); }
        }
        Visibility visibilityAll = Visibility.Collapsed;
        public Visibility VisibilityAll
        {
            get { return visibilityAll; }
            set { visibilityAll = value; OnPropertyChanged("VisibilityAll"); }
        }
        Visibility visibilityImage = Visibility.Collapsed;
        public Visibility VisibilityImage
        {
            get { return visibilityImage; }
            set { visibilityImage = value; OnPropertyChanged("VisibilityImage"); }
        }
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
                string s = "";
                List<string> listD = new List<string>();
                List<string> listF = new List<string>();
                myModel.GetInfoDirectoryFull(myWindow.Text.Text, listD, listF, ref s );
                ListSubDirectory = listD;
                ListFile = listF;
                InfoDirectory = s;
                VisibilityAll = Visibility.Visible;
            }

        }
    }
}
