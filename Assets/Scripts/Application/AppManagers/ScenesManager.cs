using Common;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace App
{
    public class ScenesManager : MonoBehaviour
    {

        public FloatVal LoadingProcessValue;
        private string scene;

        private static ScenesManager _instance;
        public static ScenesManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ScenesManager();
                }
                return _instance;
            }
        }

        private void Awake()
        {
            _instance = this;
        }

        public void LoadScene(string scene)
        {
            StartCoroutine(BeginLoad(scene));
        }

        AsyncOperation operation;
        private IEnumerator BeginLoad(string scene)
        {
            this.scene = scene;
            operation = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

            yield return operation;

            while (operation != null && !operation.isDone)
            {
                // Update LoadingProcessValue ScriptableObject
                LoadingProcessValue.Value = operation.progress;
                //Debug.Log(LoadingProcessValue.Value);
                yield return null;
            }

            if (operation != null && operation.isDone)
            {
                operation.completed += LoadingComplited;
                operation = null;
                yield break;
            }
        }

        private void LoadingComplited(AsyncOperation obj)
        {
            // notify about finish Loading          
            ApplicationEvents.LoadingFinish(scene);
            operation.completed -= LoadingComplited;
        }

        public void UnLoadScene(string scene)
        {
            if (IsActiveScene(scene))
                SceneManager.UnloadSceneAsync(scene);
            //notify about finish UnLoading
            ApplicationEvents.UnLoadingFinish(scene);
        }

        public void UnLoadScenesExept(string scene)
        {
            int countLoaded = SceneManager.sceneCount;
            Scene[] loadedScenes = new Scene[countLoaded];

            for (int i = 1; i < countLoaded; i++)
            {
                loadedScenes[i] = SceneManager.GetSceneAt(i);
                if (loadedScenes[i].name != scene)
                {
                    SceneManager.UnloadSceneAsync(loadedScenes[i]);
                }
            }
            //notify about finish UnLoading
            ApplicationEvents.UnLoadingFinish(scene);
        }

        // verifay if scene is already loaded
        private bool IsActiveScene(string scene)
        {
            int countLoaded = SceneManager.sceneCount;
            Scene[] loadedScenes = new Scene[countLoaded];

            for (int i = 0; i < countLoaded; i++)
            {
                loadedScenes[i] = SceneManager.GetSceneAt(i);
                if (loadedScenes[i].name == scene)
                {
                    return true;
                }
            }
            return false;
        }

        public void SetSceneToActiveScene(string scene)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene));
        }

    }
}