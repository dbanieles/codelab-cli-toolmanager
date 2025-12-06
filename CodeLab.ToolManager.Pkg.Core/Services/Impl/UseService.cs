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
            var installPath = $"{settings.InstallPath}/versions/{version}";

            if (!Directory.Exists(installPath))
                return new Result(false, $"Version {version} not found.");

            try
            {
                string path = Environment.GetEnvironmentVariable("PATH", settings.InstallMode) ?? "";

                var pathFiltered = path.Split(';', StringSplitOptions.RemoveEmptyEntries)
                    .Where(p => !p.Contains(settings.InstallPath, StringComparison.OrdinalIgnoreCase));

                string newPathFiltered = string.Join(";", pathFiltered);
                string newPath = $"{installPath}" + ";" + newPathFiltered;
                Environment.SetEnvironmentVariable("PATH", newPath, settings.InstallMode);
            }
            catch (Exception ex)
            {
                return new Result(false, $"Failed to update environment variables: {ex.Message}.");
            }

            return new Result(true, $"Successfully using version {version}!Restart your terminal to apply changes.");
        }
    }
}
