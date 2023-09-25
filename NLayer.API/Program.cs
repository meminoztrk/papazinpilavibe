using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnitOfWorks;
using NLayer.Service.Mapping;
using NLayer.Service.Services;
using System.Reflection;
using FluentValidation.AspNetCore;
using NLayer.Service.Validations;
using NLayer.API.Filters;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Middlewares;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using NLayer.API.Modules;
using Microsoft.AspNetCore.Authentication.Certificate;
using Newtonsoft.Json;
using NLayer.Service.Helper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<UserRegisterDtoValidator>())
                             .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme).AddCertificate();
builder.Services.AddCors();

builder.Services.AddScoped(typeof(NotFoundFilter<>));
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<CustomImageProcessing>();

builder.Services.AddAutoMapper(typeof(MapProfile));


builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepoServiceModule()));

var app = builder.Build();

app.UseCors(options => options
                .WithOrigins(new[] { "http://localhost:3000", "http://localhost:5168", "http://localhost:4200", "http://2.56.154.39:1040", "http://2.56.154.39" }) // react, view, angular                                                                                                                                                                  //.WithOrigins(new[] { "http://2.56.154.39:1040" }) // react, view, angular
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
            );

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();    
app.UseStaticFiles();

app.UseCustomException();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
