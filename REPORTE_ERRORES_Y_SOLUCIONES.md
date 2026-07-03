# ?? REPORTE DE ERRORES Y SOLUCIONES

## ?? Resumen de Errores Encontrados

```
Total de errores encontrados: 8 errores de referencia de assembly
Estado: Los archivos fuente están correctos
Causa: Error de compilación en cascada (cadena de dependencias)
```

---

## ?? Problemas Identificados

### Problema 1: Falta compilar proyectos en orden correcto

```
Los proyectos tienen estas dependencias:
  TP_SanchezVillaverde 
    ? (depende de)
  Negocio_BLL, Servicios
    ? (depende de)
  Acceso_DAL
    ? (depende de)
  Entidad_BE

ERROR: Se intenta compilar todo simultáneamente
SOLUCIÓN: Compilar en orden: Entidad_BE ? Acceso_DAL ? Negocio_BLL ? Servicios ? TP_SanchezVillaverde
```

### Problema 2: Referencias de Assembly faltantes

Los siguientes DLL no se encuentran:
- `Entidad_BE.dll`
- `Acceso_DAL.dll`
- `Negocio_BLL.dll`
- `Servicios.dll`

**Causa:** Los archivos fuente están bien, pero las DLL compiladas no existen en los directorios `bin\Debug\`

---

## ? SOLUCIONES RECOMENDADAS

### Solución 1: Limpiar y Reconstruir (RECOMENDADO)

**En Visual Studio:**
1. Menú: `Generar` ? `Limpiar solución`
2. Esperar a que termine
3. Menú: `Generar` ? `Reconstruir solución`

**Desde PowerShell:**
```powershell
# Eliminar directorios bin y obj
Get-ChildItem -Path "." -Directory -Name "bin" -Recurse | ForEach-Object { 
    Remove-Item -Path $_ -Recurse -Force -ErrorAction SilentlyContinue 
}

Get-ChildItem -Path "." -Directory -Name "obj" -Recurse | ForEach-Object { 
    Remove-Item -Path $_ -Recurse -Force -ErrorAction SilentlyContinue 
}

# Luego reconstruir en Visual Studio
```

### Solución 2: Compilar Proyectos en Orden

Si limpiar no funciona, compila manualmente en este orden:

1. **Entidad_BE** (sin dependencias)
   - Click derecho ? Compilar

2. **Acceso_DAL** (depende de Entidad_BE)
   - Click derecho ? Compilar

3. **Servicios** (depende de Entidad_BE)
   - Click derecho ? Compilar

4. **Negocio_BLL** (depende de Acceso_DAL, Entidad_BE, Servicios)
   - Click derecho ? Compilar

5. **TP_SanchezVillaverde** (depende de todos)
   - Click derecho ? Compilar

---

## ?? Estado de Archivos Fuente

### ? Archivos Verificados (SIN ERRORES)

```
? Entidad_BE/Permiso.cs        - Correcto
? Entidad_BE/PermisoSimple.cs  - Correcto
? Entidad_BE/Familia.cs        - Correcto
? Entidad_BE/TipoPermiso.cs    - Correcto

? Acceso_DAL/PerfilDAL.cs      - Correcto
? Acceso_DAL/AccesoDatos.cs    - Correcto

? Negocio_BLL/PerfilBLL.cs     - Correcto (usando es Entidad_BE)
```

**Conclusión:** Los archivos fuente están correctamente implementados. El problema es de compilación, no de código.

---

## ?? Detalles de los Errores

### Error CS0006: Metadatos no encontrados

```
CS0006: No se encontró el archivo de metadatos 'C:\...\Entidad_BE\bin\Debug\Entidad_BE.dll'
```

**Significado:** El compilador intenta cargar un DLL que no ha sido compilado aún.

**Causa:** Compilación en orden incorrecto o compilación fallida previa.

**Solución:**
1. Limpiar solución (eliminar bin/obj)
2. Reconstruir solución completa
3. Si persiste: Compilar proyecto por proyecto en orden

---

## ?? Paso a Paso: SOLUCIÓN DEFINITIVA

### Opción A: Visual Studio GUI (Más Fácil)

```
1. Abre Visual Studio
2. Menú ? Generar ? Limpiar solución
   ?? Espera a que finalice (verás "Compilación completada" en la ventana de salida)

3. Menú ? Generar ? Reconstruir solución
   ?? Espera a que finalice

