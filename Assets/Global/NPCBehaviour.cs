using UnityEditor;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Events;

namespace Global
{
    public class NPCBehaviour : MonoBehaviour
    {
        public CountableEvent DialogEvents;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (DialogEvents.InvokeCount == 0)
                DialogEvents.Invoke();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (DialogEvents.InvokeCount == 0)
                DialogEvents.Invoke();
        }
    }

    // [CustomEditor(typeof(NPCBehaviour))]
    // public class NPCBehaviourEditor : Editor
    // {
    //     private SerializedProperty _dialogEventsProp;
    //
    //     private void OnEnable()
    //     {
    //         _dialogEventsProp = serializedObject.FindProperty("DialogEvents");
    //     }
    //
    //     public override void OnInspectorGUI()
    //     {
    //         serializedObject.Update();
    //
    //         // Display the DialogEvents field using default Unity GUI
    //         EditorGUILayout.PropertyField(_dialogEventsProp);
    //
    //         serializedObject.ApplyModifiedProperties();
    //     }
    // }
}
