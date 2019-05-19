using System.Linq;
using System.Reflection;
using Karma.Common;
using Karma.Injection;
using UnityEditor;
using UnityEngine;

namespace Karma
{
    public class KarmaKernelEditor
    {
        public const string KARMA_DOTWEEN = "KARMA_DOTWEEN";

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

#if KARMA_DOTWEEN
        [MenuItem("Karma/Extensions/Disable DoTween Extensions")]
#else
        [MenuItem("Karma/Extensions/Enable DoTween Extensions")]
#endif
        public static void ToggleDoTweenSupport()
        {
            var targetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            var defineString = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup);
            var defines = defineString.Split(';').ToList();
#if KARMA_DOTWEEN
            if (defines.Contains(KARMA_DOTWEEN)) { defines.Remove(KARMA_DOTWEEN); }
#else
            if (!defines.Contains(KARMA_DOTWEEN)) { defines.Add(KARMA_DOTWEEN); }
#endif
            defineString = string.Join(";", defines);
            PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup, defineString);
        }
    }
}