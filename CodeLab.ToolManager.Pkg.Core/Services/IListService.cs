using CodeLab.ToolManager.Pkg.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLab.ToolManager.Pkg.Core.Services
{
    public interface IListService
    {
        Result<string[]> List();
    }
}
