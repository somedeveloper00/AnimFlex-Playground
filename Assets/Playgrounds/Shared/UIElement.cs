using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Playgrounds.Shared
{
    public class UIElement : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_label, m_value;

        private void OnValidate()
        {
            if (m_label == null || m_value == null)
            {
                var children = GetComponentsInChildren<TMP_Text>();
                m_label = children.First(obj => obj.name.ToLowerInvariant().Contains("label"));
                m_value = children.First(obj => obj.name.ToLowerInvariant().Contains("value"));
            }

            m_value.fontSize = m_label.fontSize;
            m_value.color = m_label.color;
        }

        public string Label
        {
            get => m_label.text;
            set => m_label.text = value;
        }

        public string Value
        {
            get => m_value.text;
            set => this.m_value.text = value;
        }

        public float fontSize
        {
            get => m_label.fontSize;
            set => m_label.fontSize = m_value.fontSize = value;
        }

        public Color color
        {
            get => m_label.color;
            set => m_label.color = m_value.color = value;
        }

#if UNITY_EDITOR
        [CustomEditor(typeof(UIElement))]
        private class editor : Editor
        {
            private UIElement instance;
            private void OnEnable() => instance = target as UIElement;

            public override void OnInspectorGUI()
            {
                serializedObject.Update();
                using (new GUILayout.HorizontalScope())
                {
                    // label
                    using (new GUILayout.VerticalScope())
                    {
                        GUILayout.Label("Label");
                        EditorGUILayout.PropertyField(
                            serializedObject.FindProperty(nameof(m_label)), GUIContent.none);
                        using (var check = new EditorGUI.ChangeCheckScope())
                        {
                            var txt = EditorGUILayout.TextField(GUIContent.none, instance.Label);
                            if (check.changed)
                            {
                                Undo.RecordObjects(new Object[] { instance.m_label, instance.m_value },
                                    "label modified");
                                instance.Label = txt;
                            }
                        }
                    }

                    // value
                    using (new GUILayout.VerticalScope())
                    {
                        GUILayout.Label("Value");
                        EditorGUILayout.PropertyField(
                            serializedObject.FindProperty(nameof(m_value)), GUIContent.none);
                        using (var check = new EditorGUI.ChangeCheckScope())
                        {
                            var txt = EditorGUILayout.TextField(GUIContent.none, instance.Value);
                            if (check.changed)
                            {
                                Undo.RecordObjects(new Object[] { instance.m_label, instance.m_value }, "value modified");
                                instance.Value = txt;
                            }
                        }
                    }
                }

                using (var check = new EditorGUI.ChangeCheckScope())
                {
                    var value = EditorGUILayout.FloatField("Font Size", instance.fontSize);
                    if (check.changed)
                    {
                        Undo.RecordObjects(new Object[] { instance.m_label, instance.m_value }, "font size changed");
                        instance.fontSize = value;
                    }
                }
                using (var check = new EditorGUI.ChangeCheckScope())
                {
                    var value = EditorGUILayout.ColorField("Color", instance.color);
                    if (check.changed)
                    {
                        Undo.RecordObjects(new Object[] { instance.m_label, instance.m_value }, "color changed");
                        instance.color = value;
                    }
                }


                serializedObject.ApplyModifiedProperties();
            }
        }
#endif
    }
}