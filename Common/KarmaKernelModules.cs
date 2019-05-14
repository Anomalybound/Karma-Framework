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
        protected bool EnableLog = true;

        [Header("UI")]
        [SerializeField]
        protected bool IsLandscape = true;

        [SerializeField]
        protected Vector2 ReferenceResolution = new Vector2(1920, 1080);

        [SerializeField]
        [Tooltip("Can left empty.")]
        protected UIStackManager UiStackInstance;

        public override void RegisterBindings()
        {
            // Log
            Container.Bind<ITime, UnityTime>();
            Container.Bind<ILog, UnityLog>().FromInstance(new UnityLog(EnableLog));
            Container.Bind<IEventBroker, EventBroker>();
            Container.Bind<IUIStack>().FromMethod(
                () => UiStackInstance != null
                    ? UIStackManager.FromInstance(UiStackInstance)
                    : UIStackManager.BuildHierarchy(IsLandscape, ReferenceResolution)
            );

            // View Loader
            Container.Bind<IViewLoader, ResourcesViewLoader>();
        }
    }
}