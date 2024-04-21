using FetchToDos.Services;
using FetchToDos.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FetchToDos
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Thread.Sleep(10000); //Wait 10 seconds until the API is up and running

            string workingDirectory = Environment.CurrentDirectory;

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(workingDirectory)!.Parent!.Parent!.FullName)
                .AddJsonFile("config.json", optional: false);

            IConfiguration config = builder.Build();

            //setup our DI
            var services = new ServiceCollection();

            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            services.AddLogging(loggingBuilder => loggingBuilder
                .AddConsole()
                .AddDebug()
                .SetMinimumLevel(LogLevel.Debug));
            services.AddHttpClient<ITodoService, TodoService>(c => c.BaseAddress = new Uri(config.GetValue<string>("BaseUrl")!));
            services.AddSingleton<IFetchService, FetchService>();
            services.AddSingleton(config);

            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

            var serviceProvider = services.BuildServiceProvider();

            var logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<Program>();

            //Working stuff here
            var fetchService = serviceProvider.GetService<IFetchService>();

            logger.LogInformation($"FetchToDos service started it's work at {DateTime.UtcNow}");

            await fetchService!.FetchTodos();

            logger.LogInformation($"FetchToDos service finished it's work at {DateTime.UtcNow}");
        }
    }
}
