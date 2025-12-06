using CodeLab.ToolManager.Pkg.Core.Clients;
using CodeLab.ToolManager.Pkg.Core.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace CodeLab.ToolManager.Cli.Terraform.Commands
{
    internal class ListCommand : Command
    {
        public ListCommand(Settings settings, VersionManagerClient vManagerClient)
            : base("list", "Show all installed versions")
        {
            this.SetAction(async (parseResult, cancellationToken) =>
            {
                await AnsiConsole.Status().StartAsync($"[yellow]Getting istalled versions...[/]", async ctx => {
                    var versions = vManagerClient.List();
                    
                    var table = new Table();
                    table.Border(TableBorder.Heavy);
                    table.AddColumn("[bold]Version[/]");
                    table.AddColumn("[bold]Path[/]");

                    foreach (string version in versions.Value)
                        table.AddRow(version, $"{settings.InstallPath}/{version}");

                    AnsiConsole.Write(table);
                });
            });
        }
    }
}
