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

using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Diagnostics;

namespace KohTaskManager {
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window {
        private DispatcherTimer timer;
        private ObservableCollection<Process> processes = new ObservableCollection<Process>();
        
        public MainWindow() {
            InitializeComponent();

            timer = new DispatcherTimer(DispatcherPriority.Normal);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += (sender, e) => {
                processes.Clear();
                foreach ( var p in Process.GetProcesses() ) {
                    processes.Add(p);
                }
            };
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            dataGridProcesses.ItemsSource = processes;
            timer.Start();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            timer.Stop();
        }

        private void dataGridProcesses_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e) {
            switch ( e.PropertyName ) {
            case "ProcessName":
                e.Column.Header = "プロセス名";
                e.Column.DisplayIndex = 0;
                break;
            default:
                e.Cancel = true;
                break;
            }
        }
    }
}
