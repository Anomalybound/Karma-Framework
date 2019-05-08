using Karma.Services;
using UnityEngine;
using wLib;
using wLib.Injection;
using wLib.UIStack;

namespace Karma.Common
{
    public class KarmaKernelModules : MonoModule
    {
        [Header("General")]
        [SerializeField]
        private bool _enableLog = true;

        [Header("UI")]
        [SerializeField]
        private bool _isLandscape;

        [SerializeField]
        private Vector2 _referenceResolution = new Vector2(1920, 1080);

        [SerializeField]
        [Tooltip("Can left empty.")]
        private UIManager _uiMgrInstance;

        public override void RegisterBindings()
        {
            // Essentials
            Container.Bind<ILog, UnityLog>().FromInstance(new UnityLog(_enableLog));
            Container.Bind<IEventBroker, EventBroker>();
            Container.Bind<IUIManager>().FromMethod(
                () => _uiMgrInstance != null
                    ? UIManager.FromInstance(Container, _uiMgrInstance)
                    : UIManager.BuildHierarchy(Container, _isLandscape, _referenceResolution)
            );

            Container.Bind<IViewLoader, ResourcesViewLoader>();
            Container.Bind<Kar, Kar>().NonLazy();
        }

        public override void Dispose() { }
    }
}