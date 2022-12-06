using Autofac;
using Microsoft.AspNetCore.Http;
using EmployeeManagement.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Api.Infrastructure
{
    public class ApplicationModule : Autofac.Module
    {
        public ApplicationModule()
        {


        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmployeeService>()
              .As<EmployeeService>()
              .InstancePerLifetimeScope();
        }

    }
}