using UnityEngine;
using UnityEditor;


namespace Common
{

    [CustomEditor(typeof(Game_Event))]
    public class Event_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            Game_Event e = target as Game_Event;
            if (GUILayout.Button("Raise"))
                e.Raise();
        }
    }
}