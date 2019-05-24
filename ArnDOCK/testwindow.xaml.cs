using dock;
using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace ArnDESK
{
    /// <summary>
    /// Interaction logic for test.xaml
    /// </summary>
    public partial class testwindow : Window
    {
        private ExecManager exec = new ExecManager();
        public testwindow()
        {
            InitializeComponent();
            //exec.GetExe(@"C:\Users\ASUS ZEN\Documents\Visual Studio 2017\Projects\ArnDESK\ArnDESK.sln");
            testimg.Source = new BitmapImage(new Uri(@"C:\Users\ASUS ZEN\Documents\ArnDESK\ArnDESK\ArnDESK.imgdata"));
        }
    }
}
