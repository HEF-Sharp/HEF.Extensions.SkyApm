using Microsoft.Extensions.DependencyInjection;
using SkyApm.Utilities.DependencyInjection;
using System;

namespace SkyApm.Diagnostics.MySqlClient
{
    public static class SkyWalkingBuilderExtensions
    {
        public static SkyApmExtensions AddMySqlClient(this SkyApmExtensions extensions)
        {
            if (extensions == null)
            {
                throw new ArgumentNullException(nameof(extensions));
            }

            extensions.Services.AddSingleton<ITracingDiagnosticProcessor, MySqlClientTracingDiagnosticProcessor>();

            return extensions;
        }
    }
}
