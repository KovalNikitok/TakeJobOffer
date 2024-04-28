using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using TakeJobOffer.Application.Services;
using TakeJobOffer.DAL;
using TakeJobOffer.DAL.Repositories;
using TakeJobOffer.DAL.Migrations;
using TakeJobOffer.Domain.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));
builder.Services.AddHealthChecks();
builder.Services.AddControllers();
builder.Services.AddCors(options =>
    options.AddDefaultPolicy(opt =>
    {
        if(builder.Environment.IsDevelopment())
            opt.WithOrigins("http://localhost:3000", "http://0.0.0.0:5432", "http://0.0.0.0:3000", "null");
        else
            opt.WithOrigins("www.takejoboffer.ru", "http://localhost:3000", "http://0.0.0.0:3000", "https://0.0.0.0:3000");
        opt.WithMethods("GET", "POST", "PUT", "DELETE");
        opt.AllowAnyHeader();
        opt.AllowCredentials();
    }));

builder.Services.AddEndpointsApiExplorer();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TakeJobOfferDbContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(TakeJobOfferDbContext)));
    });


builder.Services.AddScoped<IProfessionsRepository, ProfessionsRepository>();
builder.Services.AddScoped<ISkillsRepository, SkillsRepository>();
builder.Services.AddScoped<IProfessionsSkillsRepository, ProfessionsSkillsRepository>();

builder.Services.AddScoped<IProfessionsService, ProfessionsService>();
builder.Services.AddScoped<ISkillsService, SkillsService>();
builder.Services.AddScoped<IProfessionsSkillsService, ProfessionsSkillsService>();


var app = builder.Build();

app.MigrateDatabase<TakeJobOfferDbContext, Program>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/health");

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();