4. Si ves errores, intenta compilar proyecto por proyecto:
   ?? Haz click derecho en Entidad_BE ? Compilar
   ?? Luego click derecho en Acceso_DAL ? Compilar
   ?? Luego click derecho en Servicios ? Compilar
   ?? Luego click derecho en Negocio_BLL ? Compilar
   ?? Finalmente click derecho en TP_SanchezVillaverde ? Compilar
```

### Opción B: Línea de Comando

```powershell
# 1. Navega al directorio del proyecto
cd "C:\Users\aguss\OneDrive - UNIVERSIDAD ABIERTA INTERAMERICANA\2026\TRABAJO DE CAMPO\Nueva carpeta"

# 2. Limpia directorios
Remove-Item -Path "*\bin" -Recurse -Force -ErrorAction SilentlyContinue
Remove-Item -Path "*\obj" -Recurse -Force -ErrorAction SilentlyContinue

# 3. Compila con msbuild
msbuild "." -t:Rebuild

# O si tienes dotnet instalado:
dotnet clean
dotnet build
```

---

## ?? Si Persisten los Errores

### Checklist de Diagnóstico

```
[ ] 1. ¿Eliminaste carpetas bin y obj completamente?
      ? Verifica que no existan: Entidad_BE\bin, Entidad_BE\obj, etc.

[ ] 2. ¿Cierras y reabriste Visual Studio después de limpiar?
      ? A veces VS cachea información antigua

[ ] 3. ¿Todos los proyectos tienen TargetFramework = v4.7.2?
      ? Revisa cada .csproj

[ ] 4. ¿Las referencias entre proyectos están correctas?
      ? Verifica Negocio_BLL.csproj tiene referencia a Entidad_BE
      ? Verifica Acceso_DAL.csproj tiene referencia a Entidad_BE

[ ] 5. ¿Hay errores sintácticos en los archivos .cs?
      ? Revisa Negocio_BLL/PerfilBLL.cs usando statements

[ ] 6. ¿El archivo de solución .sln está actualizado?
      ? Intenta cerrarlo y reabrirlo
```

### Comandos para Diagnóstico

```powershell
# Ver si existen archivos DLL
Get-ChildItem -Path "." -Filter "*.dll" -Recurse

# Ver estructura de directorios bin
Get-ChildItem -Path ".\*\bin" -Recurse -Force

# Buscar archivos .csproj
Get-ChildItem -Path "." -Filter "*.csproj" -Recurse | Format-List FullName
```

---

## ?? Información Adicional

### Dependencias del Proyecto

```
Entidad_BE (SIN dependencias - COMPILAR PRIMERO)
  ?? Negocio_BLL (depende de Entidad_BE, Acceso_DAL, Servicios)
  ?? Acceso_DAL (depende de Entidad_BE, Servicios)
  ?? Servicios (depende de Entidad_BE)

TP_SanchezVillaverde (depende de Negocio_BLL, Servicios, Entidad_BE)
  ?? COMPILAR ÚLTIMO
```

### Verificación Manual

```csharp
// En Negocio_BLL/PerfilBLL.cs, los using deben ser:
using Acceso_DAL;          // ? Correcto
using Entidad_BE;          // ? Correcto
using System;
using System.Collections.Generic;
using System.Linq;
// etc.

// En Acceso_DAL/PerfilDAL.cs:
using Entidad_BE;          // ? Correcto
using System;
using System.Collections.Generic;
// etc.
```

---

## ? RESUMEN DE ACCIONES

| Acción | Prioridad | Estado |
|--------|-----------|--------|
| Limpiar solución (Generar ? Limpiar) | ?? CRÍTICA | Hacer ahora |
| Reconstruir solución (Generar ? Reconstruir) | ?? CRÍTICA | Hacer ahora |
| Cerrar y reabre Visual Studio | ?? IMPORTANTE | Si no funciona |
| Compilar por proyectos en orden | ?? IMPORTANTE | Si no funciona |
| Revisar referencias de proyectos | ?? MENOR | Verificar después |

---

## ?? Si Necesitas Más Ayuda

**Proporciona:**
1. Screenshot de los errores en Visual Studio
2. Output de la compilación completo
3. Resultado de limpiar y reconstruir

**Entonces podré:**
- Identificar errores específicos de código
- Sugerir correcciones particulares
- Verificar configuraciones específicas

---

**Reporte Completado:** ?  
**Archivos Fuente:** ? Correctos  
**Causa del Error:** ?? Compilación en cascada  
**Solución:** ?? Limpiar y Reconstruir solución
