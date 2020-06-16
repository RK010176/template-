using Game;
using Common;
using UnityEngine;

namespace App
{ 
    public class Game_Manager : MonoBehaviour
    {
        private Levels Levels;
        private INpcsManager _npcsManager;
        private IElements _environment;
        private PlayerManager _playerManager;
        private Level _currentLevel;
        private int _currentLevelNumber = 0;
        //private StateMachine _stateMachine = new StateMachine();

        private void Awake() 
        {            
            ApplicationEvents.DisableCamAndLight();
            Levels = Resources.Load<Levels>("GameLevels/GameLevels");
            _npcsManager = GetComponent<INpcsManager>();
            _npcsManager._level = Levels.levels[_currentLevelNumber];
            _environment = GetComponent<IElements>();
            _environment._level = Levels.levels[_currentLevelNumber];
            _playerManager = GetComponent<PlayerManager>();
            _playerManager.Level = Levels.levels[_currentLevelNumber];
        }
        private void Start() // init npc's, env, player
        {            
            ScenesManager.Instance.SetSceneToActiveScene("Game"); //  Instantiate Gameobjects in this scene
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