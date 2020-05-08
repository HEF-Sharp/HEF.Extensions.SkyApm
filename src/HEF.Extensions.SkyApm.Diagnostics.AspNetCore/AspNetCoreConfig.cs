using System;

namespace SkyApm.Config
{
    [Config("SkyWalking", "AspNetCore")]
    public class AspNetCoreConfig
    {
        public string[] IgnorePaths { get; set; } = Array.Empty<string>();
    }
}
