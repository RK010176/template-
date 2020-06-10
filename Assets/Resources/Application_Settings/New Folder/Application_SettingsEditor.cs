using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Application_Settings))]
public class Application_SettingsEditor : Editor
{
    Application_Settings Application_SettingsAssets;
    Color originalGUIColor;
    Vector2 scrollPos;


    #region related to UI bounding boxs folding (Saves Last folding status)
    bool foldGeneralSettings = false;
    bool foldControllerSettings = false;
    bool foldUISettings = false;
    bool foldOptimization = false;
    bool foldTagsAndLayers = false;


    void OnEnable()
    {
        foldGeneralSettings = Application_Settings.Instance.foldGeneralSettings;
        foldControllerSettings = Application_Settings.Instance.foldControllerSettings;
        foldUISettings = Application_Settings.Instance.foldUISettings;
        foldOptimization = Application_Settings.Instance.foldOptimization;
        foldTagsAndLayers = Application_Settings.Instance.foldTagsAndLayers;
    }

    void OnDestroy()
    {
        Application_Settings.Instance.foldGeneralSettings = foldGeneralSettings;
        Application_Settings.Instance.foldControllerSettings = foldControllerSettings;
        Application_Settings.Instance.foldUISettings = foldUISettings;
        Application_Settings.Instance.foldOptimization = foldOptimization;
        Application_Settings.Instance.foldTagsAndLayers = foldTagsAndLayers;
    }

    #endregion


    public override void OnInspectorGUI()
    {
        // load asset file
        serializedObject.Update();
        Application_SettingsAssets = (Application_Settings)target;

        #region Header

        // top Header
        originalGUIColor = GUI.color;
        EditorGUIUtility.labelWidth = 250;
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Application Asset Settings Editor", EditorStyles.boldLabel);
        GUI.color = new Color(.75f, 1f, .75f);

        // Header's paragraph
        EditorGUILayout.LabelField("This editor will keep update necessary .asset files in your project. Don't change directory of the ''Resources/Application_Settings''.", EditorStyles.helpBox);
        GUI.color = originalGUIColor;
        EditorGUILayout.Space();

        #endregion

        // move text to right
        EditorGUI.indentLevel++;

        // Open ScrollView -------------------------------------------------
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, false);

        // row space
        EditorGUILayout.Space();

        #region General Settings - section
        //___________________________________________________________________________________________________

        // fold GeneralSettings section
        foldGeneralSettings = EditorGUILayout.Foldout(foldGeneralSettings, "Application General Settings");

        if (foldGeneralSettings)
        {
            // outer box
            EditorGUILayout.BeginVertical(GUI.skin.box); // must add EndVertical()

            GUI.color = new Color(.75f, 1f, .75f);
            GUILayout.Label("General Settings", EditorStyles.boldLabel);

            GUI.color = originalGUIColor;
            // Value- overrideFixedTimeStep
            EditorGUILayout.PropertyField(serializedObject.FindProperty("overrideFixedTimeStep"), new GUIContent("Override FixedTimeStep"));

            // Value- fixedTimeStep
            if (Application_SettingsAssets.overrideFixedTimeStep)
                EditorGUILayout.PropertyField(serializedObject.FindProperty("fixedTimeStep"), new GUIContent("Fixed Timestep"));

            // Value- maxAngularVelocity
            EditorGUILayout.PropertyField(serializedObject.FindProperty("maxAngularVelocity"), new GUIContent("Maximum Angular Velocity"));

            // Value- behaviorType
            EditorGUILayout.PropertyField(serializedObject.FindProperty("behaviorType"), new GUIContent("Behavior Type"));
            GUI.color = new Color(.75f, 1f, .75f);

            // Green box tooltip
            EditorGUILayout.HelpBox("Using behavior preset will override  settings,  yaw, roll, and other stuff. Using ''Custom'' mode will not override anything.", MessageType.Info);
            GUI.color = originalGUIColor;

            // Value- useFixedWheelColliders
            EditorGUILayout.PropertyField(serializedObject.FindProperty("useFixedWheelColliders"), new GUIContent("Use Fixed WheelColliders"));
            EditorGUILayout.EndVertical();
        }
        #endregion

        EditorGUILayout.Space();


        #region Controller Settings - section
        foldControllerSettings = EditorGUILayout.Foldout(foldControllerSettings, "Controller Settings");

