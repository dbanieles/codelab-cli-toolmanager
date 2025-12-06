using CodeLab.ToolManager.Pkg.Core.Clients;
using CodeLab.ToolManager.Pkg.Core.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace CodeLab.ToolManager.Cli.Terraform.Commands
{
    internal class UninstallCommand : Command
    {
        private readonly Argument<string> _version = new("version");

        public UninstallCommand(ToolManagerClient toolManagerClient)
            : base("uninstall", "Uninstall a specific version")
        {
            this.Add(_version);
            this.SetAction(async (parseResult, cancellationToken) =>
            {
                var version = parseResult.GetValue<string>("version");
                await AnsiConsole.Status().StartAsync($"[yellow]Uninstalling version {version}...[/]", async ctx => {
                    var result = toolManagerClient.Uninstall(version);
                    var message = result.Success ? $"[green]✔ {result.Message}[/]" : $"[red]✗ {result.Message}[/]";
                    AnsiConsole.MarkupLine(message);
                });
            });
        }
    }
}
