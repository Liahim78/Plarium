using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Plarium.Views
{
    /// <summary>
    /// Interaction logic for HelpWindow.xaml
    /// </summary>
    public partial class HelpWindow : Window
    {
        public HelpWindow()
        {
            InitializeComponent();
            StringBuilder builder = new StringBuilder();
            builder.Append("  Данная программа позволяет\n");
            builder.Append("пользователю создавать xml\n");
            builder.Append("файл, в который можно записать\n");
            builder.Append("информацию о выбранной папке.\n");
            builder.Append("  При запуске программы\n");
            builder.Append("нажмите на кнопку \"Выбрать\"\n");
            builder.Append("и введите полный путь к папке.\n");
            builder.Append("В результате в окне\n");
            builder.Append("появятся два списка, текст\n");
            builder.Append("и две кнопки. В первом списке\n");
            builder.Append("хранятся названия всех\n");
            builder.Append("поддиректорий, во втором - \n");
            builder.Append("все файлы из этой папки.\n");
            builder.Append("В тексте записана информация\n");
            builder.Append("о директории. И две кнопки\n");
            builder.Append("для записи xml файла.\n");
            builder.Append("Одна кнопка всегда записывает\n");
            builder.Append("информацию о главной папке.\n");
            builder.Append("А так как можно переходить\n");
            builder.Append("по поддиректориям, то вторая\n");
            builder.Append("кнопка записывает информацию\n");
            builder.Append("о текущей директории.\n");
            TextHelp.Text = builder.ToString();
        }
    }
}