        if (foldControllerSettings)
        {
            // Controller Types
            List<string> controllerTypeStrings = new List<string>();
            controllerTypeStrings.Add("HeadSet");
            controllerTypeStrings.Add("Mobile");
            controllerTypeStrings.Add("Custom");

            // box Main Controller Type-------------------------------------------------
            EditorGUILayout.BeginVertical(GUI.skin.box);

            GUI.color = new Color(.75f, 1f, .75f);
            GUILayout.Label("Main Controller Type", EditorStyles.boldLabel);

            // 3 tabs
            GUI.color = new Color(.5f, 1f, 1f, 1f);
            Application_SettingsAssets.toolbarSelectedIndex = GUILayout.Toolbar(Application_SettingsAssets.toolbarSelectedIndex, controllerTypeStrings.ToArray());
            GUI.color = originalGUIColor;

            #region Tabs

            if (Application_SettingsAssets.toolbarSelectedIndex == 0)
            {
                // box 1-------------------------------------------------
                EditorGUILayout.BeginVertical(GUI.skin.box);
                // set enum
                Application_SettingsAssets.controllerType = Application_Settings.ControllerType.Headset;

                GUILayout.Label("Settings", EditorStyles.boldLabel);

                // properties
                EditorGUILayout.PropertyField(serializedObject.FindProperty("Bool1"), new GUIContent("use Bool1"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("Bool2"), new GUIContent("run Bool2"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("Bool3"), new GUIContent("keep Bool3"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("Bool4"), new GUIContent("auto Bool4"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("Bool5"), new GUIContent("fix Bool5"));

                EditorGUILayout.PropertyField(serializedObject.FindProperty("UIButtonSensitivity"), new GUIContent("UI Button Sensitivity"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("UIButtonGravity"), new GUIContent("UI Button Gravity"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("gyroSensitivity"), new GUIContent("gyro Sensitivity"));


                EditorGUILayout.EndVertical();
                // close box 1-------------------------------------------
            }


            if (Application_SettingsAssets.toolbarSelectedIndex == 1)
            {
                // box 2-------------------------------------------------
                EditorGUILayout.BeginVertical(GUI.skin.box);
                Application_SettingsAssets.controllerType = Application_Settings.ControllerType.Mobile;
                GUILayout.Label("Settings", EditorStyles.boldLabel);

                // properties
                EditorGUILayout.PropertyField(serializedObject.FindProperty("UIButtonSensitivity"), new GUIContent("UI Button Sensitivity"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("uiType"), new GUIContent("UI Type"));
                GUI.color = new Color(.75f, 1f, .75f);

                EditorGUILayout.EndVertical();
                // close box 2-------------------------------------------
            }

            if (Application_SettingsAssets.toolbarSelectedIndex == 2)
            {
                // box 3-------------------------------------------------
                EditorGUILayout.BeginVertical(GUI.skin.box);
                Application_SettingsAssets.controllerType = Application_Settings.ControllerType.Custom;
                GUILayout.Label("Settings", EditorStyles.boldLabel);

                // properties
                GUI.color = new Color(.75f, 1f, .75f);
                EditorGUILayout.HelpBox("In this mode, controller won't receive these inputs from keyboard or UI buttons. You need to feed these inputs in your own script.", MessageType.Info);
                EditorGUILayout.Space();
                EditorGUILayout.HelpBox("controller uses these inputs; \n  \n   A Input = Clamped 0f - 1f.  \n    B Input = Clamped 0f - 1f.  \n    C Input = Clamped -1f - 1f. ", MessageType.Info);
                EditorGUILayout.Space();
                GUI.color = originalGUIColor;

                EditorGUILayout.EndVertical();
                // close box 3-------------------------------------------
            }

            #endregion


            #region Main Controller Settings  
            //box Main-------------------------------------------------
            EditorGUILayout.BeginVertical(GUI.skin.box);
            GUI.color = new Color(.75f, 1f, .75f);
            GUILayout.Label("Main Controller Settings", EditorStyles.boldLabel);

            GUI.color = originalGUIColor;


            EditorGUILayout.PropertyField(serializedObject.FindProperty("units"), new GUIContent("Units"));


            EditorGUILayout.EndVertical();
            //close box Main-------------------------------------------
            #endregion


            EditorGUILayout.EndVertical();


        }
        #endregion



        #region Resources

        EditorGUILayout.BeginVertical(GUI.skin.box);

        GUILayout.Label("Resources", EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(serializedObject.FindProperty("A"), new GUIContent("1"), false);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("B"), new GUIContent("2"), false);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("C"), new GUIContent("3"), false);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("D"), new GUIContent("4"), false);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("E"), new GUIContent("5"), false);

        EditorGUILayout.Space();

        EditorGUILayout.Space();
        EditorGUILayout.EndVertical();

        #endregion



        EditorGUILayout.EndScrollView();
        // Close ScrollView -------------------------------------------------
        #region footer

        EditorGUILayout.BeginVertical(GUI.skin.button); // -> must have !!!!  EditorGUILayout.EndVertical();
        GUI.color = new Color(.75f, 1f, .75f);
        GUI.color = new Color(.5f, 1f, 1f, 1f);

        // reset button
        if (GUILayout.Button("Reset To Defaults"))
        {
            //ResetToDefaults();
            Debug.Log("Resetted To Defaults!");
        }
        // url button
        if (GUILayout.Button("Browse Site"))
        {
            string url = "http://www.google.com";
            Application.OpenURL(url);
        }

        GUI.color = originalGUIColor;
        EditorGUILayout.LabelField("GAME NAME", EditorStyles.centeredGreyMiniLabel, GUILayout.MaxHeight(50f));

        #endregion

        EditorGUILayout.EndVertical();

        // update changes to asset file
        serializedObject.ApplyModifiedProperties();
        if (GUI.changed)
            EditorUtility.SetDirty(Application_SettingsAssets);

    }


    void ResetToDefaults()
    {
        Application_SettingsAssets.overrideFixedTimeStep = true;
        Application_SettingsAssets.fixedTimeStep = .02f;
        Application_SettingsAssets.maxAngularVelocity = 6f;
        Application_SettingsAssets.behaviorType = Application_Settings.BehaviorType.A;
        
        Application_SettingsAssets.Bool1 = true;
        Application_SettingsAssets.Bool2 = true;
        Application_SettingsAssets.Bool3 = true;
        Application_SettingsAssets.Bool4 = true;
        Application_SettingsAssets.Bool5 = true;
        Application_SettingsAssets.units = Application_Settings.Units.cm;
        Application_SettingsAssets.uiType = Application_Settings.UIType.NGUI;
        
        Application_SettingsAssets.UIButtonSensitivity = 3f;
        Application_SettingsAssets.UIButtonGravity = 5f;
        Application_SettingsAssets.gyroSensitivity = 2f;
        
        Application_SettingsAssets.foldGeneralSettings = false;
        Application_SettingsAssets.foldControllerSettings = false;
        Application_SettingsAssets.foldUISettings = false;
        
        Application_SettingsAssets.foldOptimization = false;
    }



}

