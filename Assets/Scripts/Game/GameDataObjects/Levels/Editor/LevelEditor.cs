using UnityEngine;
using UnityEditor;
using Common;

[CustomEditor(typeof(Level))]
public class LevelEditor : Editor
{
    Level _level;
    Color originalGUIColor;
    Vector2 scrollPos;
    private bool FoldLevels = true;



    public override void OnInspectorGUI()
    {
        #region Header        
        originalGUIColor = GUI.color;
        EditorGUIUtility.labelWidth = 250;
        EditorGUILayout.Space();

        GUI.color = new Color(.75f, 1f, .75f);
        EditorGUILayout.LabelField("Level Editor Window", EditorStyles.boldLabel);
        GUI.color = originalGUIColor;
        // Header's paragraph
        EditorGUILayout.LabelField("set level NPC's ,Game Elements ,Player ,Conditions ", EditorStyles.helpBox);
        GUI.color = originalGUIColor;
        EditorGUILayout.Space();
        //EditorGUILayout.Space();
        #endregion

        #region Init serializedObject 
        _level = (Level)target;
        SerializedObject serializedObject = new SerializedObject(_level);
        serializedObject.Update();
        #endregion

        EditorGUI.indentLevel++;

        #region Settings - section
        //___________________________________________________________________________________________________

        //// fold GeneralSettings section
        //foldGeneralSettings = EditorGUILayout.Foldout(foldGeneralSettings, "Application General Settings");

        //if (foldGeneralSettings)
        //{
        // outer box
        EditorGUILayout.BeginVertical(GUI.skin.box);

        GUI.color = new Color(.75f, 1f, .75f);
        GUILayout.Label("Settings", EditorStyles.boldLabel);

        GUI.color = originalGUIColor;
        // Value- overrideFixedTimeStep
        EditorGUILayout.PropertyField(serializedObject.FindProperty("LevelNumber"), new GUIContent("Level Number"));

        //    // Value- fixedTimeStep
        //    if (_level.overrideFixedTimeStep)
        //        EditorGUILayout.PropertyField(serializedObject.FindProperty("fixedTimeStep"), new GUIContent("Fixed Timestep"));

        //    // Value- maxAngularVelocity
        //    EditorGUILayout.PropertyField(serializedObject.FindProperty("maxAngularVelocity"), new GUIContent("Maximum Angular Velocity"));

        //    // Value- behaviorType
        //    EditorGUILayout.PropertyField(serializedObject.FindProperty("behaviorType"), new GUIContent("Behavior Type"));
        //    GUI.color = new Color(.75f, 1f, .75f);

        //    // Green box tooltip
        //    EditorGUILayout.HelpBox("Using behavior preset will override  settings,  yaw, roll, and other stuff. Using ''Custom'' mode will not override anything.", MessageType.Info);
        //    GUI.color = originalGUIColor;

        //    // Value- useFixedWheelColliders
        //    EditorGUILayout.PropertyField(serializedObject.FindProperty("useFixedWheelColliders"), new GUIContent("Use Fixed WheelColliders"));
        EditorGUILayout.EndVertical();
        //}
        #endregion
        

        // Open ScrollView -------------------------------------------------
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, false);

        //base.OnInspectorGUI();

        
        EditorGUILayout.BeginVertical(GUI.skin.button);
        GUI.color = new Color(.75f, 1f, .75f);
        //GUI.color = new Color(.5f, 1f, 1f, 1f);

        #region NPCs
        EditorGUILayout.BeginVertical(GUI.skin.box);
        GUI.color = new Color(.75f, 1f, .75f);
        GUILayout.Label("Set Non Playable Characters", EditorStyles.boldLabel);
        GUI.color = originalGUIColor;
        SerializedProperty serializedPropertyList = serializedObject.FindProperty("Npcs");
        Draw(serializedPropertyList);
        EditorGUILayout.EndVertical();
        #endregion

        #region Elements
        EditorGUILayout.BeginVertical(GUI.skin.box);
        GUI.color = new Color(.75f, 1f, .75f);
        GUILayout.Label("Set Elements", EditorStyles.boldLabel);
        GUI.color = originalGUIColor;
        serializedPropertyList = serializedObject.FindProperty("Elements");
        Draw(serializedPropertyList);
        EditorGUILayout.EndVertical();
        #endregion

        #region Conditions
        EditorGUILayout.BeginVertical(GUI.skin.box);
        GUI.color = new Color(.75f, 1f, .75f);
        GUILayout.Label("Set Conditions", EditorStyles.boldLabel);
        GUI.color = originalGUIColor;
        serializedPropertyList = serializedObject.FindProperty("Conditions");
        Draw(serializedPropertyList);
        EditorGUILayout.EndVertical();
        #endregion

        #region PlayerSpecs
        EditorGUILayout.BeginVertical(GUI.skin.box);
        GUI.color = new Color(.75f, 1f, .75f);
        GUILayout.Label("Set Player specifications", EditorStyles.boldLabel);
        GUI.color = originalGUIColor;
        serializedPropertyList = serializedObject.FindProperty("PlayerSpecs");
        Draw(serializedPropertyList);
        EditorGUILayout.EndVertical();
        #endregion
        


        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();
        GUI.color = originalGUIColor;


        EditorGUILayout.EndScrollView();
        // Close ScrollView -------------------------------------------------

        

        #region footer

        EditorGUILayout.BeginVertical(GUI.skin.button);
        GUI.color = new Color(.75f, 1f, .75f);
        GUI.color = new Color(.5f, 1f, 1f, 1f);

        // reset button
        if (GUILayout.Button("Test Level"))
        {
            //ResetToDefaults();
            //Debug.Log("Resetted To Defaults!");
        }
        // pdf button
        if (GUILayout.Button("Browse Site"))
        {
            string url = "http://www.google.com";
            Application.OpenURL(url);
        }

        GUI.color = originalGUIColor;
        EditorGUILayout.LabelField("Level", EditorStyles.centeredGreyMiniLabel, GUILayout.MaxHeight(20f));

        #endregion

        EditorGUILayout.EndVertical();

        // update changes to asset file
        serializedObject.ApplyModifiedProperties();
        if (GUI.changed)
            EditorUtility.SetDirty(_level);
    }

    private void Draw(SerializedProperty list)
    {
        EditorGUILayout.PropertyField(list);
        //ShowElements(list);
    }

    private void ShowElements(SerializedProperty list, bool showElementLabels = true)
    {
        for (int i = 0; i < list.arraySize; i++)
        {
            EditorGUILayout.BeginHorizontal();
            if (showElementLabels)
            {
                EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i));
            }
            else
            {
                EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), GUIContent.none);
            }

            //if (GUILayout.Button("Delete", EditorStyles.miniButtonRight, GUILayout.ExpandWidth(false)))
            //{
            //    Object.DestroyImmediate(list.GetArrayElementAtIndex(i).objectReferenceValue, true);
            //    levels.levels.RemoveAt(i);
            //    AssetDatabase.SaveAssets();
            //}

            EditorGUILayout.EndHorizontal();
        }
    }


}
