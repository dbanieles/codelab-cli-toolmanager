using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLab.ToolManager.Pkg.Core.Models
{
    public class Settings
    {
        public string InstallPath { get; init; }
        public string InstallBaseUrl { get; init; }
        public string InstallName { get; init; }
        public EnvironmentVariableTarget InstallMode { get; init; }

    }
}
