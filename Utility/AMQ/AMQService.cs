using GreenPipes;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Utility.AMQ
{
    //public class EndPointNameFormatter : IEndpointNameFormatter
    //{
    //    public string TemporaryEndpoint(string tag)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    string IEndpointNameFormatter.CompensateActivity<T, TLog>()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    string IEndpointNameFormatter.Consumer<T>()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    string IEndpointNameFormatter.ExecuteActivity<T, TArguments>()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    string IEndpointNameFormatter.Saga<T>()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
    public class AMQService
    {
        public static void AddService(
            IServiceCollection services,
            IConfiguration configuration,
            Assembly assembly                
            )
        {
            var amq = configuration.GetSection("AMQ");
            var host = amq.GetSection("Host").Value;
            var username = amq.GetSection("Username").Value;
            var password = amq.GetSection("Password").Value;

            services.AddHealthChecks();
            services.AddMassTransit(x => {
                x.AddConsumers(assembly);                
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host(new Uri(host), host =>
                    {
                        host.Username(username);
                        host.Password(password);
                    });
                    cfg.ConfigureEndpoints(provider);                    
                }));
            });
       
            services.AddSingleton<IHostedService, BusService>();
        }
    }

}

