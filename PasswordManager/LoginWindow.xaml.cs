using System.Windows;
using System.Windows.Input;

namespace PasswordManager
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            // Primer inicio: cambiar el título para indicar que se debe crear una nueva contraseña
            if (!PasswordHash.PasswordFileExists())
                TitleText.Text = "CREAR CONTRASEÑA NUEVA";
        }

        /// <summary>
        /// Gestiona la creación inicial de la contraseña y la validación de inicios posteriores.
        /// </summary>
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string password = txtPassword.Password;

            // Primer inicio: no existe archivo → guardar contraseña
            if (!PasswordHash.PasswordFileExists())
            {
                // Validar que el usuario haya introducido una contraseña
                if (string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("La contraseña no puede estar vacía.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Opcional: validar longitud mínima
                if (password.Length < 6)
                {
                    MessageBox.Show("La contraseña debe tener al menos 6 caracteres.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                PasswordHash.SavePassword(password);
                MessageBox.Show("Contraseña creada correctamente.", "Éxito", MessageBoxButton.OK);
            }
            else
            {
                // Inicio normal: validar contraseña
                if (!PasswordHash.ValidatePassword(password))
                {
                    MessageBox.Show("Contraseña incorrecta.", "Error", MessageBoxButton.OK);
                    return;
                }
            }

            // Si la contraseña se creó o validó correctamente, abrir la aplicación
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        /// <summary>
        /// Permite arrastrar la ventana haciendo clic en el área superior.
        /// </summary>
        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}
