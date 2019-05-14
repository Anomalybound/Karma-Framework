using System;
using Karma.Injection;
using Karma.UIStack;

namespace Karma
{
    public partial class Kar
    {
        public static Version Version = new Version("0.0.1");

        private static Kar Instance
        {
            get
            {
                if (instance == null) { throw new Exception("Please add KarmaKernel to your bootstrap scene."); }

                return instance;
            }
        }

        private static Kar instance;

        private readonly IDependencyContainer _container;

        private readonly IEventBroker _eventBroker;

        private readonly IUIStack _iuiStack;

        private readonly ILog _logger;

        public Kar(IDependencyContainer container, IEventBroker eventBroker, IUIStack iuiStack, ILog logger)
        {
            _container = container;
            _eventBroker = eventBroker;
            _iuiStack = iuiStack;
            _logger = logger;

            instance = this;
        }
    }
}