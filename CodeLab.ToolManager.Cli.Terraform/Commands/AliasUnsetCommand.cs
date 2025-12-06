using CodeLab.ToolManager.Pkg.Core.Clients;
using CodeLab.ToolManager.Pkg.Core.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace CodeLab.ToolManager.Cli.Terraform.Commands
{
    internal class AliasUnsetCommand : Command
    {
        private readonly Argument<string> _aliasName = new("aliasName");

        public AliasUnsetCommand(Settings settings, VersionManagerClient vManagerClient)
            : base("unset-alias", "Unset tool alias.")
        {
            this.Add(_aliasName);
            this.SetAction(async (parseResult, cancellationToken) =>
            {
                var aliasName = parseResult.GetValue<string>("aliasName");
                await AnsiConsole.Status().StartAsync($"[yellow]Alias {aliasName}...[/]", async ctx => {
                    var result = vManagerClient.UnsetAlias(aliasName);
                    var message = result.Success ? $"[green]✔ {result.Message}[/]" : $"[red]✗ {result.Message}[/]";
                    AnsiConsole.MarkupLine(message);
                });
            });
        }
    }
}
