using CodeLab.ToolManager.Pkg.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeLab.ToolManager.Pkg.Core.Services.Impl
{
    internal class AliasService(Settings settings) : IAliasService
    {
        public Result SetAlias(string alias)
        {
            string aliasFolderPath = Path.Combine(settings.InstallPath, "aliases");
            string aliasFilePath = Path.Combine(aliasFolderPath, alias + ".cmd");

            if (File.Exists(aliasFilePath))
                return new Result(false, $"Alias already exists for this tool.");

            Directory.CreateDirectory(aliasFolderPath);

            try
            {
                string path = Environment.GetEnvironmentVariable("PATH", settings.InstallMode) ?? "";
                var toolPath = path.Split(';', StringSplitOptions.RemoveEmptyEntries)
                    .FirstOrDefault(p => p.Contains($"{settings.InstallPath}\\versions", StringComparison.OrdinalIgnoreCase));
                string toolExePath = Path.Combine(toolPath ?? "", "terraform.exe");
                string content = $"@echo off\r\n\"{toolExePath}\" %*";

                File.WriteAllText(aliasFilePath, content);

                string newPath = $"{aliasFolderPath}" + ";" + path;
                Environment.SetEnvironmentVariable("PATH", newPath, settings.InstallMode);
            }
            catch (Exception ex)
            {
                return new Result(false, $"Failed to create alias file at {aliasFilePath}: {ex.Message}.");
            }

            return new Result(true, $"Successfully created alias {alias}!");
        }

        public Result UnsetAlias(string alias)
        {
            string aliasFilePath = $"{settings.InstallPath}/aliases/{alias}.cmd";

            if (!Directory.Exists(aliasFilePath))
                return new Result(false, $"Alias not found.");

            try
            {
                string path = Environment.GetEnvironmentVariable("PATH", settings.InstallMode) ?? "";

                var pathFiltered = path.Split(';', StringSplitOptions.RemoveEmptyEntries)
                    .Where(p => !p.Contains($"{aliasFilePath}", StringComparison.OrdinalIgnoreCase));

                string newPathFiltered = string.Join(";", pathFiltered);

                Environment.SetEnvironmentVariable("PATH", newPathFiltered, settings.InstallMode);
            }
            catch (Exception ex)
            {
                return new Result(false, $"Failed to remove environment path for {aliasFilePath}: {ex.Message}.");
            }

            try
            {
                File.Delete(aliasFilePath);
            }
            catch (Exception ex)
            {
                return new Result(false, $"Failed to remove file at {aliasFilePath}: {ex.Message}.");
            }

            return new Result(true, $"Successfully created alias {alias}!");
        }
    }
}
