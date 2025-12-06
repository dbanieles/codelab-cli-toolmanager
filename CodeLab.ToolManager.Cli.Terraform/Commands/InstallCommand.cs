using CodeLab.ToolManager.Pkg.Core.Clients;
using CodeLab.ToolManager.Pkg.Core.Models;
using Spectre.Console;
using System;
using System.CommandLine;

namespace CodeLab.ToolManager.Cli.Terraform.Commands
{
    internal class InstallCommand : Command
    {
        private readonly Argument<string> _version = new("version");

        public InstallCommand(Settings settings, VersionManagerClient vManagerClient)
            : base("install", "Install a specific version.")
        {
            this.Add(_version);
            this.SetAction(async (parseResult, cancellationToken) =>
            {
                var version = parseResult.GetValue<string>("version");
                await AnsiConsole.Status().StartAsync($"[yellow]Installing version {version}...[/]", async ctx => {
                    var result = await vManagerClient.InstallAsync(version);
                    var message = result.Success ? $"[green]✔ {result.Message}[/]" : $"[red]✗ {result.Message}[/]";
                    AnsiConsole.MarkupLine(message);

                    var table = new Table();
                    table.Border(TableBorder.Heavy);
                    table.AddColumn("[bold]Source[/]");
                    table.AddColumn("[bold]Version[/]");
                    table.AddColumn("[bold]Path[/]");
                    table.AddColumn("[bold]Status[/]");
                    table.AddRow(settings.InstallBaseUrl, version, $"{settings.InstallPath}/{version}", "[green]✔[/]");

                    AnsiConsole.Write(table);
                });
            });
        }
    }
}
