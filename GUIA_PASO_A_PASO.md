# ?? GUÍA DE IMPLEMENTACIÓN PASO A PASO

## ?? Tiempo estimado: 30 minutos

---

## PASO 1: PREPARAR LA BASE DE DATOS (10 minutos)

### 1.1 Ejecutar Scripts de Creación de Tablas

Abre **SQL Server Management Studio** y ejecuta:

```sql
-- ============================================
-- CREAR TABLA DE PERMISOS
-- ============================================
CREATE TABLE Permiso (
    Nombre_Permiso NVARCHAR(100) PRIMARY KEY,
    Tipo NVARCHAR(20),           -- 'Simple' o 'Compuesto'
    Rol BIT                       -- 1 para roles, 0 para familias
);

-- ============================================
-- CREAR TABLA DE RELACIONES
-- ============================================
CREATE TABLE PermisoPermisos (
    Nombre_PermisoCompuesto NVARCHAR(100),
    Nombre_Permiso NVARCHAR(100),
    PRIMARY KEY (Nombre_PermisoCompuesto, Nombre_Permiso),
    FOREIGN KEY (Nombre_PermisoCompuesto) REFERENCES Permiso(Nombre_Permiso),
    FOREIGN KEY (Nombre_Permiso) REFERENCES Permiso(Nombre_Permiso)
);

-- ============================================
-- VERIFICAR TABLAS CREADAS
-- ============================================
SELECT * FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_NAME IN ('Permiso', 'PermisoPermisos');
```

### 1.2 Cargar Datos Iniciales

```sql
-- ============================================
-- INSERTAR ROLES BASE
-- ============================================
INSERT INTO Permiso (Nombre_Permiso, Tipo, Rol) VALUES 
('Cajero', 'Simple', 1),
('Vendedor', 'Simple', 1),
('Administrador', 'Simple', 1);

-- ============================================
-- INSERTAR PERMISOS SIMPLES
-- ============================================
INSERT INTO Permiso (Nombre_Permiso, Tipo, Rol) VALUES 
('CargarCarrito', 'Simple', 0),
('ConsultarProducto', 'Simple', 0),
('GenerarFactura', 'Simple', 0),
('GestionUsuario', 'Simple', 0),
('GestionPerfiles', 'Simple', 0),
('GestionIdiomas', 'Simple', 0),
('Clientes', 'Simple', 0),
('Productos', 'Simple', 0);

-- ============================================
-- CREAR FAMILIAS (Compuestos) - Opcional
-- ============================================
INSERT INTO Permiso (Nombre_Permiso, Tipo, Rol) VALUES 
('GestionCompleta', 'Compuesto', 0),
('VentasAvanzadas', 'Compuesto', 0);

-- ============================================
-- ASIGNAR PERMISOS A FAMILIAS
-- ============================================
INSERT INTO PermisoPermisos (Nombre_PermisoCompuesto, Nombre_Permiso) VALUES 
('GestionCompleta', 'CargarCarrito'),
('GestionCompleta', 'ConsultarProducto'),
('GestionCompleta', 'GenerarFactura'),
('GestionCompleta', 'GestionUsuario'),
('VentasAvanzadas', 'CargarCarrito'),
('VentasAvanzadas', 'ConsultarProducto'),
('VentasAvanzadas', 'GenerarFactura');

-- ============================================
-- VERIFICAR DATOS
-- ============================================
SELECT * FROM Permiso;
SELECT * FROM PermisoPermisos;
```

---

## PASO 2: VERIFICAR COMPILACIÓN (5 minutos)

### 2.1 Compilar Solución

1. Abre tu solución en Visual Studio
2. **Menú:** Generar ? Compilar solución
3. Verifica en la ventana de Salida:
   ```
   Compilación completada... 0 errores, 0 advertencias
   ```

### 2.2 Verificar Proyectos

