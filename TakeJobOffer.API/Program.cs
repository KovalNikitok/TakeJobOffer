using Microsoft.EntityFrameworkCore;
using TakeJobOffer.Application.Services;
using TakeJobOffer.DAL;
using TakeJobOffer.DAL.Repositories;
using TakeJobOffer.DAL.Migrations;
using TakeJobOffer.Domain.Abstractions.Repositories;
using TakeJobOffer.Domain.Abstractions.Services;
using TakeJobOffer.Domain.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(opt =>
    {
        if(builder.Environment.IsDevelopment())
            opt.WithOrigins("nginx", "http://localhost:3000", "http://localhost:443", "null");
        else
            opt.WithOrigins("nginx", "www.takejoboffer.ru", "frontend:3000");
        opt.AllowAnyMethod();
        opt.AllowAnyHeader();
        opt.AllowCredentials();
    }));

builder.Services.AddRouting();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TakeJobOfferDbContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(TakeJobOfferDbContext)));
    });

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("TakeJobOfferRedis");
    options.InstanceName = "backend";
});

builder.Services.AddScoped<IProfessionsRepository, ProfessionsRepository>();
builder.Services.AddScoped<ISkillsRepository, SkillsRepository>();
builder.Services.AddScoped<IProfessionsSkillsRepository, ProfessionsSkillsRepository>();
builder.Services.AddScoped<IProfessionsSlugRepository, ProfessionsSlugRepository>();

builder.Services.AddScoped<IProfessionsService, ProfessionsService>();
builder.Services.AddScoped<ISkillsService, SkillsService>();
builder.Services.AddScoped<IProfessionsSkillsService, ProfessionsSkillsService>();
builder.Services.AddScoped<IProfessionsSlugService, ProfessionsSlugService>();


var app = builder.Build();

app.MigrateDatabase<TakeJobOfferDbContext, Program>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/health");

app.UseExceptionMiddleware();

app.UseRouting();

app.UseCors();

app.MapControllers();

app.Run();
