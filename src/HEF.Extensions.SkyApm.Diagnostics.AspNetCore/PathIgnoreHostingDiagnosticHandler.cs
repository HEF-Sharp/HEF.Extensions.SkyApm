using Microsoft.AspNetCore.Http;
using SkyApm.Config;
using SkyApm.Tracing;
using SkyApm.Tracing.Segments;
using System.Linq;

namespace SkyApm.Diagnostics.AspNetCore.Handlers
{
    public class PathIgnoreHostingDiagnosticHandler : IHostingDiagnosticHandler
    {
        private readonly AspNetCoreConfig _config;

        public PathIgnoreHostingDiagnosticHandler(IConfigAccessor configAccessor)
        {
            _config = configAccessor.Get<AspNetCoreConfig>();
        }

        public bool OnlyMatch(HttpContext httpContext)
        {
            return _config.IgnorePaths.Any(path => httpContext.Request.Path.StartsWithSegments(path));
        }

        public void BeginRequest(ITracingContext tracingContext, HttpContext httpContext)
        {

        }

        public void EndRequest(SegmentContext segmentContext, HttpContext httpContext)
        {

        }
    }
}
