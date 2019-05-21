using UnityEditor;
using UnityEngine;

namespace Hermit.DataBindings
{
    [CustomEditor(typeof(OneWayDataBinding))]
    public class OneWayDataBindingEditor : Editor
    {
        protected OneWayDataBinding Target;

        private void OnEnable()
        {
            Target = target as OneWayDataBinding;
            if (Target != null)
            {
                if (Target.DataProvider != null) { return; }

                var dataProvider = Target.GetComponentInParent<IViewModelProvider>();
                Target.DataProvider = dataProvider as Component;

                if (Target.DataProvider != null) { Debug.Log("Data Provider Found."); }
            }
        }
    }
}