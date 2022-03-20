﻿using Microsoft.EntityFrameworkCore;
using ForumBackEnd.Data;
using ForumBackEnd.Repositories;
using ForumBackEnd.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ForumBackEndContext>(options =>

    options.UseSqlServer(builder.Configuration.GetConnectionString("ForumBackEndContext")).UseLazyLoadingProxies());
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IModuleRepository, ModuleRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionsRepository>();
builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();
builder.Services.AddScoped<AnswerServices>();
builder.Services.AddScoped<QuestionServices>();
builder.Services.AddScoped<ModuleServices>();
builder.Services.AddScoped<CourseServices>();
builder.Services.AddScoped<RoleServices>();
builder.Services.AddScoped<UserServices>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
