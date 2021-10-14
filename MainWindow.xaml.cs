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
using System.Windows.Input.Manipulations;
using Microsoft.Win32;

namespace WPF_Text_Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String path;

        public static RoutedCommand AboutCmd = new RoutedCommand();
        public static RoutedCommand ClearCmd = new RoutedCommand();
        public static RoutedCommand LoadCmd = new RoutedCommand();
        public static RoutedCommand SaveCmd = new RoutedCommand();
        public static RoutedCommand SaveAsCmd = new RoutedCommand();
        public static RoutedCommand CloseCmd = new RoutedCommand();
        public static RoutedCommand CopyCmd = new RoutedCommand();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void fileSaver()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text Files | *.txt";
            dialog.DefaultExt = "txt";

            bool? resultat = dialog.ShowDialog();
            if (resultat.HasValue && resultat.Value)
            {
                path = dialog.FileName;
                ContenuFichier.Text = File.ReadAllText(path);
                NomFichier.Text = "Path: " + path;
            }
        }

        private void AboutCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Thanks for using the Лучший Text Editor!\nVisit our website at http://government.ru/");
        }

        private void AboutCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ClearCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Voulez-vous effacer tout le contenu du fichier?", "MyApp", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                ContenuFichier.Text = "";
            }
        }

        private void ClearCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

            e.CanExecute = !ContenuFichier.Text.Equals("");
        }

        private void LoadCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (path != null)
            {
                if (ContenuFichier.Text.Equals(File.ReadAllText(path)))
                //System.ArgumentException: 'Empty path name is not legal. (Parameter 'path')' fiuHGTUEHBR;OA
                {
                    fileSaver();
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Warning, opening without saving will result in content loss. Do you want to open a new file?", "MyApp", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        fileSaver();
                    }
                }
            }
            else
            {
                if (!ContenuFichier.Text.Equals(""))
                {
                    MessageBoxResult result = MessageBox.Show("Warning, opening without saving will result in content loss. Do you want to open a new file?", "MyApp", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        fileSaver();
                    }
                }
                else
                {
                    fileSaver();
                }
            }
        }

        private void LoadCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

            e.CanExecute = true;

        }

        private void SaveCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (path != null)
            {
                File.WriteAllText(path, ContenuFichier.Text);
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text Files | *.txt";
                saveFileDialog.DefaultExt = "txt";
                if (saveFileDialog.ShowDialog() == true)
                {
                    File.WriteAllText(saveFileDialog.FileName, ContenuFichier.Text);
                }
                path = saveFileDialog.FileName;
            }
        }

        private void SaveCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = path != null;
        }
        private void SaveAsCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files | *.txt";
            saveFileDialog.DefaultExt = "txt";
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, ContenuFichier.Text);
            }
            path = saveFileDialog.FileName;
        }

        private void SaveAsCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CloseCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            if (path != null)
            {
                if (ContenuFichier.Text.Equals(File.ReadAllText(path)))
                {
                    ContenuFichier.Text = "";
                    path = null;
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Close file without saving?", "MyApp", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        ContenuFichier.Text = "";
                        path = null;
                    }
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("No open file to close");
            }
        }

        private void CloseCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = path != null;
        }
    }
}