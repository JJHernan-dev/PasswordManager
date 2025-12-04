using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PasswordManager.Utils
{
    internal static class EncryptionHelper
    {
        // Clave simétrica utilizada para el cifrado AES (32 bytes = 256 bits).
        // Debe mantenerse privada y almacenarse de forma segura en un entorno real.
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("MySuperSecureKey1234567890ABCDEF");

        public static string Encrypt(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;

                // Generamos un IV aleatorio para reforzar la seguridad del cifrado.
                aes.GenerateIV();

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream())
                {
                    // El IV se escribe al inicio del flujo para poder recuperarlo durante el descifrado.
                    ms.Write(aes.IV, 0, aes.IV.Length);

                    // El CryptoStream se encarga de aplicar el cifrado al escribir en el MemoryStream.
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (var sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText); // Escribimos el texto a cifrar
                    }

                    // Convertimos todo el contenido (IV + datos cifrados) a Base64 para almacenamiento.
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        /// <summary>
        /// +++++++++++++++++++ MÉTODO Decrypt ++++++++++++++++++++++++++++++++++++
        /// Desencripta un texto previamente cifrado con AES y codificado en Base64.
        /// El método extrae el IV incrustado al inicio del mensaje cifrado, reconstruye
        /// el descifrador AES con la clave e IV originales y devuelve el texto plano.
        /// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        /// </summary>
        public static string Decrypt(string cipherText)
        {
            // El texto cifrado se recibió codificado en Base64, por lo que lo convertimos a bytes
            var fullCipher = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;

                // El IV fue almacenado al inicio del bloque cifrado: lo extraemos
                byte[] iv = new byte[aes.BlockSize / 8];
                byte[] cipher = new byte[fullCipher.Length - iv.Length];

                // Copiamos IV y datos cifrados en buffers separados
                Array.Copy(fullCipher, iv, iv.Length);
                Array.Copy(fullCipher, iv.Length, cipher, 0, cipher.Length);

                aes.IV = iv;

                // Creamos el descifrador con la clave y el IV recuperados
                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream(cipher))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var sr = new StreamReader(cs))
                {
                    // Leemos el contenido ya descifrado y lo devolvemos
                    return sr.ReadToEnd();
                }
            }
        }
    }
}
