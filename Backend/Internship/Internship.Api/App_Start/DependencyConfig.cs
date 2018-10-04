using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System.Web.Http;
using Internship.Data;
using Internship.Data.Repositories;

namespace Internship.Api.App_Start
{
    public class DependencyConfig
    {
        public static void RegisterDependencies()
        {
            // 1. Create a new Simple Injector container
            var container = new Container();

            // 2. Cach instances during the execution of a single Http Web Request
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            // 3. Configure the container (register)
            container.Register<ApplicationDbContext>(() => new ApplicationDbContext(), Lifestyle.Scoped);
            container.Register<IStagevoorstelRepository, StagevoorstelDBRepository>(Lifestyle.Scoped);
            container.Register<IStageopdrachtenRepository, StageopdrachtenDbRepository>(Lifestyle.Scoped);
            container.Register<IBedrijfRepository, BedrijfDBRepository>(Lifestyle.Scoped);
            container.Register<ILectorRepository, LectorDBRepository>(Lifestyle.Scoped);
            container.Register<IStudentRepository, StudentDBRepository>(Lifestyle.Scoped);
            container.Register<IStagecoördinatorRepository, StagecoördinatorDBRepository>(Lifestyle.Scoped);
            container.Register<IUserAccountRepository, UserAccountDbRepository>(Lifestyle.Scoped);
            container.Register<IStageRepository, StageDBRepository>(Lifestyle.Scoped);


            // 4. Optionally verify the container's configuration.
            container.Verify();

            // 5. Register the container as WebAPI IDependencyResolver.
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}