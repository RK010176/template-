using Common;
using UnityEngine;

namespace App
{
    public class InMenu_State : IState
    {
        [SerializeField]
        private string _sceneName = "UIMenu";
        private ScenesManager _scenesManager;

        public void Enter()
        {
            // load menu UI       
            if (_scenesManager == null)
                _scenesManager = ScenesManager.Instance;
            _scenesManager.LoadScene(_sceneName);
            _scenesManager.UnLoadScenesExept(_sceneName);
        }

        public void Execute()
        {

        }

        public void Exit()
        {
            // UnLoad menu UI
            _scenesManager.UnLoadScene(_sceneName);
            //Debug.Log("Exit - Loading_State");
        }


    }
}