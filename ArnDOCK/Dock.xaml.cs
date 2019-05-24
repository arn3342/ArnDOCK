using System;
using System.Windows;
using dock;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Collections;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ArnDESK
{
    public partial class MainWindow : Window
    {
        ExecManager exec = new ExecManager();
        public MainWindow()
        {           
            InitializeComponent();
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;       
            this.Top = desktopWorkingArea.Bottom - this.Height;
            this.ShowInTaskbar = false;
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\ArnDESK\\ArnDESK\\"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\ArnDESK\\ArnDESK\\");
            }
            //AddApp();
            LoadItems();
            this.Topmost = true;
            this.Left = (desktopWorkingArea.Right / 2) - (this.Width / 2);
        }
        private void LoadItems()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\ArnDESK\ArnDESK\";
            List<string> exes = new List<string>(Directory.GetFiles(path, "*.data", SearchOption.AllDirectories));
            List<string> icons = new List<string>(Directory.GetFiles(path, "*.imgdata", SearchOption.AllDirectories));
            ImageLoader img = new ImageLoader();
            if (exes.Any())
            {
                int i = 0;
                foreach (string item in exes)
                {
                    ConfigButton newExe = new ConfigButton()
                    {
                        ProcessName = item,
                        Icon = img.LoadImage(new Uri(icons[i]), 30)
                    };
                    Container.Children.Add(newExe);
                    i++;
                    this.Width += 52;
                }
            }
            exes.Clear();
            icons.Clear();           
        }

    }
}
