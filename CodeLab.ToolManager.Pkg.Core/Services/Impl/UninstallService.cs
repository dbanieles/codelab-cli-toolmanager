using CodeLab.ToolManager.Pkg.Core.Models;
using CodeLab.ToolManager.Pkg.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLab.ToolManager.Pkg.Core.Services.Impl
{
    internal class UninstallService(Settings settings) : IUninstallService
    {
        public Result Uninstall(string version)
        {
            var installPath = $"{settings.InstallPath}/{version}";

            if (!Directory.Exists(installPath))
                return new Result(false, $"Version {version} not found.");

            try
            {
                Directory.Delete(installPath, true);
            }
            catch (Exception ex)
            {
                return new Result(false, $"Failed to remove folder at {installPath}: {ex.Message}.");
            }

            try
            {
                Environment.SetEnvironmentVariable(settings.InstallName, null, settings.InstallMode);
                string path = Environment.GetEnvironmentVariable("PATH", settings.InstallMode) ?? "";
                string variableReference = $"%{settings.InstallName}%";
                var pathParts = path.Split(';', StringSplitOptions.RemoveEmptyEntries)
                        .Where(p => !p.Equals(variableReference, StringComparison.OrdinalIgnoreCase));
                string newPath = string.Join(";", pathParts);

                Environment.SetEnvironmentVariable("PATH", newPath, settings.InstallMode);
            }
            catch (Exception ex)
            {
                return new Result(false, $"Failed to update environment variables: {ex.Message}.");
            }

            return new Result(true, $"Successfully unistalled vesion {version}!");
        }
    }
}
