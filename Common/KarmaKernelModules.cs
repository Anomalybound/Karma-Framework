using Karma.Injection;
using Karma.Services;
using UnityEngine;
using Karma.UIStack;

namespace Karma.Common
{
    public class KarmaKernelModules : MonoModule
    {
        [Header("General")]
        [SerializeField]
        protected bool _enableLog = true;

        [Header("UI")]
        [SerializeField]
        protected bool _isLandscape = true;

        [SerializeField]
        protected Vector2 _referenceResolution = new Vector2(1920, 1080);

        [SerializeField]
        [Tooltip("Can left empty.")]
        protected UIStackManager _uiMgrInstance;

        public override void RegisterBindings()
        {
            // Log
            Container.Bind<ITime, UnityTime>();
            Container.Bind<ILog, UnityLog>().FromInstance(new UnityLog(_enableLog));
            Container.Bind<IEventBroker, EventBroker>();
            Container.Bind<IUIStack>().FromMethod(
                () => _uiMgrInstance != null
                    ? UIStackManager.FromInstance(Container, _uiMgrInstance)
                    : UIStackManager.BuildHierarchy(Container, _isLandscape, _referenceResolution)
            );

            // View Loader
            Container.Bind<IViewLoader, ResourcesViewLoader>();
        }
    }
}