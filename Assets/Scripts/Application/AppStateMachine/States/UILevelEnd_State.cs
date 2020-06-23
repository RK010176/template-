using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace App
{
    public class UILevelEnd_State : IState
    {
        [SerializeField]
        private string _uIsceneName = "UILevelEnd";
        private string _uIGame = "UIGame";       
        private ScenesManager _scenesManager;

        public void Enter()
        {
            // load UI  LevelEnd
            if (_scenesManager == null)
                _scenesManager = ScenesManager.Instance;
            _scenesManager.LoadScene(_uIsceneName);
           // _scenesManager.UnLoadScene(_uIGame);
        }


        public void Execute()
        {
                        
        }

        public void Exit()
        {
            _scenesManager.UnLoadScene(_uIsceneName);
        }
    }
}
