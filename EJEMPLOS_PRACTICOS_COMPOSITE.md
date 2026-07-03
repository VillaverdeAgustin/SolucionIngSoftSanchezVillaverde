# ?? EJEMPLOS PRÁCTICOS: PATRÓN COMPOSITE EN ACCIÓN

## ?? Objetivo
Mostrar cómo el patrón Composite se manifiesta en nuestro código real

---

## ?? EJEMPLO 1: Creando una Estructura Jerárquica

### Código en frmGestionPerfiles (button5_Click - Agregar familia)

```csharp
// CREANDO HOJAS (PermisoSimple)
var cargar = new PermisoSimple("CargarCarrito");
var consultar = new PermisoSimple("ConsultarProducto");
var factura = new PermisoSimple("GenerarFactura");

// CREANDO COMPOSITE (Familia)
var ventasBasica = new Familia("VentasBasica", false);

// COMPOSICIÓN: Agregar hojas al composite
ventasBasica.AgregarHijo(cargar);
ventasBasica.AgregarHijo(consultar);
ventasBasica.AgregarHijo(factura);

// Resultado: Composite que contiene hojas
// VentasBasica (Familia)
// ?? CargarCarrito (Hoja)
// ?? ConsultarProducto (Hoja)
// ?? GenerarFactura (Hoja)
```

**? Patrón Composite presente:**
- `PermisoSimple` = Hoja
- `Familia` = Composite
- `AgregarHijo()` = Operación de composición
- Ambos heredan de `Permiso` = Interfaz uniforme

---

## ?? EJEMPLO 2: Composición Anidada (Composite dentro de Composite)

### Escenario Real

```csharp
// NIVEL 1: Crear hojas
var ver = new PermisoSimple("Ver");
var crear = new PermisoSimple("Crear");
var editar = new PermisoSimple("Editar");
var eliminar = new PermisoSimple("Eliminar");
var descuento = new PermisoSimple("AplicarDescuento");

// NIVEL 2: Crear primer composite (con hojas)
var gestionBasica = new Familia("GestionBasica", false);
gestionBasica.AgregarHijo(ver);
gestionBasica.AgregarHijo(crear);

// NIVEL 2b: Crear segundo composite (con hojas)
var gestionAvanzada = new Familia("GestionAvanzada", false);
gestionAvanzada.AgregarHijo(editar);
gestionAvanzada.AgregarHijo(eliminar);
gestionAvanzada.AgregarHijo(descuento);

// NIVEL 3: Crear composite de nivel superior
var gestionCompleta = new Familia("GestionCompleta", true);  // Esto es un Rol

// ¡AQUÍ VIENE LO IMPORTANTE! Composite dentro de Composite
gestionCompleta.AgregarHijo(gestionBasica);      // ? Otro Composite
gestionCompleta.AgregarHijo(gestionAvanzada);    // ? Otro Composite
gestionCompleta.AgregarHijo(new PermisoSimple("Reportes"));  // ? Hoja extra

// RESULTADO FINAL: Estructura jerárquica de 3 niveles
// GestionCompleta (Familia/Rol) - Nivel 3
// ?? GestionBasica (Familia) - Nivel 2
// ?  ?? Ver (PermisoSimple)
// ?  ?? Crear (PermisoSimple)
// ?? GestionAvanzada (Familia) - Nivel 2
// ?  ?? Editar (PermisoSimple)
// ?  ?? Eliminar (PermisoSimple)
// ?  ?? AplicarDescuento (PermisoSimple)
// ?? Reportes (PermisoSimple)
```

**? Patrón Composite en su máxima expresión:**
- Composite (Familia) dentro de Composite (Familia)
- Sin límite de profundidad
- Esto **ES** el patrón Composite

---

## ?? EJEMPLO 3: Operaciones Recursivas Uniformes

### En PerfilDAL.ListaPermisosEnArbol()

```csharp
public List<Permiso> ListaPermisosEnArbol()
{
    // ... Lectura de BD ...

    // OPERACIÓN CLAVE: Construir árbol sin diferenciar tipos
    foreach (DataRow dr in ds.Tables[0].Rows)
    {
        if (dr[1].ToString() == "Simple")
        {
            // Crear HOJA
            listaPermiso.Add(new PermisoSimple(dr[0].ToString()));
        }
        else
        {
            // Crear COMPOSITE
            Familia pfamilia = new Familia(dr[0].ToString(), ...);
            listaPermiso.Add(pfamilia);  // ? Mismo tipo base (Permiso)
        }
    }

    // SEGUNDA OPERACIÓN: Establecer relaciones
    foreach (DataRow dr in ds.Tables[0].Rows)
    {
        // Buscar el composite padre
        Familia familialeida = (Familia)listapermisosCompuestos
            .Find(x => x.Nombre == dr[0].ToString());

        // Buscar el hijo (puede ser HOJA o COMPOSITE)
        Permiso permisoleido = listapermisosSimples
            .Find(x => x.Nombre == dr[1].ToString());

        // AGREGAR: Sin importar si el hijo es simple o compuesto
        if (familialeida != null && permisoleido != null)
        {
            familialeida.AgregarHijo(permisoleido);  // ? Polimorfismo
        }
    }
}
```

