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

        public override void RegisterBindings(IDependencyContainer Container)
        {
            // Log
            Container.Bind<ITime>().To<UnityTime>();
            Container.Bind<ILog>().To<UnityLog>().FromInstance(new UnityLog(EnableLog));
            Container.Bind<IEventBroker>().To<EventBroker>();
            Container.Bind<IUIStack>().FromMethod(BuildUIStackInstance);

            // View Loader
            Container.Bind<IViewLoader>().To<ResourcesViewLoader>();
        }

        protected IUIStack BuildUIStackInstance(IDependencyContainer container)
        {
            return UiStackInstance != null
                ? UIStackManager.FromInstance(UiStackInstance)
                : UIStackManager.BuildHierarchy(IsLandscape, ReferenceResolution);
        }
    }
}