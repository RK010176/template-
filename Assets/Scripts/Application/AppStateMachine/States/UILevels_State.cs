using Common;
using UnityEngine;

namespace App
{
    public class UILevels_State : IState
    {

        [SerializeField]
        private string _sceneName = "UILevels";
        private ScenesManager _scenesManager;

        public void Enter()
        {
            // load menu UI       
            if (_scenesManager == null)
                _scenesManager = ScenesManager.Instance;
            _scenesManager.LoadScene(_sceneName);
        }

        public void Execute()
        {
        }

        public void Exit()
        {
            _scenesManager.UnLoadScene(_sceneName);

        }
    }
}