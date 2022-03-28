using Microsoft.EntityFrameworkCore;
using ForumBackEnd.Data;

using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ForumBackEnd.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Configuration;
using ForumBackEnd.Services.UserRepository;
using ForumBackEnd.Services.PasswordRepository;
using ForumBackEnd.Services;
using ForumBackEnd.Services.CourseRepository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
// Se llama a la BBDD y se utiliza lazyloding
builder.Services.AddDbContext<ForumBackEndContext>(options =>

    options.UseSqlServer(builder.Configuration.GetConnectionString("ForumBackEndContext")).UseLazyLoadingProxies());
// Se añade configuracion cors

// Add services to the container.
builder.Services.AddControllers();
// Se indica al controlador que ignore los referencias cíclicas
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Se añaden scoped de repos
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordRepository, PasswordRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
// Scoped para crear los jwt
builder.Services.AddScoped <JwtServices>();

// Se utiliza authenticacion (JWT)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options => 
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "yourdomain.com",
        ValidAudience = "yourdomain.com",
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Super_Secret_Ob_Key:Token").Value)
            )
    }
    );

builder.Services.AddMvc();
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
app.UseCors( c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.UseStatusCodePages();
app.MapControllers();

app.Run();
