using AnimFlex.Editor;
using UnityEngine;

namespace DefaultNamespace
{
    public class Testt : MonoBehaviour
    {

        public void Test()
        {
            AFEditorUtils.ReloadUnsavedDirtyScene();
        }
    }
}