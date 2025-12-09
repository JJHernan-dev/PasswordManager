# 🔐 Password Manager (WPF)

- Sencillo **gestor de contraseñas** desarrollado en **C# y WPF**.
- Contiene encriptación básica, persistencia de datos y diseño en UI/UX.


---

## 📸 Capturas
<img width="347" height="230" alt="1" src="https://github.com/user-attachments/assets/ea3a8d6c-b2e8-4055-b67c-4ff04512d706" />
<img width="694" height="450" alt="2" src="https://github.com/user-attachments/assets/c2c6b4b5-25d5-4305-8deb-459e5b6cdd63" />
<img width="697" height="446" alt="3" src="https://github.com/user-attachments/assets/a806a19a-55a0-4bba-b3a3-a557d4bb62a1" />
<img width="695" height="446" alt="4" src="https://github.com/user-attachments/assets/a5d994c8-1f62-4333-9428-dd47ebabb0fd" />

---

## ✨ Características

- ✔ **Inicio de sesión seguro**
  - Contraseña protegida mediante **hash + salt**.
  - Si es el primer uso, el usuario crea su contraseña maestra.

- ✔ **Gestión de contraseñas**
  - Guarda sitios, usuarios y contraseñas.
  - Oculta/Muestra contraseña con un clic.
  - Elimina registros fácilmente.

- ✔ **Interfaz moderna**
  - Ventanas sin borde.
  - Esquinas redondeadas.
  - Colores estilo *Visual Studio / Fluent Dark*.

- ✔ **Generador de contraseñas aleatorias**
  - Contraseñas seguras con un botón.

- ✔ **Datos almacenados localmente**
  - Las contraseñas se guardan en un archivo JSON.
  - La contraseña maestra se almacena **hasheada**, nunca en texto plano.

---

## 🛠️ Tecnologías utilizadas

- **WPF (.NET Framework / .NET Core)**
- **C#**
- `SHA256` + salt para proteger la clave maestra
- Archivos JSON para persistencia
- XAML para la interfaz y estilos personalizados