```
? Entidad_BE
   ?? TipoPermiso.cs
   ?? Permiso.cs
   ?? PermisoSimple.cs
   ?? Familia.cs

? Negocio_BLL
   ?? PerfilBLL.cs

? Acceso_DAL
   ?? PerfilDAL.cs

? TP_SanchezVillaverde
   ?? frmGestionPerfiles.cs
   ?? frmGestionPerfiles.Designer.cs
   ?? frmUsuario.cs (actualizado)
```

---

## PASO 3: VERIFICAR CONFIGURACIÓN DE BD (5 minutos)

### 3.1 Revisar ConnectionString

Verifica que `Acceso_DAL/AccesoDatos.cs` tenga la conexión correcta:

```csharp
private readonly string cadenaSQL = @"Data Source=KAINMPC;
                                       Initial Catalog=FerreDB;
                                       Integrated Security=True";
```

?? **Importante:** Reemplaza `KAINMPC` y `FerreDB` con tu servidor y BD.

### 3.2 Probar Conexión

En SQL Server Management Studio:
```sql
-- Cambiar la BD
USE FerreDB;

-- Verificar conexión
SELECT @@SERVERNAME AS ServerName;

-- Verificar tablas
SELECT * FROM Permiso;
SELECT * FROM PermisoPermisos;
SELECT * FROM Usuario;
```

---

## PASO 4: PRUEBA 1 - CREAR USUARIO (5 minutos)

### 4.1 Abrir frmUsuario

1. Ejecuta la aplicación (F5)
2. Login con usuario administrativo (si aplica)
3. Abre formulario Gestión de Usuarios

### 4.2 Crear Nuevo Usuario

1. Clickea **"Crear Usuario"**
2. Completa datos:
   ```
   DNI:      12345678
   Nombre:   Juan
   Apellido: Pérez
   Usuario:  jperez
   Dirección: Calle 123
   Teléfono: 1234567
   Email:    juan@example.com
   ```
3. Selecciona Rol: **"Vendedor"** (debe cargar de BD)
4. Clickea **"Guardar"**

### 4.3 Verificar

```sql
SELECT * FROM Usuario WHERE NombreUsuario = 'jperez';
```

Debe mostrar el usuario con Rol = "Vendedor"

---

## PASO 5: PRUEBA 2 - GESTIÓN DE PERFILES (5 minutos)

### 5.1 Abrir frmGestionPerfiles

1. Busca el formulario Gestión de Perfiles
2. Observa:
   ```
   ? TreeView con estructura de permisos
   ? ComboBox con familias cargadas
   ? CheckedListBox con permisos disponibles
   ? Botones: Crear, Guardar, Cancelar, Eliminar
   ```

### 5.2 Crear Nueva Familia

1. En **Nombre:** escribe `"VentasEspecial"`
2. Selecciona **RadioButton "Familia"**
3. Checkea permisos:
   - ? CargarCarrito
   - ? ConsultarProducto
   - ? GenerarFactura
4. Clickea **"Crear"**

### 5.3 Verificar en BD

```sql
SELECT * FROM Permiso WHERE Nombre_Permiso = 'VentasEspecial';
SELECT * FROM PermisoPermisos 
WHERE Nombre_PermisoCompuesto = 'VentasEspecial';
```

---

## PASO 6: PRUEBA 3 - VALIDACIONES (5 minutos)

### 6.1 Validar Nombre Duplicado

1. Intenta crear otra familia con nombre **"Vendedor"** (ya existe)
2. Debe mostrar: **"Nombre repetido"**

### 6.2 Validar Referencia Circular

1. Crea familia **"Familia1"** con permisos simples
2. Crea familia **"Familia2"** con permisos simples
3. Intenta asignar **"Familia1"** como permiso de **"Familia2"**
4. Intenta asignar **"Familia2"** como permiso de **"Familia1"**
5. Debe detectar ciclo: **"Referencia circular detectada"**

### 6.3 Validar Perfil en Uso

