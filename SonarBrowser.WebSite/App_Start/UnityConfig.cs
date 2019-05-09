using AutoMapper;
using ServiceStack.Caching;
using SonarBrowser.ActiveDirectory.Service;
using SonarBrowser.ActiveDirectory.Service.Interface;
using SonarBrowser.ActiveDirectory.Service.Model;
using SonarBrowser.Infrastructure.Cache;
using SonarBrowser.Infrastructure.ContextProvider;
using SonarBrowser.Infrastructure.Logging;
using SonarBrowser.Infrastructure.WebClient;
using SonarBrowser.Sonar.Service;
using SonarBrowser.Sonar.Services.Model.Request;
using SonarBrowser.SonarBrowserOrchestrator.Services;
using SonarBrowser.Tfs.Service;
using SonarBrowser.Tfs.Service.Interface;
using SonarBrowser.WebSite.Factories;
using SonarBrowser.WebSite.Factories.Interfaces;
using System;
using System.Configuration;
using Unity;
using Unity.Injection;

namespace SonarBrowser.WebSite
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            container.RegisterType<IIssuesSonarViewModelFactory, IssuesSonarViewModelFactory>();
            container.RegisterType<ISonarBrowserOrchestrator, SonarBrowserOrchestrator.Services.SonarBrowserOrchestrator>();

            container.RegisterType<ISonarConnector, SonarConnector>();
            container.RegisterType<SonarConnector>(new InjectionConstructor(GetSonarSetting(), typeof(CacheManager), typeof(WebApiClient), typeof(Log4NetLoggingService)));

            container.RegisterType<ICacheClient, CacheClient>();

            container.RegisterType<ICacheManager, CacheManager>();
            container.RegisterType<CacheManager>( new InjectionProperty("CacheDuration", TimeSpan.FromHours(Convert.ToInt32(ConfigurationManager.AppSettings["CacheDuration"]))));

            container.RegisterType<IActiveDirectoryService, ActiveDirectoryService>(new InjectionConstructor(GetAdSetting(),typeof(CacheManager)));
            container.RegisterType<IHttpApiClient, WebApiClient>();

            container.RegisterType<ITfsConnector, TfsConnector>();
            container.RegisterType<TfsConnector>(new InjectionConstructor(GetTfsSetting(), typeof(CacheManager), typeof(WebApiClient)));

            container.RegisterType<ILoggingService, Log4NetLoggingService>();
            container.RegisterType<IContextService, HttpContextService>();

            var config = new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<Sonar.Service.Model.Issues, SonarBrowserOrchestrator.Services.DTO.IssueDetail>();
            });
            IMapper mapper = config.CreateMapper();
            container.RegisterInstance<IMapper>(mapper);
        }


        private static SonarSettings GetSonarSetting()
        {
            SonarSettings sonarSetting = new SonarSettings();
            sonarSetting.Token = ConfigurationManager.AppSettings["TokenSonar"];
            sonarSetting.Uri_GetIssues = ConfigurationManager.AppSettings["UriSonarApiSearchIssue"];
            sonarSetting.Uri_GetChangeSet = ConfigurationManager.AppSettings["UriSonarApiSourceLine"];
            sonarSetting.NbIssuesPerRequestDefault = Int32.Parse(ConfigurationManager.AppSettings["NbIssuesPerRequest"]);
            return sonarSetting;
        }

        private static AdSettings GetAdSetting()
        {
            AdSettings adSettings = new AdSettings();
            adSettings.Login = ConfigurationManager.AppSettings["ActiveDirectoryLogin"];
            adSettings.Pass = ConfigurationManager.AppSettings["ActiveDirectoryPass"];
            adSettings.DomainName = ConfigurationManager.AppSettings["ActiveDirectoryDomain"];
            return adSettings;
        }

        private static TfsSettings GetTfsSetting()
        {
            TfsSettings tfsSettings = new TfsSettings();
            tfsSettings.Token = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", "", ConfigurationManager.AppSettings["personalAccessTokenTfs"])));
            tfsSettings.Url = ConfigurationManager.AppSettings["tfsUrl"];
            return tfsSettings;
        }
    }
}