using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Practices.Unity.WebApi;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(AnApiOfIceAndFire.App_Start.UnityWebApiActivator), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(AnApiOfIceAndFire.App_Start.UnityWebApiActivator), "Shutdown")]

namespace AnApiOfIceAndFire.App_Start
{
    /// <summary>Provides the bootstrapping for integrating Unity with WebApi when it is hosted in ASP.NET</summary>
    public static class UnityWebApiActivator
    {
        /// <summary>Integrates Unity when the application starts.</summary>
        public static void Start() 
        {
            // Use UnityHierarchicalDependencyResolver if you want to use a new child container for each IHttpController resolution.
            // var resolver = new UnityHierarchicalDependencyResolver(UnityConfig.GetConfiguredContainer());
            var container = UnityConfig.GetConfiguredContainer();
            var resolver = new UnityDependencyResolver(container);

            //Set resolver for MVC
            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));

            //Set resolver for Web API
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        /// <summary>Disposes the Unity container when the application is shut down.</summary>
        public static void Shutdown()
        {
            var container = UnityConfig.GetConfiguredContainer();
            container.Dispose();
        }
    }
}
