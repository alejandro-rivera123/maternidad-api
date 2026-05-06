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
    var port = uri.Port > 0 ? uri.Port : 5432;
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
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    }); builder.Services.AddEndpointsApiExplorer();
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
    c.RoutePrefix = "swagger"; // ← CAMBIADO: antes era string.Empty
});

// ── Archivos estáticos (wwwroot/index.html) ──
app.UseDefaultFiles();   // index.html como página por defecto en "/"
app.UseStaticFiles();    // sirve todo lo que está en wwwroot/

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.Run();