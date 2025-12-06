using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLab.ToolManager.Pkg.Core.Models
{
    public record Result<T>(T Value, bool Success, string Message);
    public record Result(bool Success, string Message);
}
