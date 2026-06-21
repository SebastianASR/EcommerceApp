# ⚡ Z-COMMERCE — Enterprise E-Commerce Platform

![.NET](https://img.shields.io/badge/.NET%209.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/Neon%20Postgres-4169E1?style=for-the-badge&logo=postgresql&logoColor=white)
![Transbank](https://img.shields.io/badge/Webpay%20Plus-FF6C00?style=for-the-badge&logo=cashapp&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap%205.3-7952B3?style=for-the-badge&logo=bootstrap&logoColor=white)
![Architecture](https://img.shields.io/badge/Arquitectura-MVC-008080?style=for-the-badge)

**Z-Commerce** es una plataforma de comercio electrónico de alto rendimiento desarrollada en **ASP.NET Core MVC**. Diseñada bajo una arquitectura limpia y escalable, simula el flujo completo de una tienda de hardware avanzado, desde la gestión de inventario en base de datos relacional hasta la conciliación de pagos reales mediante integración bancaria.

---

## 📸 Vista Previa de la Interfaz

| Inicio (Hero Cyber-Universe) | Panel de Carrito (Async) |
| :--- | :--- |
| ![Portada](https://github.com/user-attachments/assets/37009572-61a5-42ae-a574-e0a20313d865) | ![Carrito](https://github.com/user-attachments/assets/53c5bba0-938c-4868-8cdb-c9821f09e3bf) |

---

## 🚀 Características Principales (Core Features)

* **Arquitectura MVC:** Separación estricta de lógica de negocio, acceso a datos y renderizado de vistas mediante motor **Razor**.
* **Integración Transbank Webpay Plus:** Consumo de la API REST oficial de Webpay Plus v1.2 mediante `HttpClient`, gestionando transacciones de inicialización y retorno seguro.
* **Persistencia en la Nube:** Conexión nativa a base de datos **PostgreSQL alojada en Neon.tech** a través de *Entity Framework Core*.
* **Seguridad y Control de Acceso:** Implementación de **ASP.NET Core Identity** con encriptación de contraseñas PBKDF2, manejo de sesiones y división de roles (`Admin` / `Customer`).
* **Estado de Carrito Asíncrono:** Carrito de compras persistido en memoria de sesión (`HttpContext.Session`), sincronizado en tiempo real mediante peticiones `Fetch API` sin recarga de página.
* **UI/UX Premium:** Interfaz basada en *Glassmorphism*, transiciones fluidas de físicas cúbicas y tipografía de alta legibilidad (*Inter*).

---

## 🛠️ Tecnologías y Stack

* **Backend:** C# / ASP.NET Core 9.0.301 (MVC)
* **ORM:** Entity Framework Core (Npgsql)
* **Base de Datos:** PostgreSQL (Neon Cloud)
* **Frontend:** HTML5, CSS3, Bootstrap 5.3, FontAwesome 6.4, JavaScript Vanilla
* **Pasarela de pagos:** Transbank Webpay SDK / REST API

---

## ⚙️ Configuración y Despliegue Local

1. **Clonar:** `git clone https://github.com/SebastianASR/EcommerceApp.git`
2. **Configurar:** Renombra `appsettings.Example.json` a `appsettings.json` y configura tu `NeonConnection` en los `ConnectionStrings`.
3. **Ejecutar:**
```bash
dotnet ef database update
dotnet run

Rol,Usuario,Contraseña,Permisos
Admin,admin@zcommerce.cl,ZCommerce2026!,"CRUD, Leads, Gestión"
Cliente,cliente@zcommerce.cl,Cliente123!,"Carrito, Checkout"

🌐 Demo en vivo: https://z-commerce-dh8r.onrender.com/

Desarrollado por Sebastián Sandoval Romero Ingeniero en Informática — Santiago, Chile.
