using CodeLab.ToolManager.Pkg.Core.Clients;
using CodeLab.ToolManager.Pkg.Core.Models;
using CodeLab.ToolManager.Pkg.Core.Services;
using CodeLab.ToolManager.Pkg.Core.Services.Impl;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CodeLab.ToolManager.Pkg.Core
{
    public static class ToolManager
    {
        public static IServiceCollection AddToolManager(this IServiceCollection services, Settings settings)
        {
            services.AddSingleton(settings);
            services.AddScoped<IInstallService, InstallService>();
            services.AddScoped<IListService, ListService>();
            services.AddScoped<IUseService, UseService>();
            services.AddScoped<IUninstallService, UninstallService>();
            services.AddScoped<VersionManagerClient>();
            return services;
        }
    }
}
