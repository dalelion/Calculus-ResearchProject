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

namespace Calculus_ResearchProject {
    /// <summary>
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void B_TaylorSeries_Click( object sender, RoutedEventArgs e ) {
            new TaylorWindow().Show();
            this.Close();
        }

        private void B_Classic_Click( object sender, RoutedEventArgs e ) {
            new ClassicWindow().Show();
            this.Close();
        }

        private void B_Parser_Click( object sender, RoutedEventArgs e ) {

        }
    }
}
