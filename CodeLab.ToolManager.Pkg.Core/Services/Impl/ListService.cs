using CodeLab.ToolManager.Pkg.Core.Models;
using System;
using System.Net.NetworkInformation;

namespace CodeLab.ToolManager.Pkg.Core.Services.Impl
{
    internal class ListService(Settings settings) : IListService
    {
        public Result<string[]> List()
        {
            if(!Directory.Exists(settings.InstallPath))
                return new Result<string[]>(Array.Empty<string>(), true,"No version found.");

            var path = Environment.GetEnvironmentVariable(settings.InstallName, settings.InstallMode) ?? "";

            var versions = Directory.GetDirectories(settings.InstallPath)
                .Select(dir => 
                {
                    var version = Path.GetFileName(dir);
                    if (path.Equals($"{settings.InstallPath}\\{version}"))
                        return $"[green]{version}[/]";
                    return version;
                })
                .ToArray();

            return new Result<string[]>(versions, true, "");

        }
    }
}
