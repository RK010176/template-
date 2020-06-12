using Game;
using Common;
using UnityEngine;
using System.Collections.Generic;

namespace App
{ 
    public class Game_Manager : MonoBehaviour
    {
        private Levels Levels;

        private INpcsManager _npcsManager;
        private IElements _environment;
        private PlayerManager _playerManager;
        private Level _currentLevel;
        private int _currentLevelNumber;
        private StateMachine _stateMachine = new StateMachine();

        private void Awake()
        {            
            ApplicationEvents.DisableCamAndLight();
            Levels = Resources.Load<Levels>("Levels/GameLevels");
            _npcsManager = GetComponent<INpcsManager>();
            _npcsManager._level = Levels.levels[0];
            _environment = GetComponent<IElements>();
            _environment._level = Levels.levels[0];
            
        }



        private void Start()
        {
            ScenesManager.Instance.SetSceneToActiveScene("Game");

            _playerManager = GetComponent<PlayerManager>();
            _playerManager.Level = Levels.levels[0];

            _npcsManager.AddNpcs();
            _environment.AddElements();
            // TODO : conditions
            _playerManager.AddPlayer();
        }

        private void OnDisable()
        {
            ApplicationEvents.EnableCamAndLight();
        }
    }
}