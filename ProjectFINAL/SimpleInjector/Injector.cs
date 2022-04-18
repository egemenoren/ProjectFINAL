using Project.Data.Context;
using Project.Data.Repository;
using Project.Entity;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace ProjectFINAL.SimpleInjector
{
    public class Injector
    {
        public static void Register()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            container.Register<IGenericRepository<BaseEntity>, GenericRepository<BaseEntity>>(Lifestyle.Scoped);
            container.Register<IHumidityRateRepository, HumidityRateRepository>(Lifestyle.Scoped);
            container.Register<DbContext,ProjectContext>(Lifestyle.Scoped);
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.Verify();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}