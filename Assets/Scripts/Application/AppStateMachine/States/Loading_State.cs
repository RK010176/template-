using Common;
using UnityEngine;

namespace App
{
    public class Loading_State : IState
    {
        [SerializeField]
        private string _sceneName = "Loading";
        private ScenesManager _scenesManager;

        public void Enter()
        {
            //Debug.Log("Enter - Loading_State");

            // load scene
            if (_scenesManager == null)
                _scenesManager = ScenesManager.Instance;
            _scenesManager.LoadScene(_sceneName);
        }

        public void Execute()
        {
            //Debug.Log("Execute - Loading_State");

        }

        public void Exit()
        {
            _scenesManager.UnLoadScene(_sceneName);
            //Debug.Log("Exit - Loading_State");
        }


    }
}