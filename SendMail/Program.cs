using BusinessLayer.Dto.ConfigurationModel;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SendMail.Consumers;

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory).AddJsonFile("appsettings.json", true, true).AddJsonFile($"appsettings.{environment}.json", true, true).Build();
var hostBuilder = Host.CreateDefaultBuilder().ConfigureServices(service =>
{
    service.Configure<RebbitmqSetting>(configuration.GetSection("RabbitmqSettings"));
}).Build();

var rebbitmqSetting = hostBuilder.Services.GetRequiredService<IOptions<RebbitmqSetting>>().Value;

var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
{
    cfg.Host(rebbitmqSetting.Host, "/", host =>
    {
        host.Username(rebbitmqSetting.Username);
        host.Password(rebbitmqSetting.Password);
    });
    cfg.ReceiveEndpoint(rebbitmqSetting.MailQueueName, e =>
    {
        e.Consumer<MailConsumer>();
    });
});
busControl.Start();
try
{
    Console.WriteLine("Press enter to exit");
    var timeSpan = DateTime.Now.AddDays(1) - DateTime.Now;
    await Task.Delay(timeSpan);
}
finally
{
    busControl.Stop();
}
