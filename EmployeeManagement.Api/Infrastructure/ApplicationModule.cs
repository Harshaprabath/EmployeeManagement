using Autofac;
using EmployeeManagement.Business;

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
              .As<IEmployeeService>()
              .InstancePerLifetimeScope();
        }

    }
}