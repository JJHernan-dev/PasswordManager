using PasswordManager.Utils;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PasswordManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<PasswordEntry> Passwords { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            // Cargar contraseñas almacenadas en disco
            Passwords = StorageManager.LoadPasswords();
            dgPasswords.ItemsSource = Passwords;

            // Guardar automáticamente al cerrar la aplicación
            this.Closing += (s, e) => StorageManager.SavePasswords(Passwords);
        }

        private void AddPassword_Click(object sender, RoutedEventArgs e)
        {
            // Abrir ventana modal para añadir una nueva contraseña
            AddPasswordWindow addWindow = new AddPasswordWindow();
            addWindow.Owner = this;
            if (addWindow.ShowDialog() == true)
            {
                // Añadir nueva entrada y guardar cambios
                Passwords.Add(addWindow.NewPassword);
                try
                {
                    StorageManager.SavePasswords(Passwords);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al guardar: {ex.Message}");
                }
            }
        }

        private void DeletePassword_Click(Object sender, RoutedEventArgs e)
        {
            // Obtener la entrada asociada al botón pulsado
            var button = (Button)sender;
            var entry = button.Tag as PasswordEntry;   

            if (entry == null)
                return;

            var site = entry.Site;

            // Confirmación antes de eliminar
            var dialog = new ConfirmDialog($"¿Seguro que quieres eliminar la contraseña del sitio: {site}?");
            dialog.Owner = this;
            dialog.ShowDialog();

            if (!dialog.Result)
                return;

            // Eliminar y guardar
            Passwords.Remove(entry);

            try
            {
                StorageManager.SavePasswords(Passwords);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}");
            }
        }

        private void TogglePasswordVisibility(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var panel = button.Parent as StackPanel;

            // Buscar elementos de texto dentro del template
            var hidden = panel.FindName("txtHiddenPassword") as TextBlock;
            var real = panel.FindName("txtRealPassword") as TextBlock;

            // Fallback si el FindName no encuentra los elementos
            if (hidden == null || real == null)
            {
                foreach (var child in panel.Children)
                {
                    if (child is TextBlock tb)
                    {
                        if (tb.Name == "txtHiddenPassword") hidden = tb;
                        if (tb.Name == "txtRealPassword") real = tb;
                    }
                }
            }

            // Alternar visibilidad
            if (real.Visibility == Visibility.Collapsed)
            {
                real.Visibility = Visibility.Visible;
                hidden.Visibility = Visibility.Collapsed;
                button.Content = "🙈";
            }
            else
            {
                real.Visibility = Visibility.Collapsed;
                hidden.Visibility = Visibility.Visible;
                button.Content = "👁️";
            }
        }

        // Modelo utilizado por el DataGrid y el sistema de almacenamiento para representar cada contraseña guardada.
        public class PasswordEntry
        {
            public string Site { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
        }

        // Permite arrastrar la ventana desde la barra superior
        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        // Evento Minimizar ventana
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        // Evento Cerrar aplicación
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}