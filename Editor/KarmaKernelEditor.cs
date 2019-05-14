using System.Reflection;
using Karma.Common;
using Karma.Injection;
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

            var kernelObj = new GameObject("Karma Kernel", typeof(KarmaKernel), typeof(KarmaKernelModules));
            kernel = kernelObj.GetComponent<KarmaKernel>();

            var array = new MonoModule[] {kernelObj.GetComponent<KarmaKernelModules>()};
            var fieldInfo = typeof(KarmaKernel).GetField("Modules", BindingFlags.NonPublic | BindingFlags.Instance);
            fieldInfo?.SetValue(kernel, array);
        }
    }
}