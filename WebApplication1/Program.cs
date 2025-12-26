using System.Net;
using System.Net.Sockets;
using System.Text;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using WebApiSIA.Infrastructure.Persistence.DependencyInjection;
using WebApiSIA.Core.Application.DependencyInjection;

// ========================================
// 1. CARGAR VARIABLES DE ENTORNO (.env)
// ========================================
DotNetEnv.Env.Load();

// ========================================
// 2. CONFIGURAR SERILOG (Logging estructurado)
// ========================================
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/webapi-.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    Log.Information("🚀 Iniciando WebApiSIA Backend...");

    var builder = WebApplication.CreateBuilder(args);

    // Configurar Serilog como proveedor de logging
    builder.Host.UseSerilog();

    // ========================================
    // 3. CONFIGURAR PUERTO DINÁMICO CON FALLBACK
    // ========================================
    var requestedPort = int.Parse(Environment.GetEnvironmentVariable("PORT") ?? "5037");
    var host = Environment.GetEnvironmentVariable("HOST") ?? "localhost";
    var httpsPort = int.Parse(Environment.GetEnvironmentVariable("HTTPS_PORT") ?? "7242");
    
    int availablePort = GetAvailablePort(requestedPort);
    
    if (availablePort != requestedPort)
    {
        Log.Warning(
            "⚠️ Puerto {RequestedPort} no disponible. Usando puerto alternativo: {AvailablePort}",
            requestedPort,
            availablePort);
    }

    builder.WebHost.UseUrls($"http://{host}:{availablePort}", $"https://{host}:{httpsPort}");
    Log.Information("✅ API configurada para ejecutarse en: http://{Host}:{Port}", host, availablePort);

    // ========================================
    // 4. CONFIGURAR SERVICIOS
    // ========================================
    
    // Controllers
    builder.Services.AddControllers();

    // CORS
    var corsOrigins = Environment
        .GetEnvironmentVariable("CORS_ALLOWED_ORIGINS")
        ?.Split(",", StringSplitOptions.RemoveEmptyEntries);

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowFrontend", policy =>
        {
            policy.WithOrigins(corsOrigins ?? [])
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
    });

    // Swagger
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Persistencia y aplicación
    builder.Services.AddPersistenceDependency(builder.Configuration);
    builder.Services.AddApplicationDependency();

    // ========================================
    // 5. RATE LIMITING
    // ========================================
    builder.Services.AddMemoryCache();
    builder.Services.Configure<IpRateLimitOptions>(options =>
    {
        options.EnableEndpointRateLimiting = true;
        options.StackBlockedRequests = false;
        options.HttpStatusCode = 429;
        options.RealIpHeader = "X-Real-IP";
        options.GeneralRules = new List<RateLimitRule>
        {
            new RateLimitRule
            {
                Endpoint = "POST:/api/barcode/scan",
                Period = "1m",
                Limit = int.Parse(Environment.GetEnvironmentVariable("RATE_LIMIT_BARCODE_REQUESTS") ?? "100")
            }
        };
    });

    builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
    builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
    builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
    builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

    // ========================================
    // 6. JWT AUTHENTICATION (Seguridad reforzada)
    // ========================================
    var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET") 
                    ?? Environment.GetEnvironmentVariable("JWT_KEY") 
                    ?? throw new InvalidOperationException("JWT_SECRET o JWT_KEY debe estar configurado en .env");

    if (jwtSecret.Length < 32)
    {
        Log.Warning("⚠️ JWT_SECRET tiene menos de 32 caracteres. Considere usar una clave más larga en producción.");
    }

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.FromMinutes(5), // Tolerancia de 5 minutos para diferencias de reloj
                ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
                ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSecret))
            };

            // Logging de eventos de autenticación
            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    Log.Warning("❌ Autenticación JWT fallida: {Error}", context.Exception.Message);
                    return Task.CompletedTask;
                },
                OnTokenValidated = context =>
                {
                    Log.Debug("✅ Token JWT validado para usuario: {User}", context.Principal?.Identity?.Name);
                    return Task.CompletedTask;
                }
            };
        });

    builder.Services.AddAuthorization();

    // ========================================
    // 7. CONSTRUIR LA APLICACIÓN
    // ========================================
    var app = builder.Build();

    // Swagger (siempre disponible para testing)
    app.UseSwagger();
    app.UseSwaggerUI();

    // ========================================
    // 8. HTTPS REDIRECTION (Solo en producción)
    // ========================================
    if (app.Environment.IsProduction())
    {
        app.UseHttpsRedirection();
        Log.Information("🔒 HTTPS Redirection habilitado (Producción)");
    }
    else
    {
        Log.Information("🔓 HTTPS Redirection deshabilitado (Desarrollo)");
    }

    // Rate Limiting Middleware
    app.UseIpRateLimiting();

    // CORS
    app.UseCors("AllowFrontend");

    // Authentication & Authorization
    app.UseAuthentication();
    app.UseAuthorization();

    // Controllers
    app.MapControllers();

    // ========================================
    // 9. EJECUTAR LA APLICACIÓN
    // ========================================
    Log.Information("✅ API escuchando en puerto: {Port}", availablePort);
    Log.Information("📡 Swagger UI disponible en: http://{Host}:{Port}/swagger", host, availablePort);
    
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "💥 La aplicación falló al iniciar");
    throw;
}
finally
{
    Log.CloseAndFlush();
}

// ========================================
// FUNCIÓN: Obtener puerto disponible con fallback
// ========================================
static int GetAvailablePort(int preferredPort)
{
    try
    {
        // Intentar con el puerto preferido
        using var listener = new TcpListener(IPAddress.Loopback, preferredPort);
        listener.Start();
        listener.Stop();
        return preferredPort;
    }
    catch (SocketException)
    {
        // Puerto ocupado, buscar el siguiente disponible
        for (int port = preferredPort + 1; port < preferredPort + 100; port++)
        {
            try
            {
                using var listener = new TcpListener(IPAddress.Loopback, port);
                listener.Start();
                listener.Stop();
                return port;
            }
            catch (SocketException)
            {
                // Continuar buscando
            }
        }
        
        // Si no encuentra ninguno, usar puerto aleatorio del sistema
        using var randomListener = new TcpListener(IPAddress.Loopback, 0);
        randomListener.Start();
        int randomPort = ((IPEndPoint)randomListener.LocalEndpoint).Port;
        randomListener.Stop();
        return randomPort;
    }
}

