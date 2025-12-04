using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using static PasswordManager.MainWindow;

namespace PasswordManager.Utils
{
    // Clase encargada de gestionar la persistencia de datos en disco.
    // Guarda y carga las contraseñas en formato JSON, aplicando cifrado/des-cifrado.
    internal class StorageManager
    {
        // Archivo donde se almacenarán las contraseñas cifradas.
        private static readonly string FilePath = "passwords.json";

        // Guarda la lista de contraseñas en un archivo JSON.
        // Antes de almacenar, cifra cada contraseña para evitar exponer datos sensibles.
        public static void SavePasswords(ObservableCollection<PasswordEntry> passwords)
        {
            // Se genera una nueva lista con las contraseñas cifradas,
            // manteniendo el resto de campos en texto claro.
            var encryptedList = passwords.Select(p => new PasswordEntry
            {
                Site = p.Site,
                Username = p.Username,
                Password = EncryptionHelper.Encrypt(p.Password)
            }).ToList();

            // Serialización en formato JSON con formato legible.
            var json = JsonSerializer.Serialize(encryptedList, new JsonSerializerOptions { WriteIndented = true });

            // Se guarda el resultado en disco.
            File.WriteAllText(FilePath, json);
        }

        // Carga las contraseñas almacenadas en el archivo JSON.
        // Si el archivo no existe, devuelve una lista vacía.
        // Las contraseñas son descifradas antes de devolverlas a la aplicación.
        public static ObservableCollection<PasswordEntry> LoadPasswords()
        {
            if (!File.Exists(FilePath))
                return new ObservableCollection<PasswordEntry>();

            var json = File.ReadAllText(FilePath);

            // Se deserializa la lista tal cual fue guardada (con contraseñas cifradas).
            var encryptedList = JsonSerializer.Deserialize<List<PasswordEntry>>(json);

            // Se crea una nueva lista donde las contraseñas vuelven a texto claro.
            var decryptedList = encryptedList.Select(p => new PasswordEntry
            {
                Site = p.Site,
                Username = p.Username,
                Password = EncryptionHelper.Decrypt(p.Password)
            });

            return new ObservableCollection<PasswordEntry>(decryptedList);
        }
    }
}
