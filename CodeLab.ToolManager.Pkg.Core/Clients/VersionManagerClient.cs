using CodeLab.ToolManager.Pkg.Core.Models;
using CodeLab.ToolManager.Pkg.Core.Services;
using System;

namespace CodeLab.ToolManager.Pkg.Core.Clients
{
    public class VersionManagerClient(
        IInstallService installService,
        IListService listService,
        IUseService useService,
        IUninstallService uninstallService,
        IAliasService aliasService)
    {
        public async Task<Result> InstallAsync(string version) => await installService.InstallAsync(version);
        public Result<string[]> List() => listService.List();
        public Result Use(string version) => useService.Use(version);
        public Result Uninstall(string version) => uninstallService.Uninstall(version);
        public Result SetAlias(string aliasName) => aliasService.SetAlias(aliasName);
        public Result UnsetAlias(string aliasName) => aliasService.UnsetAlias(aliasName);
    }
}
