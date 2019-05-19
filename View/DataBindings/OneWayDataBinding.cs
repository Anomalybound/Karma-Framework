using UnityEngine;

namespace Karma
{
    public class OneWayDataBinding : MonoBehaviour
    {
        public Component DataProvider;

        public string PropertyEventName;
        
        private void Awake()
        {
            var provider = DataProvider as IViewModelProvider;
            if (provider?.GetViewModel() is ViewModel viewModel)
            {
                
            }
        }
    }
}