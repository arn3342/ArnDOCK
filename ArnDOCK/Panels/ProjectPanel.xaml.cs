using dock;
using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
namespace ArnDESK
{
    /// <summary>
    /// Interaction logic for ProjectPanel.xaml
    /// </summary>
    public partial class ProjectPanel : Page
    {
        ImageLoader img = new ImageLoader();
        ExecManager exec = new ExecManager();
        public ProjectPanel()
        {
            InitializeComponent();
        }
        private void AddItem(object sender, MouseButtonEventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\ArnDESK\\SliderWidget\\proj\\";
            OpenFileDialog op = new OpenFileDialog
            {
                Title = "Choose file to add..."
            };
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (op.ShowDialog() == DialogResult.OK)
            {
                exec.GetExe(op.FileName, path);
                ResourceOpener rc = new ResourceOpener() { FileName = op.SafeFileName, Icon = img.LoadImage(new Uri(path + System.IO.Path.GetFileNameWithoutExtension(op.FileName) + ".imgdata"), 60) };
                rc.MouseLeftButtonDown += new MouseButtonEventHandler(RunProcess);
                Container.Children.Add(rc);
                Container.Height += 38;
            }
        }
        private void RunProcess(object sender, MouseButtonEventArgs e)
        {
            ResourceOpener rc = (ResourceOpener)sender;
            exec.RunProcess(rc.FileName, "","");
        }
    }

}
