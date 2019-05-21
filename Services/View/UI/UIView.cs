namespace Hermit.View
{
    public abstract class UIView<TViewModel> : UIViewBase, IViewModelProvider where TViewModel : ViewModel
    {
        public TViewModel DataContext { get; protected set; }
        
        public override void SetViewModel(object context)
        {
            if (context is TViewModel viewModel) { DataContext = viewModel; }
            else { Her.Warn($"{context} is not matching {typeof(TViewModel)}"); }
        }

        public virtual object GetViewModel()
        {
            return DataContext;
        }
    }

    public abstract class UIView : UIView<EmptyViewModel>
    {
        public override void SetViewModel(object context)
        {
            DataContext = (EmptyViewModel) ViewModel.Empty;
            if (context != null) { Her.Log($"{GetType().Name} will not receive any models."); }
        }
    }
}