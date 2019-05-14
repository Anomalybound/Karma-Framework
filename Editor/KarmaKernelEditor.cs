using Karma.Common;
using UnityEditor;
using UnityEngine;

namespace Karma
{
    public class KarmaKernelEditor
    {
        [MenuItem("Karma/Quick Scene Setup %#k")]
        public static void Setup()
        {
            var kernel = Object.FindObjectOfType<KarmaKernel>();
            if (kernel != null)
            {
                Debug.LogWarning("Kernel already exists.");
                return;
            }

            kernel = Resources.Load<KarmaKernel>("Karma Kernel");
            if (kernel != null) { Object.Instantiate(kernel).name = "Karma Kernel"; }
        }
    }
}