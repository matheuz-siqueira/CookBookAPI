using cookbook_api.Data;
using cookbook_api.Repositories;
using cookbook_api.Services;
using Microsoft.EntityFrameworkCore;

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using cookbook_api.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<RecipeRepository>();
builder.Services.AddScoped<RecipeService>();

builder.Services.Register();

builder.Services.AddDbContext<Context>(
  options =>
  //Dizendo que vamos usar o MySQL
  options.UseMySql(
      //Pegando as configurações de acesso ao BD
      builder.Configuration.GetConnectionString("Conection"),
      //Detectando o Servidor de BD
      ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Conection"))
  )
);

//Settings for Authentication with JWT
var JWTKey = Encoding.ASCII.GetBytes(builder.Configuration["JWTKey"]);
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(JWTKey),
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
