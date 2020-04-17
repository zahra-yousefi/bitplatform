﻿using Bit.Hangfire.Contracts;
using Bit.Owin.Contracts;
using Hangfire;
using Hangfire.Dashboard;
using Owin;
#if DotNetCore
using Bit.OwinCore.Contracts;
using Microsoft.AspNetCore.Builder;
#endif

namespace Bit.Hangfire
{
    public class JobSchedulerMiddlewareConfiguration :
#if DotNet
        IOwinMiddlewareConfiguration
#else
        IAspNetCoreMiddlewareConfiguration
#endif
    {
        public virtual DashboardOptionsFactory DashboardOptionsFactory { get; set; } = default!;
        public virtual IJobSchedulerBackendConfiguration JobSchedulerBackendConfiguration { get; set; } = default!;

#if DotNetCore
        public MiddlewarePosition MiddlewarePosition => MiddlewarePosition.BeforeOwinMiddlewares;
        public virtual void Configure(IApplicationBuilder app)
#else
        public virtual void Configure(IAppBuilder app)
        
#endif
        {
            JobSchedulerBackendConfiguration.Init();

            app.UseHangfireDashboard("/jobs", DashboardOptionsFactory());
        }
    }
}
