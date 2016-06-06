using Plarium.Comands;
using Plarium.Models;
using Plarium.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Plarium.ViewModels
{
    class DirectoryViewModel: ViewModelIBase 
    {
        DirectoryModel myModel = new DirectoryModel();
        private DelegateCommand<string> digitButtonPressCommand;
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
        public ICommand ChooseButtonPressCommand
        {
            get
            {
                if (digitButtonPressCommand == null)
                {
                    digitButtonPressCommand = new DelegateCommand<string>(
                        ChooseButtonPress, (string button) => { return true; });
                }
                return digitButtonPressCommand;
            }
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
                myModel.GetInfoDirectory(myWindow.Text.Text, listD, listF, ref s );
                ListSubDirectory = listD;
                ListFile = listF;
                InfoDirectory = s;
                
            }

        }
    }
}
