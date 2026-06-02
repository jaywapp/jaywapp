using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Jaywapp.Wpf.Helpers;
using Jaywapp.Wpf.Interfaces;
using Microsoft.Practices.Unity;
using Prism.Unity;
using ReactiveUI;

namespace Jaywapp.Wpf.Services
{
    public class JayBootstraper<TShell, TShellViewModel> 
        : UnityBootstrapper
        where TShell : Window
        where TShellViewModel : ReactiveObject
    {
        private readonly IJayViewMeterial[] _meterials;

        public JayBootstraper(params IJayViewMeterial[] meterials)
        {
            _meterials = meterials ?? throw new ArgumentNullException(nameof(meterials));
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            foreach(var meterial in _meterials)
                Container.RegisterSingletone(meterial.GetViewType(), meterial.GetViewModelType());
        }

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<TShell>();
        }
    }
}
