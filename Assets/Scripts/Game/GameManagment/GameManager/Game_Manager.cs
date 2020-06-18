using Game;
using Common;
using UnityEngine;

namespace App
{ 
    public class Game_Manager : MonoBehaviour
    {
        private Levels Levels;
        private PlayerManager _playerManager;
        private InteractablesManager _interactablesManager;
        private IElements _environment;
        private INpcsManager _npcsManager;
        private Level _currentLevel;
        private int _currentLevelNumber = 0;
        //private StateMachine _stateMachine = new StateMachine();

        private void Awake() 
        {            
            ApplicationEvents.DisableCamAndLight();
            Levels = Resources.Load<Levels>("GameLevels/GameLevels");
            
            _playerManager = GetComponent<PlayerManager>();
            _playerManager.Level = Levels.levels[_currentLevelNumber];

            _interactablesManager = GetComponent<InteractablesManager>(); 
            _interactablesManager.Interactables = Levels.levels[_currentLevelNumber].Interactables;

            _environment = GetComponent<IElements>();
            _environment.Elements = Levels.levels[_currentLevelNumber].Elements;

            _npcsManager = GetComponent<INpcsManager>();
            _npcsManager._level = Levels.levels[_currentLevelNumber];                        
        }
        private void Start() // init npc's, env, player
        {            
            ScenesManager.Instance.SetSceneToActiveScene("Game"); //  Instantiate Gameobjects in this scene

            _playerManager.AddPlayer();

            _interactablesManager.AddInteactables();

            _environment.AddElements();
            
            _npcsManager.AddNpcs();           
        }

        private void OnDisable()
        {
            ApplicationEvents.EnableCamAndLight();
        }


    }
}