using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Forms;
using System;
using System.Windows.Media.Animation;
using System.Threading.Tasks;

namespace ArnDESK
{
    public partial class SlideWidget : Window
    {
        int Count; string SlideType;
        bool MenuOpen;
        private Timer MainCounter = new Timer();
        private Storyboard Sb;
        int C = 0;
        private void ShowOption(object sender, MouseButtonEventArgs e)
        {
            MenuOpen = true;
            var cm = ContextMenuService.GetContextMenu(sender as DependencyObject);
            if (cm == null)
                return;
            else
            {
                cm.Placement = PlacementMode.Left;
                cm.PlacementTarget = MainBody;
                cm.IsOpen = true;
            }
        }
        private void SlideOnHover(object sender, RoutedEventArgs e)
        {
            C = 0;
            Count = 05; SlideType = "SlideIn";
            MainCounter.Start();
        }
        private void SlideOnLeave(object sender, RoutedEventArgs e)
        {
            if (MenuOpen != true)
            {
                C = 0;
                Count = 10; SlideType = "SlideOut";
                MainCounter.Start();
            }
        }
        private void MainCounter_Tick(object sender, EventArgs e)
        {
            C += 1;
            if (C == Count)
            {
                MainCounter.Stop();
                Sb = (Storyboard)FindResource(SlideType);
                Storyboard.SetTarget(Sb, MainCan);
                Sb.Begin();
            }
        }
        private void Window_Initialized(object sender, EventArgs e)
        {
            var desktopWorkingArea = SystemParameters.WorkArea;
            Topmost = true;
            Top = (desktopWorkingArea.Bottom / 2) - (Height / 2);
            Left = desktopWorkingArea.Right - Width;
            Sb = (Storyboard)FindResource("SlideIn");
            Storyboard.SetTarget(Sb, MainCan);
            Sb.Begin();
            MainCounter.Tick += new EventHandler(MainCounter_Tick);
            Count = 20; SlideType = "SlideOut";
            MainCounter.Start();
        }
        private void ContextMenu_Closed(object sender, RoutedEventArgs e)
        {
            if (cnf.IsOpen == false && cnc.IsOpen == false && cnp.IsOpen == false)
            {
                MenuOpen = false;
                SlideOnLeave(sender, e);
            }
        }
    }
}