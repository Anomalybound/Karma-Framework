using UnityEditor;
using Karma.Procedure;

namespace Karma
{
    [CustomEditor(typeof(GameProcedureController<,>), true)]
    public class GameProcedureControllerEditor : FsmContainerEditor
    {
        public override void OnInspectorGUI()
        {
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_initState"));
                if (check.changed) { serializedObject.ApplyModifiedProperties(); }
            }

            base.OnInspectorGUI();
        }
    }
}