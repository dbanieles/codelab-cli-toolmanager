
using CodeLab.ToolManager.Cli.Terraform.Commands;
using CodeLab.ToolManager.Pkg.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.CommandLine;

Console.OutputEncoding = System.Text.Encoding.UTF8;

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var services = new ServiceCollection();

services.AddToolManager(new()
{
    InstallBaseUrl = configuration["Terraform:RepositoryBaseUrl"],
    InstallPath = configuration["Terraform:InstallPath"],
    InstallName = configuration["Terraform:InstallName"],
    InstallMode = configuration["Terraform:InstallMode"] switch
    {
        "User" => EnvironmentVariableTarget.User,
        "System" => EnvironmentVariableTarget.Machine,
        _ => throw new ArgumentException($"Invalid InstallMode: {configuration["Terraform:InstallMode"]}")
    }
});

services.AddScoped<Command, InstallCommand>();
services.AddScoped<Command, UseCommand>();
services.AddScoped<Command, ListCommand>();
services.AddScoped<Command, UninstallCommand>();
services.AddScoped<Command, AliasSetCommand>();
services.AddScoped<Command, AliasUnsetCommand>();

var serviceProvider = services.BuildServiceProvider();

var root = new RootCommand("Terraform Tool Manager (tftool)");
var commands = serviceProvider.GetServices<Command>();

foreach (var cmd in commands)
    root.Add(cmd);

var commandExec = root.Parse(args);
commandExec.Invoke();
