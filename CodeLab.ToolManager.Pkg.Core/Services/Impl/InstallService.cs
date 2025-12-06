using CodeLab.ToolManager.Pkg.Core.Models;
using CodeLab.ToolManager.Pkg.Core.Services;
using System.IO.Compression;

namespace CodeLab.ToolManager.Pkg.Core.Services.Impl
{
    internal class InstallService(Settings settings) : IInstallService
    {
        public async Task<Result> InstallAsync(string version)
        {
            var installPath = $"{settings.InstallPath}/{version}";

            if (Directory.Exists(installPath))
                return new Result(false, $"Version {version} is already installed at {installPath}.");

            Directory.CreateDirectory(installPath);

            var url = $"{settings.InstallBaseUrl.TrimEnd("/")}/{version}/terraform_{version}_windows_amd64.zip";
            var zipPath = Path.Combine(installPath, $"terraform-{version}.zip");

            using HttpClient client = new();
            byte[] bytes;

            try
            {
                bytes = await client.GetByteArrayAsync(url);
            }
            catch (HttpRequestException ex)
            {
                return new Result(false, $"Failed to download Terraform {version}: {ex.Message}.");
            }

            try
            {
                await File.WriteAllBytesAsync(zipPath, bytes);
            }
            catch (IOException ex)
            {
                return new Result(false, $"Failed to write file {zipPath}: {ex.Message}.");
            }

            try
            {
                ZipFile.ExtractToDirectory(zipPath, installPath, overwriteFiles: true);
                File.Delete(zipPath);
            }
            catch (Exception ex)
            {
                return new Result(false, $"Failed to extract ZIP: {ex.Message}.");
            }

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

            return new Result(true, $"Successfully installed version {version}!");
        }
    }
}
