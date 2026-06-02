using Microsoft.Practices.Unity;
using Prism.Mvvm;

namespace Jaywapp.Wpf.Helpers
{
    public static class ContainerHelper
    {
        public static void RegisterSingletone<TView, TViewModel>(this IUnityContainer container, bool create = false)
        {
            container.RegisterType<TView>(new ContainerControlledLifetimeManager());
            container.RegisterType<TViewModel>(new ContainerControlledLifetimeManager());

            if(create)
            {
                container.Resolve<TView>();
                container.Resolve<TViewModel>();
            }

            ViewModelLocationProvider.Register<TView, TViewModel>();
        }

        public static void RegisterSingletone(
            this IUnityContainer container, Type viewType, Type viewModelType, bool create = false)
        {
            container.RegisterType(viewType, new  ContainerControlledLifetimeManager());    
            container.RegisterType(viewModelType, new ContainerControlledLifetimeManager());

            if (create)
            {
                container.Resolve(viewType);
                container.Resolve(viewModelType);
            }

            ViewModelLocationProvider.Register(viewType.ToString(), viewModelType);
        }
    }
}
