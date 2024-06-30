using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Focus.Business.Components;
using Focus.Business.Interface;
using AutoMapper;
using System.Reflection;
using MediatR;
using Focus.Business.Common.Behaviours;

namespace Focus.Business
{
   public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<ICompanyComponent, CompanyComponent>();
            services.AddTransient<IUserComponent, UserComponent>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddTransient<ISendEmail, SendEmail>();
            return services;
        }
    }
}
