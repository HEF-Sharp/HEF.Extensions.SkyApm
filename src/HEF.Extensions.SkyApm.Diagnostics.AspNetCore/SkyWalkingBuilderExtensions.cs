using Microsoft.Extensions.DependencyInjection;
using SkyApm.Diagnostics.AspNetCore.Handlers;
using SkyApm.Utilities.DependencyInjection;

namespace SkyApm.Diagnostics.AspNetCore
{
    public static class SkyWalkingBuilderExtensions
    {
        public static SkyApmExtensions AddAspNetCorePathIgnore(this SkyApmExtensions extensions)
        {
            extensions.Services.AddSingleton<IHostingDiagnosticHandler, PathIgnoreHostingDiagnosticHandler>();
            return extensions;
        }
    }
}
