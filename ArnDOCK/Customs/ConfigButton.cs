using dock;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ArnDESK
{

    public class ConfigButton : Control
    {
        Rectangle MainRect;
        System.Windows.Forms.OpenFileDialog op = new System.Windows.Forms.OpenFileDialog();
        ExecManager exec = new ExecManager();
        StackPanel Stack;
        ContextMenu OptionsMenu;
        public string ProcessName;
        public static readonly DependencyProperty IconDependency = DependencyProperty.Register("Icon", typeof(ImageSource), typeof(ConfigButton));
        static ConfigButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ConfigButton), new FrameworkPropertyMetadata(typeof(ConfigButton)));
        }
        public ImageSource Icon
        {
            get
            {
                return (ImageSource)GetValue(IconDependency);
            }
            set
            {
                SetValue(IconDependency, value);
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();       
            MainRect = (Rectangle)GetTemplateChild("MainRect");
            MainRect.Fill = new ImageBrush(Icon);
            Stack = (StackPanel)GetTemplateChild("Stack");
            Stack.MouseRightButtonDown += new MouseButtonEventHandler(ProcessRightClicked);
            Stack.MouseLeftButtonDown += new MouseButtonEventHandler(ProcessClicked);
            OptionsMenu = (ContextMenu)GetTemplateChild("OptionsMenu");
            OptionsMenu.Loaded += new RoutedEventHandler(OptionsMenu_Loaded);
        }
        private void OptionsMenu_Loaded(object sender, RoutedEventArgs e)
        {
            var template = OptionsMenu.Template;
            TextBlock IcoBtn = (TextBlock)template.FindName("IcoBtn", OptionsMenu);
            IcoBtn.MouseLeftButtonDown += new MouseButtonEventHandler(ChangeIcon);
            TextBlock AddExe = (TextBlock)template.FindName("AddExe", OptionsMenu);
            AddExe.MouseLeftButtonDown += new MouseButtonEventHandler(AddApp);
        }
        private void ChangeIcon(object sender, MouseButtonEventArgs e)
        {
            op.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png, *.ico) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.ico";
            if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                exec.ReplaceIcon(ProcessName, op.FileName);
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }
        }
        private void ProcessRightClicked(object sender, MouseButtonEventArgs e)
        {
            var cm = ContextMenuService.GetContextMenu(sender as DependencyObject);
            if (cm == null)
                return;
            else
            {
                cm.Placement = PlacementMode.Top;
                cm.PlacementTarget = sender as UIElement;
                cm.IsOpen = true;
            }
        }
        private void ProcessClicked(object sender, MouseButtonEventArgs e)
        {
            exec.RunProcess(ProcessName, "","");
        }
        private void AddApp(object sender, MouseButtonEventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\ArnDESK\\ArnDOCK";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            op.Filter = "Applications (*.exe) | *.exe";
            if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                exec.GetExe(op.FileName, path);
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }
        }
    }
}
