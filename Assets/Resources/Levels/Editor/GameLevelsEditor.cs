using UnityEngine;
using UnityEditor;
using App;
using Common;

[CustomEditor(typeof(Levels))]
public class GameLevelsEditor : Editor
{
    Levels levels;
    Color originalGUIColor;
    Vector2 scrollPos;

    private bool FoldLevels = true;


    public override void OnInspectorGUI()
    {
        #region Header

        // top Header
        originalGUIColor = GUI.color;
        EditorGUIUtility.labelWidth = 250;
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Game Levels", EditorStyles.boldLabel);
        GUI.color = new Color(.75f, 1f, .75f);

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

        // fold GeneralSettings section
        FoldLevels = EditorGUILayout.Foldout(FoldLevels, "Levels");

        if (FoldLevels)
        {
            // outer box open
            EditorGUILayout.BeginVertical(GUI.skin.box);

            GUI.color = new Color(.75f, 1f, .75f);
            GUILayout.Label("General Settings", EditorStyles.boldLabel);

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
        GUI.color = new Color(.75f, 1f, .75f);
        GUI.color = new Color(.5f, 1f, 1f, 1f);

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

        GUI.color = originalGUIColor;
        EditorGUILayout.LabelField("Application", EditorStyles.centeredGreyMiniLabel, GUILayout.MaxHeight(50f));
        #endregion

        EditorGUILayout.EndVertical();
    }

    private void Draw(SerializedProperty list)
    {
        EditorGUILayout.PropertyField(list);

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
            level.name = "New Level";
            levels.levels.Add(level);

            AssetDatabase.AddObjectToAsset(level, levels);
            AssetDatabase.SaveAssets();

        }

        //if (GUILayout.Button("Add Item Quest", GUILayout.ExpandWidth(false)))
        //{
        //    if (!list.isExpanded)
        //        list.isExpanded = true;

        //    ItemQuest itemquest = ScriptableObject.CreateInstance<ItemQuest>();
        //    itemquest.name = "New ItemQuest";
        //    questLog.Quests.Add(itemquest);

        //    AssetDatabase.AddObjectToAsset(itemquest, questLog);
        //    AssetDatabase.SaveAssets();

        //}
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








