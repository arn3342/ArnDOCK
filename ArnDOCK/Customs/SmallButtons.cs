using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.VisualBasic;

namespace ArnDESK
{
    public class SmallButtons : Control
    {
        public Image img;
        private DockPanel MainRectangle;
        private TextBlock TextToolTip;
        public static readonly DependencyProperty ImgHeightDependency = DependencyProperty.Register("ImageHeight", typeof(double), typeof(SmallButtons));
        public static readonly DependencyProperty ImgWidthDependency = DependencyProperty.Register("ImageWidth", typeof(double), typeof(SmallButtons));
        public static readonly DependencyProperty ImgOpacityDependency = DependencyProperty.Register("ImageOpacity", typeof(double), typeof(SmallButtons));
        public static readonly DependencyProperty ImageMOOpacityDependency = DependencyProperty.Register("ImageMOOpacity", typeof(double), typeof(SmallButtons), new PropertyMetadata((double)0.6));
        public static readonly DependencyProperty ImageMLOpacityDependency = DependencyProperty.Register("ImageMLOpacity", typeof(double), typeof(SmallButtons));
        public static readonly DependencyProperty ImgSourceDependency = DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(SmallButtons));
        public static readonly DependencyProperty ToolTipTextDependency = DependencyProperty.Register("ToolTipText", typeof(string), typeof(SmallButtons));
        public static readonly DependencyProperty ImgHrAlignmentDependency = DependencyProperty.Register("ImageHorizontalAlignment", typeof(HorizontalAlignment), typeof(SmallButtons));
        public static readonly DependencyProperty RectXRadiusDependency = DependencyProperty.Register("RadiuxX", typeof(double), typeof(SmallButtons));
        public static readonly DependencyProperty RectYRadiusDependency = DependencyProperty.Register("RadiuxY", typeof(double), typeof(SmallButtons));
        public static readonly DependencyProperty MouseEnterColorDependency = DependencyProperty.Register("MouseEnter Color", typeof(Brush), typeof(SmallButtons));
        public static readonly DependencyProperty AllowToolTipDependency = DependencyProperty.Register("AllowToolTip", typeof(bool), typeof(SmallButtons));
        public static readonly DependencyProperty AllowHoverAnimationDependency = DependencyProperty.Register("AllowHoverAnimation", typeof(bool), typeof(SmallButtons));
        private BrushConverter bb = new BrushConverter();
        public HorizontalAlignment ImgHorizontalAlignment
        {
            get
            {
                return (HorizontalAlignment)GetValue(ImgHrAlignmentDependency);
            }
            set
            {
                SetValue(ImgHrAlignmentDependency, value);
            }
        }
        public bool DisallowDefaultToolTip
        {
            get
            {
                return Convert.ToBoolean(GetValue(AllowToolTipDependency));
            }
            set
            {
                SetValue(AllowToolTipDependency, value);
            }
        }
        public bool AllowHoverAnimation
        {
            get
            {
                return Convert.ToBoolean(GetValue(AllowHoverAnimationDependency));
            }
            set
            {
                SetValue(AllowHoverAnimationDependency, value);
            }
        }
        public Brush MouseEnterColor
        {
            get
            {
                return (Brush)(GetValue(MouseEnterColorDependency));
            }
            set
            {
                SetValue(MouseEnterColorDependency, value);
            }
        }
        public string ToolTipText
        {
            get
            {
                return System.Convert.ToString(GetValue(ToolTipTextDependency));
            }
            set
            {
                SetValue(ToolTipTextDependency, value);
            }
        }
        public double RadiusX
        {
            get
            {
                return Convert.ToDouble(GetValue(RectXRadiusDependency));
            }
            set
            {
                SetValue(RectXRadiusDependency, value);
            }
        }
        public double ImgOpacity
        {
            get
            {
                return Convert.ToDouble(GetValue(ImgOpacityDependency));
            }
            set
            {
                SetValue(ImgOpacityDependency, value);
            }
        }
        public double ImageMOOpacity
        {
            get
            {
                return Convert.ToDouble(GetValue(ImageMOOpacityDependency));
            }
            set
            {
                SetValue(ImageMOOpacityDependency, value);
            }
        }
        public double ImageMLOpacity
        {
            get
            {
                return Convert.ToDouble(GetValue(ImageMLOpacityDependency));
            }
            set
            {
                SetValue(ImageMLOpacityDependency, value);
            }
        }
        public double RadiusY
        {
            get
            {
                return Convert.ToDouble(GetValue(RectYRadiusDependency));
            }
            set
            {
                SetValue(RectYRadiusDependency, value);
            }
        }