**? Characteristic Composite:**
- No importa si es `PermisoSimple` o `Familia`
- Todos se tratan como `Permiso`
- `AgregarHijo()` funciona para ambos casos
- ¡Recursión automática en la estructura!

---

## ?? EJEMPLO 4: Operación Recursiva en la UI

### En frmGestionPerfiles.LoadTreeRecursive()

```csharp
private void LoadTreeRecursive(Familia familiaActual, TreeNode parentNode)
{
    // Recorrer todos los hijos (sin importar si son simples o compuestos)
    foreach (var P in familiaActual.RetornarListaHijos())  // ? Interfaz común
    {
        TreeNode permisoHijo = new TreeNode(P.getPermisoNombre());

        // CLAVE: Distinguir EN TIEMPO DE EJECUCIÓN
        if (P is Familia familiaHijo)
        {
            // Si es COMPOSITE: recursividad
            if (familiaHijo.EsRol)
            {
                permisoHijo.ForeColor = Color.Red;
            }
            else
            {
                permisoHijo.ForeColor = Color.Blue;
            }

            LoadTreeRecursive(familiaHijo, permisoHijo);  // ? RECURSIÓN
        }
        else
        {
            // Si es HOJA: sin recursión necesaria
            // (RetornarListaHijos() retorna lista vacía)
        }

        parentNode.Nodes.Add(permisoHijo);
    }
}
```

**? Composite pattern en recursión:**
- Todos tienen `RetornarListaHijos()`
- Si devuelve lista vacía (hoja) ? sin recursión
- Si devuelve lista con elementos (composite) ? recursión
- **¡El patrón se maneja solo!**

---

## ?? EJEMPLO 5: Validación de Referencias Circulares

### En frmGestionPerfiles.VerificarReferenciaCircularRecursivo()

```csharp
// VALIDACIÓN COMPOSITE: Detectar ciclos en árbol
private bool VerificarReferenciaCircularRecursivo(Familia familiaBase, Familia familiaAgregar)
{
    // Base case: Si el nombre coincide ? ciclo detectado
    if (familiaBase.Nombre == familiaAgregar.Nombre)
    {
        return true;  // ¡Ciclo encontrado!
    }

    // Recursive case: Revisar todos los hijos del composite
    foreach (var hijo in familiaAgregar.RetornarListaHijos())
    {
        // Solo si el hijo es COMPOSITE continuar recursión
        if (hijo is Familia hijoFamilia)
        {
            if (VerificarReferenciaCircularRecursivo(familiaBase, hijoFamilia))
            {
                return true;  // ¡Ciclo encontrado en nivel más profundo!
            }
        }
        // Si es HOJA: no hay recursión (RetornarListaHijos es vacía)
    }

    return false;  // Sin ciclos
}
```

**Ejemplo de detección:**
```
Intentar: GestionCompleta.AgregarHijo(GestionCompleta)
   ?? familiaBase = "GestionCompleta"
   ?? familiaAgregar = "GestionCompleta"
   ?? Resultado: TRUE (ciclo detectado) ?

Intentar: Familia1.AgregarHijo(Familia2) donde Familia2.contiene(Familia1)
   ?? Nivel 1: Familia1 != Familia2
   ?? Recursión: Revisar hijos de Familia2
   ?? Nivel 2: Encuentra Familia1 como hijo
   ?? Resultado: TRUE (ciclo detectado) ?
```

---

## ?? EJEMPLO 6: Obtener Todos los Permisos de un Usuario

### Uso en el sistema

