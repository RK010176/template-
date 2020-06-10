using Common;
using UnityEngine;

namespace App
{
    public class Playing_State : IState
    {
        [SerializeField]
        private string _sceneName = "Game";
        private string _uIsceneName = "UIGame";

        private ScenesManager _scenesManager;

        public void Enter()
        {
            // load menu UI       
            if (_scenesManager == null)
                _scenesManager = ScenesManager.Instance;
            _scenesManager.LoadScene(_sceneName);
            _scenesManager.LoadScene(_uIsceneName);
        }

        public void Execute()
        {
            //TODO: inject game object
        }

        public void Exit()
        {

        }


    }
}