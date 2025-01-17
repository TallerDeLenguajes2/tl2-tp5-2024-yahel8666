var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// agregado para el login: 
// builder.Services.AddScoped<InMemoryUserRepository>(); 


// // Habilitar servicios de sesiones para el login 
// builder.Services.AddSession(options =>
// {
//     options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de expiración de la sesión
//     options.Cookie.HttpOnly = true; // Solo accesible desde HTTP, no JavaScript
//     options.Cookie.IsEssential = true; // Necesario incluso si el usuario no acepta cookies
// });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// https://articulo.mercadolibre.com.ar/MLA-1450857491-notebook-asus-vivobook-i7-13va-16gb-ssd-512gb-16pulg-16kg-_JM#origin=touchable_link&item_id=MLA1450845137 

