# T06b — Control de Cambios sobre la entidad Usuario (Patrón Memento)

## 1. Requerimiento Funcional No Trivial (RFN)

**RFN6.2 — Control de Cambios sobre 'Usuario' (Memento) — Prioridad: Alta**

El sistema registra el historial detallado de modificaciones sobre la entidad **Usuario**: además de asentar en la bitácora *que* se modificó un usuario, guarda **el valor anterior y el posterior de cada campo modificado**, junto con quién realizó el cambio y cuándo. Esto permite responder "¿quién?", "¿cuándo?" y "¿qué cambió exactamente?", y reconstruir el estado anterior del objeto.

Se implementa con el **patrón Memento**: antes de persistir una modificación, la entidad (originador) fotografía su estado en un memento inmutable; después del cambio se toma una segunda foto y un gestor de auditoría (cuidador) compara ambas, generando **una fila de historial por cada campo modificado** en la tabla `HistorialCambios`.

**Justificación de la entidad elegida:** `Usuario` es la entidad más sensible del sistema: concentra credenciales, rol (permisos efectivos), estado de actividad y bloqueo. Es la misma entidad protegida con dígitos verificadores (T07), por lo que auditar su evolución campo por campo completa el esquema de seguridad. La contraseña se **enmascara** en el historial: se audita *que* cambió, nunca su valor.

## 2. Descripción Funcional (DF) por capa

| Capa | Componente | Responsabilidad |
|---|---|---|
| BE (Entidades) | `MementoBE` | **Memento**: foto inmutable del estado de la entidad como pares campo/valor, con fecha de captura. |
| BE (Entidades) | `IOriginadorCambios` | Interfaz del **originador**: `CrearMemento()` y `RestaurarMemento(memento)`. |
| BE (Entidades) | `UsuarioBE` | Implementa `IOriginadorCambios`: fotografía sus campos de negocio (excluye `cod`, identidad, y `dvh`, campo técnico derivado) y puede reconstruirse desde un memento. |
| BE (Entidades) | `CambioBE` | Registro de auditoría: campo, valor anterior, valor nuevo, usuario responsable, fecha, entidad afectada. |
| Servicios (aspecto técnico) | `GestorDeAuditoria` | **Cuidador/comparador**: compara dos mementos y genera un `CambioBE` por campo modificado, enmascarando los campos sensibles (`pass`). Evita repetir la lógica de comparación en cada formulario (cohesión y reuso). |
| BLL (Negocio) | `HistorialCambiosBLL` | Orquesta el registro (memento previo + entidad modificada → filas de historial) y la consulta del historial de un usuario. |
| BLL (Negocio) | `UsuarioBLL` | En cada operación que persiste un UPDATE sobre Usuario (modificación, baja lógica, bloqueo, desbloqueo, cambio de clave) toma el memento previo y delega el registro en `HistorialCambiosBLL`. |
| DAL (Acceso a Datos) | `MP_HistorialCambios` | Persistencia vía `SP_RegistrarCambio` y `SP_ExtHistorialCambios`. |
| GUI (Presentación) | `frmHistorialCambios` | Pantalla de consulta del historial de un usuario puntual, con detalle del cambio seleccionado. Implementa `IObservadorIdioma` (T05). Se abre con el botón **Ver Historial** de `frmUsuario`. |

## 3. Modelo de datos (DER)

Tabla `HistorialCambios` (script `Migracion_T06b_HistorialCambios.sql`):

| Columna | Tipo | Descripción |
|---|---|---|
| Id | int IDENTITY, PK | Identificador del registro de cambio. |
| EntidadId | int, FK → `Usuarios(Codigo)` | Usuario controlado (relación de auditoría hacia la entidad elegida). |
| NombreCampo | nvarchar(50) | Campo modificado. |
| ValorAnterior | nvarchar(300) | Valor previo (enmascarado si es sensible). |
| ValorNuevo | nvarchar(300) | Valor posterior (enmascarado si es sensible). |
| Usuario | nvarchar(50) | Quién realizó el cambio. |
| Fecha | datetime | Cuándo. |

Cada UPDATE sobre `Usuarios` genera una o varias filas (una por campo modificado), lo que permite reconstruir el "antes" campo por campo.

## 4. Escenario de Caso de Uso (ECU)

**CU: Consultar el historial de cambios de un usuario**

- **Actor principal:** Administrador (perfil con permiso `GestionUsuarios`).
- **Precondiciones:** sesión iniciada; existe al menos un usuario con modificaciones registradas.
- **Postcondiciones:** la consulta queda asentada en la bitácora (`ConsultaHistorial`).

**Flujo principal:**
1. El administrador modifica el rol de un usuario de "Cajero" a "Vendedor" y su teléfono desde *Gestión de Usuarios*.
2. `UsuarioBLL.ActualizarUsuario` toma el memento del usuario persistido, aplica el cambio, y `GestorDeAuditoria` compara ambas fotos: genera dos filas en `HistorialCambios` (una por `rol`, otra por `tel`) con valor anterior, valor nuevo, responsable y fecha.
3. Más tarde, el administrador selecciona ese usuario en la grilla y presiona **Ver Historial**.
4. El sistema abre `frmHistorialCambios` con los cambios del usuario ordenados cronológicamente (más reciente primero).
5. Al seleccionar una fila, el panel de detalle muestra la frase completa: quién, cuándo, qué campo y de qué valor a qué valor.

**Flujos alternativos:**
- FA1 — El usuario no registra cambios: la pantalla lo informa y la grilla queda vacía.
- FA2 — Error de comunicación con la BD: se informa el error y la pantalla no se abre con datos parciales.
- FA3 — Cambio de contraseña: el historial registra que el campo `pass` cambió, mostrando `****` como valor anterior y nuevo.

## 5. Relación con la bitácora (T06)

La bitácora general sigue registrando el evento grueso (`ModificacionUsuario`, `BloqueoUsuario`, etc.). El control de cambios es el nivel adicional de auditoría: el detalle campo por campo. Ambos registros se generan en la misma operación, en capas distintas (GUI registra el evento de bitácora; BLL registra el historial de cambios).
