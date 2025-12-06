using CodeLab.ToolManager.Pkg.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLab.ToolManager.Pkg.Core.Services.Impl
{
    internal class UseService(Settings settings) : IUseService
    {
        public Result Use(string version)
        {
            var installPath = $"{settings.InstallPath}/{version}";

            if (!Directory.Exists(installPath))
                return new Result(false, $"Version {version} not found.");

            try
            {
                Environment.SetEnvironmentVariable(settings.InstallName, installPath, settings.InstallMode);
                string path = Environment.GetEnvironmentVariable("PATH", settings.InstallMode) ?? "";
                Environment.SetEnvironmentVariable("PATH", $"%{settings.InstallName}%;" + path, settings.InstallMode);
            }
            catch (Exception ex)
            {
                return new Result(false, $"Failed to update environment variables: {ex.Message}.");
            }

            return new Result(true, $"Successfully using version {version}!Restart your terminal to apply changes.");
        }
    }
}
