namespace Karma.View
{
    public abstract class UIView<TDataContext> : UIViewBase where TDataContext : DataContext
    {
        protected TDataContext DataContext { get; private set; }

        public override void SetDataContext(object context)
        {
            if (context is TDataContext dataContext) { DataContext = dataContext; }
            else { Logger.Warn($"{context} is not matching {typeof(TDataContext)}"); }
        }
    }

    public abstract class UIView : UIViewBase
    {
        protected DataContext DataContext { get; private set; }

        public override void SetDataContext(object context)
        {
            if (context is DataContext dataContext) { DataContext = dataContext; }
            else { Logger.Warn($"{context} is not matching {typeof(DataContext)}"); }
        }
    }
}