using UnityEngine;
using UnityEditor;
using App;
using Common;

[CustomEditor(typeof(Levels))]
public class GameLevelsEditor : Editor
{
    private Levels levels;
    private Color originalGUIColor;
    private Vector2 scrollPos;

    private bool FoldLevels = true;


    public override void OnInspectorGUI()
    {
        originalGUIColor = GUI.color; // save default color

        #region Header

        // top Header
        EditorGUILayout.BeginVertical(GUI.skin.button);
        GUI.color = new Color(1f, 1f, 0.4f);
        EditorGUILayout.LabelField("Levels", EditorStyles.whiteLargeLabel, GUILayout.MaxHeight(20f));
        //GUI.color = originalGUIColor;
        EditorGUILayout.EndVertical();

        
        EditorGUIUtility.labelWidth = 250;
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Game Levels Window", EditorStyles.boldLabel);
        //GUI.color = new Color(.75f, 1f, .75f);
        GUI.color = new Color(1f, 1f, 0f);

        // Header's paragraph
        EditorGUILayout.LabelField("This editor will keep update necessary .asset files in your app. Don't change directory of the ''Resources/Levels''.", EditorStyles.helpBox);
        GUI.color = originalGUIColor;
        EditorGUILayout.Space();

        #endregion

        EditorGUI.indentLevel++;

        // Open ScrollView ------------------------------------------------------------------------------------
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, false);


        #region FoldLevels - section
        //___________________________________________________________________________________________________        
        FoldLevels = EditorGUILayout.Foldout(FoldLevels, "Levels");

        if (FoldLevels)
        {
            // outer box open
            EditorGUILayout.BeginVertical(GUI.skin.box);

            GUI.color = new Color(1f, 0.8f, 0f);
            GUILayout.Label("Set Levels", EditorStyles.boldLabel);

            GUI.color = originalGUIColor;
            #region Levels
            levels = (Levels)target;
            SerializedObject serializedObject = new SerializedObject(levels);
            serializedObject.Update();

            SerializedProperty serializedPropertyList = serializedObject.FindProperty("levels");
            Draw(serializedPropertyList);
            #endregion

            // outer box close
            EditorGUILayout.EndVertical();
        }
        #endregion

        EditorGUILayout.EndScrollView();
        // Close ScrollView -----------------------------------------------------------------------------------


        #region footer
        EditorGUILayout.BeginVertical(GUI.skin.button);
        GUI.color = new Color(1f, 0.7f, 0.4f);
        //GUI.color = new Color(.5f, 1f, 1f, 1f);

        // reset button
        if (GUILayout.Button("Reset To Defaults"))
        {
            //ResetToDefaults();
            Debug.Log("Resetted To Defaults!");
        }
        // pdf button
        if (GUILayout.Button("Browse Site"))
        {
            string url = "http://www.google.com";
            Application.OpenURL(url);
        }

        //GUI.color = originalGUIColor;
        
        #endregion

        EditorGUILayout.EndVertical();
    }

    private void Draw(SerializedProperty list)
    {
        //EditorGUILayout.PropertyField(list);

        if (list.isExpanded)
        {
            EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size"));
            ShowElements(list, false);
        }

        if (GUILayout.Button("Add Level", GUILayout.ExpandWidth(false)))
        {
            if (!list.isExpanded)
                list.isExpanded = true;

            Level level = ScriptableObject.CreateInstance<Level>();
            level.name = "Level"+(list.arraySize+1).ToString();
            levels.levels.Add(level);

            AssetDatabase.AddObjectToAsset(level, levels);
            AssetDatabase.SaveAssets();

        }
        
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

            if (GUILayout.Button("Delete", EditorStyles.miniButtonRight, GUILayout.ExpandWidth(false)))
            {
                Object.DestroyImmediate(list.GetArrayElementAtIndex(i).objectReferenceValue, true);
                levels.levels.RemoveAt(i);
                AssetDatabase.SaveAssets();
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}








