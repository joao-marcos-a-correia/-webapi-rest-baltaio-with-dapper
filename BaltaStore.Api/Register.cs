using BaltaStore.Domain.StoreContext.Handlers;
using BaltaStore.Domain.StoreContext.Repositories;
using BaltaStore.Domain.StoreContext.Services;
using BaltaStore.Infra.StoreContext.DataContexts;
using BaltaStore.Infra.StoreContext.Repositories;
using BaltaStore.Infra.StoreContext.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaltaStore.Api
{
    public class Register
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            //DbContext
            services.AddScoped<BaltaDataContext, BaltaDataContext>();

            //Handlers
            services.AddTransient<CustomerHandler, CustomerHandler>();

            //Interfaces
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            
            //Services
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