        public double ImgHeight
        {
            get
            {
                return Convert.ToDouble(GetValue(ImgHeightDependency));
            }
            set
            {
                SetValue(ImgHeightDependency, value);
            }
        }

        public double ImgWidth
        {
            get
            {
                return Convert.ToDouble(GetValue(ImgWidthDependency));
            }
            set
            {
                SetValue(ImgWidthDependency, value);
            }
        }
        public ImageSource ImgSource
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
        static SmallButtons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SmallButtons), new FrameworkPropertyMetadata(typeof(SmallButtons)));
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            MainRectangle = GetTemplateChild("MainRectangle") as DockPanel;
            MainRectangle.MouseEnter += new System.Windows.Input.MouseEventHandler(MainGrid_MouseEnter);
            MainRectangle.MouseLeave += new System.Windows.Input.MouseEventHandler(MainGrid_MouseLeave);

            if (DisallowDefaultToolTip == false)
            {
                TextToolTip = GetTemplateChild("ToolTip") as TextBlock;
                Binding ToolTipTextBinding = new Binding("ToolTipText")
                {
                    Source = this,
                    Mode = BindingMode.TwoWay
                };
                TextToolTip.SetBinding(TextBlock.TextProperty, ToolTipTextBinding);
            }
            img = GetTemplateChild("Image") as System.Windows.Controls.Image;
            Binding ImageHeightBinding = new Binding("ImageHeight")
            {
                Source = this,
                Mode = BindingMode.TwoWay
            };
            img.SetBinding(Image.HeightProperty, ImageHeightBinding);

            Binding ImageWidthBinding = new Binding("ImageWidth")
            {
                Source = this,
                Mode = BindingMode.TwoWay
            };
            img.SetBinding(Image.WidthProperty, ImageWidthBinding);

            Binding ImageSourceBinding = new Binding("ImageSource")
            {
                Source = this,
                Mode = BindingMode.TwoWay
            };
            img.SetBinding(Image.SourceProperty, ImageSourceBinding);

            Binding ImageHorizontalAlignmentBinding = new Binding("ImageHorizontalAlignment")
            {
                Source = this,
                Mode = BindingMode.TwoWay
            };
            img.SetBinding(Image.HorizontalAlignmentProperty, ImageHorizontalAlignmentBinding);
            ImgHorizontalAlignment = HorizontalAlignment.Stretch;

            Binding ImageDefaultOpacityHorizontalAlignmentBinding = new Binding("ImageOpacity")
            {
                Source = this,
                Mode = BindingMode.TwoWay
            };
            img.SetBinding(Image.OpacityProperty, ImageDefaultOpacityHorizontalAlignmentBinding);

        }
        private void MainGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            ImgOpacity = ImageMOOpacity;
            if (AllowHoverAnimation == false)
            {
                     MainRectangle.Background = MouseEnterColor;
            }
            else
            {
                ColorAnimation colorChangeAnimation = new ColorAnimation();
                colorChangeAnimation.To = (Color)ColorConverter.ConvertFromString(MouseEnterColor.ToString());
                colorChangeAnimation.Duration = TimeSpan.FromMilliseconds(200);
                PropertyPath colorTargetPath = new PropertyPath("(DockPanel.Background).(SolidColorBrush.Color)");
                Storyboard CellBackgroundChangeStory = new Storyboard();
                Storyboard.SetTarget(colorChangeAnimation, MainRectangle);
                Storyboard.SetTargetProperty(colorChangeAnimation, colorTargetPath);
                CellBackgroundChangeStory.Children.Add(colorChangeAnimation);
                CellBackgroundChangeStory.Begin();
            }
       
        }
        private void MainGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            ImgOpacity = ImageMLOpacity;
            MainRectangle.Background = this.Background;
        }
        
    }
}
