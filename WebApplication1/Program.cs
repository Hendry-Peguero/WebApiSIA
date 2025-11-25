using WebApiSIA.Infrastructure.Persistence.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Tu capa de persistencia (donde registras ApplicationContext y el repo)
builder.Services.AddPersistenceDependency(builder.Configuration);

var app = builder.Build();

// Swagger SIEMPRE activo mientras pruebas
app.UseSwagger();
app.UseSwaggerUI();

// SIN redirección a HTTPS por ahora
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
