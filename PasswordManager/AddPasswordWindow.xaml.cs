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
using static PasswordManager.MainWindow;

namespace PasswordManager
{
    /// <summary>
    /// Lógica de interacción para AddPasswordWindow.xaml
    /// </summary>
    public partial class AddPasswordWindow : Window
    {
        // Contendrá la contraseña creada por el usuario cuando se cierre el diálogo.
        public PasswordEntry NewPassword { get; private set; }
        
        // Constructor: Inicializa la UI del diálogo.
        public AddPasswordWindow()
        {
            InitializeComponent();
        }

        // Valida los campos obligatorios, crea la nueva entrada y cierra el diálogo devolviendo 'true'.
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSite.Text) || string.IsNullOrWhiteSpace(txtPassword.Password))
            {
                MessageBox.Show("Por favor completa los campos obligatorios.","Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NewPassword = new PasswordEntry()
            {
                Site = txtSite.Text,
                Username = txtUsername.Text,
                Password = txtPassword.Password
            };

            DialogResult = true;
            this.Close();
        }

        // Evento botón cerrar.
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Método para generar una contraseña aleatoria de 12 caracteres.
        private void GeneratePassword_Click(object sender, RoutedEventArgs e)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%&*?";
            Random random = new Random();

            string password = new string(
                Enumerable.Repeat(chars, 12)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray()
            );

            txtPassword.Password = password;
            Clipboard.SetText(password);
        }
    }
}
