using AnimFlex.Editor;
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
    [CustomEditor(typeof(Testt))]
    public class TestEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if(GUILayout.Button("Do"))
            {
                AFEditorUtils.ReloadUnsavedDirtyScene();
            }
        }
    }
}