1. Asigna usuario "Juan Pérez" al rol "Vendedor"
2. Intenta eliminar rol "Vendedor"
3. Debe mostrar: **"El perfil está en uso"**

---

## PASO 7: VERIFICACIÓN FINAL (Bonus - 2 minutos)

### 7.1 Revisar Bitácora

```sql
SELECT * FROM Bitacora 
ORDER BY Fecha DESC;
```

Debe registrar:
- Login del usuario
- Creación de usuario "jperez"
- Creación de familia "VentasEspecial"
- Etc.

### 7.2 Revisar Estructura de Árbol

En frmGestionPerfiles, el árbol debe mostrar:
```
?? Vendedor
?? Cajero
?? Administrador
?? CargarCarrito
...
?? VentasEspecial (Rol=true, color rojo)
   ?? CargarCarrito
   ?? ConsultarProducto
   ?? GenerarFactura
```

---

## ? CHECKLIST DE FINALIZACIÓN

```
[ ] Tablas Permiso y PermisoPermisos creadas
[ ] Datos iniciales cargados
[ ] Compilación exitosa (0 errores)
[ ] Conexión a BD verificada
[ ] Usuario creado exitosamente
[ ] Familia de permisos creada exitosamente
[ ] Validaciones funcionando (duplicados, ciclos)
[ ] Bitácora registra eventos
[ ] Roles cargados dinámicamente en frmUsuario
[ ] TreeView muestra estructura correcta
[ ] Colores diferenciados (rojo=rol, azul=familia)
[ ] Documentación revisada
```

---

## ?? SOLUCIÓN DE PROBLEMAS

### Problema: "Conexión denegada a BD"
```
? Revisar ConnectionString en AccesoDatos.cs
? Verificar servername y database
? Confirmar permisos de usuario SQL
? Probar conexión desde SSMS
```

### Problema: "Tabla Permiso no existe"
```
? Ejecutar scripts SQL de creación
? Verificar nombre de tabla en SELECT
? Confirmar BD correcta (USE FerreDB;)
```

### Problema: "ComboBox vacío en frmUsuario"
```
? Verificar datos en Permiso WHERE Rol = 1
? Revisar try-catch en frmUsuario_Load
? Confirmar PerfilBLL compila correctamente
? Consultar eventlog de aplicación
```

### Problema: "Referencia circular no detectada"
```
? Revisar método VerificarReferenciaCircular()
? Confirmar está siendo llamado
? Verificar lógica recursiva
? Agregar console.writeline para debug
```

---

## ?? NOTAS IMPORTANTES

1. **Roles deben ser "Simple":** Para que aparezcan en frmUsuario
2. **Familias deben ser "Compuesto":** Para estructura jerárquica
3. **Bit de Rol:** 1 = Rol, 0 = Familia
4. **Nombres únicos:** Namespace Permiso usa Nombre_Permiso como clave primaria
5. **Cascada de permisos:** Familia puede contener otras familias

---

## ?? PRÓXIMOS PASOS AVANZADOS

### Después de completar esta guía:

1. **Integración en Menú Principal**
   ```csharp
   // En frmMenu.cs, cargar permisos del usuario
   var permisos = perfilBLL.ObtenerHijosDeFamilia(usuarioActual.rol);
   // Habilitar/deshabilitar opciones según permisos
   ```

2. **Reporte de Auditoría**
   ```csharp
   // Consultar bitácora por usuario/fecha
   SELECT * FROM Bitacora 
   WHERE Usuario = 'jperez' 
   AND Fecha >= DATEADD(DAY, -30, GETDATE());
   ```

3. **Sincronización de Permisos**
   ```csharp
   // Implementar caché para mejorar performance
   // Revalidar permisos en operaciones críticas
   ```

---

**Guía Práctica Completada**  
**Tiempo Total: ~30 minutos**  
**Dificultad: Media**  
**Resultado: Sistema totalmente funcional ?**
