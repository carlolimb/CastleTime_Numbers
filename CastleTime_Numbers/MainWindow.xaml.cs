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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;

namespace CastleTime_Numbers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Simulations sim_page = new Simulations();
        analysis_page analz_page = new analysis_page();

        public MainWindow()
        {
            InitializeComponent();
        
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            frame_lbl.Text = "Simulations";
            Main_Frame.Navigate(sim_page);
        }

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            frame_lbl.Text = "Analysis";
            Main_Frame.Navigate(analz_page);
        }
    }
}
