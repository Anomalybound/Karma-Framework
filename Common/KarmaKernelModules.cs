using Karma.Services;
using UnityEngine;
using wLib;
using wLib.Injection;
using wLib.Procedure;
using wLib.UIStack;

namespace Karma.Common
{
    public class KarmaKernelModules : MonoModule
    {
        [Header("UI")]
        [SerializeField]
        private bool _fromInstance;

        [SerializeField]
        private bool _isLandscape;

        [SerializeField]
        private Vector2 _referenceResolution = new Vector2(1920, 1080);

        public override void ModuleBindings()
        {
            // Essentials
            Container.Bind<ILog, UnityLogger>();
            Container.Bind<IEventBroker, EventBroker>();
            Container.Bind<IUIManager>().FromMethod(
                () => _fromInstance
                    ? UIManager.FromInstance(Container)
                    : UIManager.BuildHierarchy(Container, _isLandscape, _referenceResolution)
            );
        }
    }
}