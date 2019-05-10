using UnityWeld.Binding;

namespace Karma.View
{
    public abstract class UIView<TViewModel> : UIViewBase, IViewModelProvider where TViewModel : ViewModel
    {
        public TViewModel DataContext { get; protected set; }

        public override void SetViewModel(object context)
        {
            if (context is TViewModel viewModel) { DataContext = viewModel; }
            else { Kar.Warn($"{context} is not matching {typeof(TViewModel)}"); }
        }

        public object GetViewModel()
        {
            return DataContext;
        }

        public string GetViewModelTypeName()
        {
            return typeof(TViewModel).FullName;
        }
    }

    public abstract class UIView : UIView<EmptyViewModel>
    {
        public override void SetViewModel(object context)
        {
            DataContext = (EmptyViewModel) ViewModel.Empty;
            if (context != null) { Kar.Log($"{GetType().Name} will not receive any models."); }
        }
    }
}