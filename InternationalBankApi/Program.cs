using Autofac;
using Autofac.Extensions.DependencyInjection;
using BusinessLayer.DependencyResolvers.Autofac;
using BusinessLayer.Dto.ConfigurationModel;
using BusinessLayer.Mappers;
using CoreLayer.DataAccess.Abstract;
using CoreLayer.DataAccess.Concrete.Repository;
using CoreLayer.Utilities.Cache.Abstract;
using CoreLayer.Utilities.Cache.Concreate;
using CoreLayer.Utilities.Ioc;
using CoreLayer.Utilities.Messages;
using CoreLayer.Utilities.Result.Abstract;
using CoreLayer.Utilities.Result.Concreate;
using EntityLayer.Models.EntityFremework;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var host = builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new AutofacBusinessModule());
});

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(options =>
{
    // Validate child properties and root collection elements
    options.ImplicitlyValidateChildProperties = true;
    options.ImplicitlyValidateRootCollectionElements = true;

    // Automatic registration of validators in assembly
    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
});
builder.Services.AddDbContext<MyBankContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString(StaticValue.ConnectionKey));
}, ServiceLifetime.Scoped);

builder.Services.AddCors();

builder.Services.AddAutoMapper(typeof(CustomMapping));

builder.Services.AddMemoryCache();

builder.Services.Configure<AppKey>(builder.Configuration.GetSection("AppKey"));

AppKey appKey = new AppKey();

builder.Configuration.GetSection("AppKey").Bind(appKey);

byte[] key = Encoding.UTF8.GetBytes(appKey.TokenKey);

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
            ValidateAudience = false,
        };

    });

RebbitmqSetting rebbitmq = new RebbitmqSetting();
builder.Configuration.GetSection("RabbitmqSettings").Bind(rebbitmq);
builder.Services.Configure<RebbitmqSetting>(builder.Configuration.GetSection("RabbitmqSettings"));

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rebbitmq.Host, "/", host =>
        {
            host.Username(rebbitmq.Username);
            host.Password(rebbitmq.Password);
        });

    });
});
builder.Services.AddMassTransitHostedService();

builder.Services.Configure<RedisSetting>(builder.Configuration.GetSection("RedisSettings"));

builder.Services.AddSingleton<ICacheWorkFlow, RedisCacheWorkFlow>(sp =>
{
    var options = sp.GetRequiredService<IOptions<RedisSetting>>().Value;
    var redis = new RedisCacheWorkFlow(options.Host, options.Port, options.Db, $"{builder.Environment.ApplicationName}_{builder.Environment.EnvironmentName}");
    return redis;
});
//builder.Services.AddSingleton<ICacheWorkFlow, MemoryCacheWorkFlow>();

builder.Services.AddSingleton(typeof(IResult<>), typeof(Result<>));

builder.Services.AddSingleton<CoreLayer.Utilities.Result.Abstract.IResult, Result>();

builder.Services.Configure<DatabaseSttings>(builder.Configuration.GetSection("DatabaseSettings"));

builder.Services.AddSingleton<IDatabaseSttings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSttings>>().Value;
});

builder.Services.AddScoped<LogMessage>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ServiceTool.Create(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.Use(async (context, next) =>
{
    var message = ServiceTool.ServiceProvider.GetService<LogMessage>();
    message.EmptyLog();

    await next();
});

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
