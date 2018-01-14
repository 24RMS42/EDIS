using System;
using System.Diagnostics;
using EDIS.Data.Api.Base;
using EDIS.Data.Api.Base.Interfaces;
using EDIS.Data.Database.DatabaseManager;
using EDIS.Service.Implementation;
using EDIS.Service.Interfaces;
using EDIS.Shared.Services;
using EDIS.Shared.Services.Interfaces;
using EDIS.Shared.ViewModels.Certificates;
using EDIS.Shared.ViewModels.Estates;
using Microsoft.Practices.Unity;

namespace EDIS.Shared.ViewModels.Base
{
    public class ViewModelLocator
    {
        private readonly IUnityContainer _unityContainer;

        public static ViewModelLocator Instance { get; } = new ViewModelLocator();

        public ViewModelLocator()
        {
            _unityContainer = new UnityContainer();

            // providers
            _unityContainer.RegisterType<IRequestProvider, RequestProvider>();
            _unityContainer.RegisterType<IDatabaseManager, DatabaseManager>();

            // services
            RegisterSingleton<INavigationService, NavigationService>();

            // data services
            _unityContainer.RegisterType<IAuthenticationService, AuthenticationService>();
            _unityContainer.RegisterType<IEstatesService, EstatesService>();
            _unityContainer.RegisterType<IBuildingsService, BuildingsService>();
            _unityContainer.RegisterType<IBuildingService, BuildingService>();
            _unityContainer.RegisterType<IProfileService, ProfileService>();
            _unityContainer.RegisterType<ICertificatesService, CertificateService>();
            _unityContainer.RegisterType<IBoardsService, BoardsService>();
            _unityContainer.RegisterType<IEstatesLookupsService, EstatesLookupsService>();
            _unityContainer.RegisterType<ILookupsService, LookupsService>();

            // view models
            _unityContainer.RegisterType<LoginViewModel>();
            _unityContainer.RegisterType<MainViewModel>();
            _unityContainer.RegisterType<MenuViewModel>();
            _unityContainer.RegisterType<CertificatesViewModel>();
            _unityContainer.RegisterType<EstatesViewModel>();
        }

        public T Resolve<T>()
        {
            try
            {
                return _unityContainer.Resolve<T>();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        public object Resolve(Type type)
        {
            try
            {
                return _unityContainer.Resolve(type);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }

        }

        public void Register<T>(T instance)
        {
            _unityContainer.RegisterInstance<T>(instance);
        }

        public void Register<TInterface, T>() where T : TInterface
        {
            _unityContainer.RegisterType<TInterface, T>();
        }

        public void RegisterSingleton<TInterface, T>() where T : TInterface
        {
            _unityContainer.RegisterType<TInterface, T>(new ContainerControlledLifetimeManager());
        }
    }
}