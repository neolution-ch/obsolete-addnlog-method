using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ObsoleteAddNLog
{
    using Autofac;
    using Autofac.Integration.Mvc;
    using Microsoft.Extensions.Logging;
    using NLog.Extensions.Logging;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            DependencyResolver.SetResolver(new AutofacDependencyResolver(CreateAutofacContainer()));

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        private static IContainer CreateAutofacContainer()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Add support for Microsoft Core Logging
            builder.RegisterType<LoggerFactory>().As<ILoggerFactory>().SingleInstance();
            builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>)).SingleInstance();

            // Build the container
            var container = builder.Build();

            ConfigureContainer(container);

            return container;
        }

        private static void ConfigureContainer(IComponentContext context)
        {
            // Add NLog to Microsoft Core Logging
            var loggerFactory = context.Resolve<ILoggerFactory>();
            loggerFactory.AddNLog();
        }
    }
}
