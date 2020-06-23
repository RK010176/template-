using System;

namespace App
{

    public class ApplicationEvents
    {
        #region singelton
        private static ApplicationEvents _instance;
        public static ApplicationEvents Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ApplicationEvents();
                }
                return _instance;
            }
        }
        #endregion


        public static event Action OnDisableCamAndLight;
        public static void DisableCamAndLight()
        { OnDisableCamAndLight?.Invoke(); }

        public static event Action OnEnableCamAndLight;
        public static void EnableCamAndLight()
        { OnEnableCamAndLight?.Invoke(); }


        public delegate void loadingFinish(string scene);
        public static event loadingFinish OnLoadingFinish;
        static public void LoadingFinish(string scene)
        {
            if (OnLoadingFinish != null)
                OnLoadingFinish(scene);
        }

        public delegate void unLoadingFinish(string scene);
        public static event unLoadingFinish OnUnLoadingFinish;
        static public void UnLoadingFinish(string scene)
        {
            if (OnUnLoadingFinish != null)
                OnUnLoadingFinish(scene);
        }


        public delegate void levelEnded(bool Win);
        public static event levelEnded OnLevelEnded;
        static public void LevelEnded(bool Win)
        {
            if (OnLevelEnded != null)
                OnLevelEnded(Win);
        }

    }
}
