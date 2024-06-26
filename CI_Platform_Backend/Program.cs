using CI_Platform.Models.DBContext;
using CI_Platform.Repository.Implementation;
using CI_Platform.Repository.Interface;
using CI_Platform.Services.Implementation;
using CI_Platform.Services.Interface;
using CI_Platform_Backend;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddControllers();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<ILandingPageRepo, LandingPageRepo>();
builder.Services.AddScoped<IAuthRepo, AuthRepo>();
builder.Services.AddScoped<ILandingPageService, LandingPageService>();
builder.Services.AddScoped<IJWTService, JWTService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "You api title", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header,

            },
            new List<string>()
          }
        });
});
builder.Services.AddDbContext<AppDbContext>(option => option.UseNpgsql("name=ConnectionStrings:DefaultConnection"));
builder.Services.AddAuthentication(cfg =>
{
    cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = false;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8
            .GetBytes(configuration["ApplicationSettings:JWT_Secret"]!)
        ),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseCors(option =>
{
    option.AllowAnyHeader();
    option.AllowAnyMethod();
    option.AllowAnyOrigin();
});
app.UseAuthorization();

app.MapControllers();

app.Run();
