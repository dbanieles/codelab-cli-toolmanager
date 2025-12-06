using CodeLab.ToolManager.Pkg.Core.Models;
using CodeLab.ToolManager.Pkg.Core.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CodeLab.ToolManager.Pkg.Core.Services.Impl
{
    internal class UninstallService(Settings settings) : IUninstallService
    {
        public Result Uninstall(string version)
        {
            var installPath = $"{settings.InstallPath}/versions/{version}";

            if (!Directory.Exists(installPath))
                return new Result(false, $"Version {version} not found.");

            try
            {
                string path = Environment.GetEnvironmentVariable("PATH", settings.InstallMode) ?? "";

                var pathParts = path.Split(';');

                foreach (var part in pathParts)
                {
                    if (part.Equals(installPath))
                    {
                        var pathFiltered = path.Split(';', StringSplitOptions.RemoveEmptyEntries)
                            .Where(p => !p.Equals(installPath, StringComparison.OrdinalIgnoreCase));

                        string newPath = string.Join(";", pathFiltered);
                        Environment.SetEnvironmentVariable("PATH", newPath, settings.InstallMode);
                        break;
                    }
                }

                Environment.SetEnvironmentVariable($"{settings.InstallName}_{version}", null, settings.InstallMode);
            }
            catch (Exception ex)
            {
                return new Result(false, $"Failed to update environment variables: {ex.Message}.");
            }

            try
            {
                Directory.Delete(installPath, true);
            }
            catch (Exception ex)
            {
                return new Result(false, $"Failed to remove folder at {installPath}: {ex.Message}.");
            }

            return new Result(true, $"Successfully unistalled vesion {version}!");
        }
    }
}