```csharp
// Cuando un usuario hace login (rolUsuario = "Vendedor")
PerfilBLL perfilBLL = new PerfilBLL();

// Obtener estructura de árbol completa
List<Permiso> permisoRol = perfilBLL.ObtenerHijosDeFamilia("Vendedor");

// PROCESSAMIENTO UNIFORME (sin distinguir tipos)
ProcessarPermisos(permisoRol);

private void ProcessarPermisos(List<Permiso> permisos)
{
    foreach (Permiso p in permisos)  // ? Todos son "Permiso"
    {
        // Trato uniforme: no importa si es simple o compuesto
        string nombre = p.getPermisoNombre();

        if (p is Familia familia)  // ? Composite
        {
            // Habilitar menú con submenu
            AgregarMenuConOpciones(nombre, familia.RetornarListaHijos());
        }
        else  // ? Hoja
        {
            // Habilitar opción simple
            HabilitarOpcion(nombre);
        }
    }
}
```

**Resultado:**
```
Usuario Login: "jperez" (Rol = "Vendedor")
  ?
Obtener permisos de "Vendedor"
  ?
Estructura recursiva devuelta:
  Vendedor (Composite)
  ?? Ver (Hoja)
  ?? Crear (Hoja)
  ?? GestionAvanzada (Composite anidado)
     ?? Editar (Hoja)
     ?? Eliminar (Hoja)
  ?
UI se construye automáticamente
```

---

## ?? TABLA COMPARATIVA: Con vs Sin Composite

### ? SIN Patrón Composite

```csharp
// Código frágil y repetitivo
public void MostrarPermisos(List<Permiso> permisos)
{
    foreach (var p in permisos)
    {
        // Problema: Necesita saber el tipo específico
        if (p.GetType() == typeof(PermisoSimple))
        {
            // Código para simple
            HabilitarBoton(p.Nombre);
        }
        else if (p.GetType() == typeof(Familia))
        {
            // Código para composite
            var familia = (Familia)p;
            CrearMenu(familia.Nombre);

            foreach (var hijo in familia.RetornarListaHijos())
            {
                // Recursión manual
                // Código duplicado...
            }
        }
    }
}

// Problema: Mucho casting, código duplicado, difícil de mantener
```

### ? CON Patrón Composite

```csharp
// Código limpio y elegante
public void MostrarPermisos(List<Permiso> permisos, TreeNode parent)
{
    foreach (var p in permisos)
    {
        // ¡Código UNIFORME!
        var hijo = new TreeNode(p.getPermisoNombre());

        // Recursión automática
        var hijos = p.RetornarListaHijos();
        if (hijos.Count > 0)
        {
            MostrarPermisos(hijos, hijo);  // ? La misma función, recursión natural
        }

        parent.Nodes.Add(hijo);
    }
}

// Ventajas: Sin casting, sin duplicación, fácil mantener, escalable
```

---

## ?? MATRIZ DE PATRÓN COMPOSITE

| Elemento | Nuestro Sistema |
|----------|-----------------|
| **Component (Base)** | `abstract Permiso` |
| **Leaf (Hoja)** | `PermisoSimple` |
| **Composite** | `Familia` |
| **Operation** | `RetornarListaHijos()` |
| **Children Management** | `AgregarHijo()`, `EliminarHijo()` |
| **Client** | `frmGestionPerfiles` |
| **Recursion** | `LoadTreeRecursive()` |
| **Polymorphism** | Sí, completo |

---

## ?? FLUJO COMPOSITE EN ACCIÓN

```
Usuario abre frmGestionPerfiles
  ?
CargarArbol()
  ?
PerfilBLL.ListaPermisosEnArbol()
  ?
PerfilDAL construye estructura (Composite)
  ?
Retorna List<Permiso> con estructura jerárquica
  ?
foreach Permiso (sin distinción)
  ?? Si es Familia: LoadTreeRecursive()
  ?? Si es PermisoSimple: Retorna lista vacía (fin recursión)
  ?
TreeView muestra estructura completa
  ?? Nivel 1: Familias
  ?? Nivel 2: Subfamilias o Simples
  ?? Nivel N: Hojas (PermisoSimple)
```

---

## ? CONCLUSIÓN: COMPOSITE PRESENTE EN CADA LÍNEA

| Característica | Evidencia |
|---------------|-----------|
| **Base común** | `abstract Permiso` |
| **Hoja** | `PermisoSimple` |
| **Composite** | `Familia` |
| **Uniforme** | Ambos implementan `RetornarListaHijos()` |
| **Recursión** | `LoadTreeRecursive()` |
| **Composición** | `AgregarHijo()`, `EliminarHijo()` |
| **Árbol** | Estructura jerárquica completa |
| **Polimorfismo** | Sin casting necesario |

**El patrón Composite está perfectamente implementado en cada interacción del sistema.**

---

**Ejemplos Prácticos Completados:** ?  
**Patrón Composite Confirmado:** ?????
