using Microsoft.EntityFrameworkCore;
using Maternidad.Data;

var builder = WebApplication.CreateBuilder(args);

// ── Base de datos: Render (DATABASE_URL) o local (appsettings.json) ──
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
string connectionString;

if (databaseUrl != null)
{
    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':');
    var port = uri.Port > 0 ? uri.Port : 5432; // si no hay puerto usa 5432 por defecto
    connectionString = $"Host={uri.Host};Port={port};" +
                       $"Database={uri.AbsolutePath.TrimStart('/')};" +
                       $"Username={userInfo[0]};Password={userInfo[1]};" +
                       $"SSL Mode=Require;Trust Server Certificate=true";
}
else
{
    connectionString = builder.Configuration.GetConnectionString("MaternidadContext")
        ?? throw new InvalidOperationException("Connection string 'MaternidadContext' not found.");
}

builder.Services.AddDbContext<MaternidadContext>(options =>
    options.UseNpgsql(connectionString));

// ── Servicios ──
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ── CORS ──
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// ── Migraciones automáticas al iniciar ──
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MaternidadContext>();
    db.Database.Migrate();
}

// ── Swagger siempre visible ──
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Maternidad API v1");
    c.RoutePrefix = string.Empty;
});

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.Run();