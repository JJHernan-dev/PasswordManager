using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PasswordManager
{
    // Clase auxiliar para gestionar el hash de la contraseña maestra.
    // Se encarga de almacenar, validar y generar el hash mediante SHA-256.
    public static class PasswordHash
    {
        // Archivo donde se almacena el hash de la contraseña maestra.
        // NOTA: En una aplicación real debería ubicarse en un directorio seguro.
        private static readonly string FilePath = "password.dat";

        // Verifica si el archivo que contiene el hash de la contraseña maestra existe.
        public static bool PasswordFileExists()
        {
            return File.Exists(FilePath);
        }

        // Calcula el hash de la contraseña proporcionada y lo guarda en el archivo.
        // No se almacena la contraseña en texto plano, solo su hash.
        public static void SavePassword(string password)
        {
            var hash = ComputeHash(password);
            File.WriteAllText(FilePath, hash);
        }

        // Compara la contraseña ingresada por el usuario con el hash almacenado.
        // Devuelve true si coinciden; false si el archivo no existe o el hash no coincide.
        public static bool ValidatePassword(string password)
        {
            if (!PasswordFileExists())
                return false;

            string storedHash = File.ReadAllText(FilePath);
            return storedHash == ComputeHash(password);
        }

        // Calcula el hash SHA-256 de la cadena de entrada y lo devuelve en Base64.
        // SHA-256 garantiza que la contraseña no se almacene en forma legible.
        private static string ComputeHash(string input)
        {
            using SHA256 sha = SHA256.Create();
            byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(bytes);
        }
    }
}
