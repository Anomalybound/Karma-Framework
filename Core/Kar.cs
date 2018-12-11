using System;
using Karma.Services;
using wLib;
using wLib.Injection;
using wLib.UIStack;

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

        private readonly DiContainer _container;

        private readonly IEventBroker _eventBroker;

        private readonly IUIManager _uiManager;

        private readonly ILog _logger;

        public Kar(DiContainer container, IEventBroker eventBroker, IUIManager uiManager, ILog logger)
        {
            _container = container;
            _eventBroker = eventBroker;
            _uiManager = uiManager;
            _logger = logger;

            instance = this;
        }
    }
}