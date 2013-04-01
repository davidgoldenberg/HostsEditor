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

namespace HostEditorGui
{
    using Hosts.Core;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HostFile file;

        public MainWindow()
        {
            InitializeComponent();
            this.SizeChanged += (sender, args) => Status.Width = this.Width;
            file = new HostFile(null);
            this.Path.Content = file.HostPath;
            StatusLabel.Content = "Loaded";
        }
    }
}