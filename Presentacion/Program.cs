using Microsoft.EntityFrameworkCore;
using Datos;
using Presentacion.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("https://localhost:44423",
                                "http://localhost:4200",
                                "https://aeropuerto-prueba.000webhostapp.com",
                                "http://aeropuerto-prueba.000webhostapp.com")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

// Configurar cadena de Conexion con EF
var connectionString = builder.Configuration.GetConnectionString("ConnectionString");
builder.Services.AddDbContext<AplicacionDBContext>(x => x.UseSqlServer(connectionString));


// Add services to the container.

builder.Services.AddControllersWithViews();

#region configure strongly typed settings objects
    var appSettingsSection = builder.Configuration.GetSection("AppSetting");
    builder.Services.Configure<AppSetting>(appSettingsSection);
#endregion

#region Configure jwt authentication inteprete el token 
    var appSettings = appSettingsSection.Get<AppSetting>();
    var key = Encoding.ASCII.GetBytes(appSettings.Secret!);

    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
#endregion

builder.Services.AddSwaggerGen(c => {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "API Prueba Técnica",
            Description = @"ASP.NET Core Web API + Angular. 
                Entityframework.
                API Autenticada con JWT (Json Web Token). ",
            Contact = new OpenApiContact
            {
                Name = "Hernando Rumbo",
                Email = string.Empty,
                Url = new Uri("https://github.com/HJRumbo/PruebaTecnica"),
            }
        });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                    }
                },
                new string[]{}
            }
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

//start swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});
//end swagger

app.MapFallbackToFile("index.html"); ;

app.Run();
