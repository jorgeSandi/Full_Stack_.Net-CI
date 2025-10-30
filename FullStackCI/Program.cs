// --- Archivo: Program.cs ---
//
// Configuración de la aplicación.
//
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using FullStackCI.Data;
using FullStackCI.Models;
using FullStackCI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuración de la base de datos en memoria y precarga de datos
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase("LibraryDb"));
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

// Register repositories
//builder.Services.AddScoped<IBookRepository, BookRepository>();
//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
//builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IUnitOfWorkService, UnitOfWorkService>();

// Register services
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();

//INYECTAR LA DEPENDENCIA
builder.Services.AddHttpClient(); //SERVICIO PROPIO DE NET CORE

//NUESTRO SERVICIO SIN INTERFACE POR FALTA DE TIEMPO JAJA
builder.Services.AddScoped<IHaciendaApiClientService, HaciendaApiClientService>();

// Agrega servicios a la tubería (pipeline) de contenedores
builder.Services.AddControllers();

// Configura el versionado de la API (solo por URL)
builder.Services.AddApiVersioning(
    options =>
    {
        options.ReportApiVersions = true;
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("api-version"),
                new QueryStringApiVersionReader("api-version"));
    })
.AddApiExplorer(
    options =>
    {
        options.GroupNameFormat = "'v'VVV"; //v1, v2
        options.SubstituteApiVersionInUrl = true;
    });

// Configura Swagger para la documentación
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    if (File.Exists(xmlPath))
        c.IncludeXmlComments(xmlPath);
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"] ?? string.Empty))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        // Obtenemos el proveedor de descripciones de versiones de la API.
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        // Creamos un endpoint en la UI de Swagger para cada versión descubierta.
        // Esto es lo que llena el menú desplegable en la esquina superior derecha.
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });

    // Cargar datos de prueba solo en desarrollo
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // Asegurar que la base de datos se crea
    context.Database.EnsureCreated();

    // Los datos ya se cargan automáticamente a través del SeedData en OnModelCreating
    // Pero podemos verificar si hay datos y agregar más si es necesario
    if (!context.Books.Any())
    {
        // Si por alguna razón no hay datos, los agregamos manualmente
        SeedTestData(context);
    }
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

// Método para asegurar la carga de datos de prueba
static void SeedTestData(ApplicationDbContext context)
{
    try
    {
        // Categorías
        var categories = new List<Category>
            {
                new() { Id = 1, Name = "Ficción", Description = "Libros de ficción y novelas" },
                new() { Id = 2, Name = "Ciencia", Description = "Libros científicos y técnicos" },
                new() { Id = 3, Name = "Historia", Description = "Libros históricos" },
                new() { Id = 4, Name = "Fantasy", Description = "Libros de fantasía" }
            };

        // Autores
        var authors = new List<Author>
            {
                new() { Id = 1, Name = "Gabriel García Márquez", Nationality = "Colombiano", BirthYear = 2007},
                new() { Id = 2, Name = "J.K. Rowling", Nationality = "Británica", BirthYear = 1965 },
                new() { Id = 3, Name = "Stephen King", Nationality = "Estadounidense", BirthYear = 2000 },
                new() { Id = 4, Name = "Isaac Asimov", Nationality = "Ruso", BirthYear =1987 },
                new() { Id = 5, Name = "Yuval Noah Harari", Nationality = "Israelí", BirthYear = 2005 }
            };

        // Libros
        var books = new List<Book>
            {
                new() {
                    Id = 1,
                    Title = "Cien años de soledad",
                    ISBN = "978-0307474728",
                    PublicationYear = 2010,
                    Pages = 417,
                    Description = "Una obra maestra de la literatura latinoamericana",
                    CategoryId = 1,
                    AuthorId = 1
                },
                new() {
                    Id = 2,
                    Title = "Harry Potter y la piedra filosofal",
                    ISBN = "978-8478884452",
                    PublicationYear = 2020,
                    Pages = 320,
                    Description = "El primer libro de la serie Harry Potter",
                    CategoryId = 4,
                    AuthorId = 2
                },
                new() {
                    Id = 3,
                    Title = "It",
                    ISBN = "978-1501142970",
                    PublicationYear = 1986,
                    Pages = 1138,
                    Description = "Una novela de terror",
                    CategoryId = 1,
                    AuthorId = 3
                },
                new() {
                    Id = 4,
                    Title = "Fundación",
                    ISBN = "978-0553293357",
                    PublicationYear = 2025,
                    Pages = 255,
                    Description = "Primer libro de la serie Fundación",
                    CategoryId = 2,
                    AuthorId = 4
                },
                new() {
                    Id = 5,
                    Title = "Sapiens: De animales a dioses",
                    ISBN = "978-8499926223",
                    PublicationYear = 2011,
                    Pages = 496,
                    Description = "Breve historia de la humanidad",
                    CategoryId = 3,
                    AuthorId = 5
                }
            };

        context.Categories.AddRange(categories);
context.Authors.AddRange(authors);
context.Books.AddRange(books);

context.SaveChanges();

Console.WriteLine("Datos de prueba cargados exitosamente.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al cargar datos de prueba: {ex.Message}");
    }
}