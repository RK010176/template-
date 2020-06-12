using UnityEngine;
using System;


//1. create directory -> Resources/Application_Settings/
//2. remove comment -> //[CreateAssetMenu(   to [CreateAssetMenu(
//3. in Application_Settings folder right click -> Create -> Create Application_Settings asset file
//4. restore comment -> [CreateAssetMenu(  to //[CreateAssetMenu(


//[CreateAssetMenu(fileName = "Application_Settings", menuName = "Create Application_Settings asset file")]

[Serializable]
public class Application_Settings : ScriptableObject
{
    #region singleton
    public static Application_Settings instance;
    public static Application_Settings Instance
    {
        get
        {
            if (instance == null) instance = Resources.Load("Application_Settings/Application_Settings") as Application_Settings;
            return instance;
        }
    }
    #endregion


    // Used for folding sections of Settings Asset file
    public bool foldGeneralSettings = false;
    public bool foldControllerSettings = false;
    public bool foldUISettings = false;
    public bool foldOptimization = false;
    public bool foldTagsAndLayers = false;


    // 1.General setting    
    public bool overrideFixedTimeStep = true; //
    [Range(.005f, .06f)]
    public float fixedTimeStep = .02f; //
    [Range(.5f, 20f)]
    public float maxAngularVelocity = 6; //           
    // Behavior Types
    public BehaviorType behaviorType; //
    public enum BehaviorType { A, B, C, D, E, F }
    public bool useFixedWheelColliders = true; //


    // 2.Main Controller Settings
    // Controller Type
    public int toolbarSelectedIndex;//
    public ControllerType controllerType;//
    public enum ControllerType { Headset, Mobile, Custom }

    public bool Bool1 = true;//
    public bool Bool2 = true;//
    public bool Bool3 = true;//
    public bool Bool4 = true;//
    public bool Bool5 = true;//

    public Units units;
    public enum Units { cm, inches }


    // Mobile controller - buttons and accelerometer sensitivity
    public float UIButtonSensitivity = 3f; //
    public float UIButtonGravity = 5f;//
    public float gyroSensitivity = 2f;//



    // 4.UI    
    public UIType uiType;
    public enum UIType { UGUI, NGUI, None }


    // Resources
    public GameObject A;
    public GameObject B;
    public GameObject C;
    public GameObject D;
    public GameObject E;

}
