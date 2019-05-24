using dock;
using System;
using System.Collections.Generic;
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
    public class ResourceOpener : Control
    {
        TextBlock NameTxt;
        public Rectangle rect;
        SmallButtons RemoveBtn;
        Image IconImg;
        public static readonly DependencyProperty ImgSourceDependency = DependencyProperty.Register("Icon", typeof(ImageSource), typeof(ResourceOpener));
        public static readonly DependencyProperty FileNameDependency = DependencyProperty.Register("FileName", typeof(string), typeof(ResourceOpener));
        public event EventHandler Clicked;
        static ResourceOpener()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ResourceOpener), new FrameworkPropertyMetadata(typeof(ResourceOpener)));
        }
        public ImageSource Icon
        {
            get
            {
                return (ImageSource)GetValue(ImgSourceDependency);
            }
            set
            {
                SetValue(ImgSourceDependency, value);
            }
        }
        public string FileName
        {
            get
            {
                return (string)(GetValue(FileNameDependency));
            }
            set
            {
                SetValue(FileNameDependency, value);
            }
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            NameTxt = (TextBlock)GetTemplateChild("NameTxt");
            RemoveBtn = (SmallButtons)GetTemplateChild("RemoveBtn");
            RemoveBtn.MouseDown += new MouseButtonEventHandler(RemoveItem);
            rect = GetTemplateChild("rect") as Rectangle;
            rect.MouseLeftButtonDown += new MouseButtonEventHandler(ClickItem);
            Binding NameTxtBinding = new Binding("FileName")
            {
                Source = this,
                Mode = BindingMode.TwoWay
            };
            NameTxt.SetBinding(TextBlock.TextProperty, NameTxtBinding);
            IconImg = (Image)GetTemplateChild("IconImg");
            Binding IconBinding = new Binding("Icon")
            {
                Source = this,
                Mode = BindingMode.TwoWay
            };
            IconImg.SetBinding(Image.SourceProperty, IconBinding);
        }
        private void ClickItem(object sender, MouseButtonEventArgs e)
        {
            Clicked?.Invoke(this, e);
        }
        private void RemoveItem(object sender, MouseButtonEventArgs e)
        {
            VirtualizingStackPanel parent = FindParent<VirtualizingStackPanel>(this);
            parent.Children.Remove(this);
            ExecManager exec = new ExecManager();
            exec.RemoveApp(FileName , "qc\\");
        }
        public static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null)
                return null;
            T parent = parentObject as T;
            if (parent != null)
                return parent;
            else
                return FindParent<T>(parentObject);
        }
    }
}
