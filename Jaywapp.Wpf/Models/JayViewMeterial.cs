using System.Windows.Controls;
using Jaywapp.Wpf.Interfaces;
using ReactiveUI;

namespace Jaywapp.Wpf.Models
{
    public class JayViewMeterial<TView, TViewModel> : IJayViewMeterial
        where TView : UserControl
        where TViewModel : ReactiveObject
    {
        public Type GetViewType() => typeof(TView);

        public Type GetViewModelType() => typeof(TViewModel);   
    }
}
