using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_copy_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Start copy");
            string targetPath = txt_target.Text;
            string sourcePath = txt_source.Text;
            if (string.IsNullOrEmpty(targetPath) || string.IsNullOrEmpty(sourcePath)) {
                MessageBox.Show("target path and source path not allow empty");
                return;
            }
            if (!Directory.Exists(sourcePath) || !Directory.Exists(targetPath)) {
                MessageBox.Show("target path and source path need exist");
                return;
            }
            try
            {
                string[] files = Directory.GetFiles(sourcePath, "*", SearchOption.AllDirectories);
                if (files == null || files.Count() <= 0) {
                    MessageBox.Show("Source files are null");
                    return;
                }
                Directory.CreateDirectory($@"{targetPath}\Backup_{string.Format(@"{0:yyyyMMdd}", DateTime.Now)}");
                targetPath = $@"{targetPath}\Backup_{string.Format(@"{0:yyyyMMdd}", DateTime.Now)}";
                foreach (string file in files) {
                    string fileName = System.IO.Path.GetFileName(file);
                    string destFile = file.Replace(sourcePath, targetPath);
                    string DirName = System.IO.Path.GetDirectoryName(destFile);
                    if (!Directory.Exists(DirName)) {
                        Directory.CreateDirectory(DirName);
                    }
                    File.Copy(file, destFile, true);
                }
            }
            catch (Exception ex) {
                MessageBox.Show($@"Copy files error => {ex.Message}");
                return;
            }
            MessageBox.Show("Finished copy");
        }
    }
}
