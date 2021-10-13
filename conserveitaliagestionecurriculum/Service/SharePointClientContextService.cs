using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace conserveitaliagestionecurriculum.Service
{
    public static class SharePointClientContextFactoryServiceConfiguration
    {
        public static IServiceCollection AddSharePointContextFactory(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ISharePointContextFactory, SharePointContextFactory>();
            return serviceCollection;
        }

        public static IServiceCollection AddSharePointClientContext(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSharePointContextFactory();

            serviceCollection.AddScoped<ClientContext>((services) =>
            {
                var contextFactory = services.GetService<ISharePointContextFactory>();
                return contextFactory.GetContext();
            });

            return serviceCollection;
        }
    }

    public interface ISharePointContextFactory
    {
        ClientContext GetContext(string siteUrl = null);
    }

    class SharePointContextFactory : ISharePointContextFactory
    {
        
        private readonly IConfiguration _configuration;

        public SharePointContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            
        }

        public ClientContext GetContext(string siteUrl = null)
        {
            string user = null;
            string password = null;
            string clientAppId = null;
            SecureString secpassword = null;

            siteUrl ??= _configuration.GetValue<string>("SharePoint:Url");
            if (string.IsNullOrEmpty(siteUrl))
                throw new Exception("The SharePoint site URL is not specified or configured");

            user ??= _configuration.GetValue<string>("SharePoint:User");
            if (string.IsNullOrEmpty(siteUrl))
                throw new Exception("The SharePoint User is not specified or configured");

            password ??= _configuration.GetValue<string>("SharePoint:Password");
            if (string.IsNullOrEmpty(siteUrl))
                throw new Exception("The SharePoint Password is not specified or configured");
            else
            {
                secpassword = new SecureString();
                foreach (char c in password) secpassword.AppendChar(c);
            };

            clientAppId ??= _configuration.GetValue<string>("SharePoint:ClientAppId");
            if (string.IsNullOrEmpty(siteUrl))
                throw new Exception("The Azure App ID is not specified or configured");



            AuthenticationManager auth = new AuthenticationManager();
              return auth.GetContext(new Uri(siteUrl), user, secpassword, clientAppId);
          
        }
    }
}
