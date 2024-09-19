# Proyecto de Gestión de Torneos de Tenis

## Descripción

Este proyecto es una aplicación para la gestión de torneos de tenis. Permite a los usuarios crear, buscar y gestionar torneos y jugadores. La aplicación está construida utilizando C# y se integra con Supabase para la gestión de la base de datos.

## Características

- **Gestión de Jugadores**: Permite obtener una lista de jugadores con filtros opcionales.
- **Gestión de Torneos**: Permite crear torneos con diferentes tipos y nombres únicos.
- **Historial de Torneos**: Permite buscar bajo distintos filtros en el historial de torneos.

## Requisitos

- .NET 8.0 
- Supabase cuenta y API Key

## Paquetes NuGet Utilizados

Este proyecto utiliza varios paquetes NuGet para facilitar el desarrollo y la integración con servicios externos. A continuación se describen los paquetes más importantes:

### Supabase

- **Paquete**: `Supabase`
- **Descripción**: Este paquete proporciona una integración con Supabase, una plataforma de backend como servicio que ofrece una base de datos PostgreSQL, autenticación, almacenamiento y funciones en tiempo real.
- **Uso**: Se utiliza para interactuar con la base de datos Supabase, realizar operaciones CRUD y gestionar la autenticación de usuarios.
- **Documentación**: [Supabase .NET SDK](https://github.com/supabase-community/supabase-csharp)

### Supabase.Postgrest

- **Paquete**: `Supabase.Postgrest`
- **Descripción**: Este paquete es una extensión del SDK de Supabase que facilita las consultas y operaciones en la base de datos PostgreSQL.
- **Uso**: Se utiliza para construir y ejecutar consultas SQL de manera fluida y tipada.
- **Documentación**: [Supabase Postgrest .NET SDK](https://github.com/supabase-community/supabase-csharp)

### Otros Paquetes

- **System.Linq.Expressions**
  - **Descripción**: Proporciona clases y métodos para trabajar con expresiones lambda y árboles de expresión.
  - **Uso**: Se utiliza para construir consultas dinámicas y filtrar datos en la base de datos.

## Instalación

1. Clona el repositorio:
    git clone https://github.com/tu_usuario/tu_repositorio.git

2. Navega al directorio del proyecto:
    cd tu_repositorio

3. Restaura los paquetes NuGet:
    dotnet restore

4. Configura las variables de entorno para Supabase:
export SUPABASE_URL=tu_supabase_url
export SUPABASE_API_KEY=tu_supabase_api_key
    


## Uso

1. Inicializa el cliente de Supabase:
    var supabaseService = new SupabaseService(SUPABASE_URL, SUPABASE_API_KEY, new SupabaseOptions());
    await supabaseService.InitializeAsync();

2. Obtén la lista de jugadores:
    var players = await supabaseService.GetPlayersAsync();

3. Crea un nuevo torneo:
    var newTournament = await supabaseService.CreateTournamentAsync(PlayerType.Man, "Torneo de Primavera", 5);


## Arquitectura y Patrones de Diseño

### Arquitectura

La arquitectura del proyecto sigue el patrón de **Capas**. Las principales capas son:

- **Capa de Servicios**: Contiene la lógica de negocio y las interacciones con la base de datos a través de Supabase.
- **Capa de Entidades**: Define las entidades del dominio como `Player`, `Tournament`, y `PlayerHistory`.
- **Capa de Solicitudes**: Define las clases de solicitud como `TournamentSearchRequest`.

### Patrones de Diseño

- **Inyección de Dependencias**: Utilizado para inyectar el cliente de Supabase en la clase `SupabaseService`.
- **Repositorio**: La clase `SupabaseService` actúa como un repositorio que maneja las operaciones CRUD para las entidades.
- **Factory Method**: El método `GenerateUniqueTournamentName` se utiliza para crear nombres únicos para los torneos.
- **Strategy**: Utilizado en la capa de Core para encapsular las diferentes estrategias de creación de torneos basados en el tipo de jugador (`PlayerType`). Las estrategias están definidas en la interfaz `ITournamentStrategy` y sus implementaciones concretas `ManTournamentStrategy` y `WomanTournamentStrategy`.



