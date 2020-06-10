using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App
{
    public class UIGameBurger_State : IState
    {
        [SerializeField]
        private string _uIsceneName = "UIGameBurger";

        private ScenesManager _scenesManager;

        public void Enter()
        {
            // load menu UI       
            if (_scenesManager == null)
                _scenesManager = ScenesManager.Instance;
            _scenesManager.LoadScene(_uIsceneName);
        }

        public void Execute()
        {
            //TODO: inject game object
        }

        public void Exit()
        {
            _scenesManager.UnLoadScene(_uIsceneName);
        }
    }
}