using dock;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace ArnDESK
{
    /// <summary>
    /// Interaction logic for CodePanel.xaml
    /// </summary>
    public partial class CodePanel : Page
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\ArnDESK\\SliderWidget\\qc\\";
        ImageLoader img = new ImageLoader();
        ExecManager exec = new ExecManager();
        public CodePanel()
        {
            InitializeComponent();
            LoadItems();
        }
        private async void LoadItems()
        {
            await Task.Delay(400);
            if (Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path, "*.imgdata", SearchOption.AllDirectories);
                string[] files2 = Directory.GetFiles(path, "*.data", SearchOption.AllDirectories);
                for (int i = 0; i < files.Length; i++)
                {
                    string file = Path.GetFileName(files[i]);
                    ResourceOpener rc = new ResourceOpener() { FileName = Path.GetFileNameWithoutExtension(files2[i]), Icon = img.LoadImage(new Uri(path + file), 40) };
                    rc.Clicked += new EventHandler(RunProcess);
                    Container.Children.Add(rc);
                    Container.Height += 38;
                }
            }
        }
        private void AddItem(object sender, MouseButtonEventArgs e)
        {
            
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
                Container.Children.Add(rc);
                rc.Clicked += new EventHandler(RunProcess);
                Container.Height += 38;
            }
        }
        private void RunProcess(object sender, EventArgs e)
        {
            ResourceOpener rc = (ResourceOpener)sender;
            exec.RunProcess(rc.FileName + ".data", "", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\ArnDESK\SliderWidget\qc\");
        }
    }
}
