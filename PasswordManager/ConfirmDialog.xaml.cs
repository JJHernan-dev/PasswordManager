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

namespace PasswordManager
{
    /// <summary>
    /// Lógica de interacción para ConfirmDialog.xaml
    /// </summary>
    public partial class ConfirmDialog : Window
    {

        // Indica si el usuario confirmó la operación (true) o la canceló (false).
        public bool Result { get; private set; } = false;

        // Recibe el mensaje a mostrar y lo asigna al TextBlock del diálogo.
        public ConfirmDialog(String message)
        {
            InitializeComponent();
            MessageText.Text = message;
        }
        //Evento cancelar.
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Result = false;
            Close();
        }

        //Evento confirmar.
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            Result = true;
            Close();
        }
    }
}
