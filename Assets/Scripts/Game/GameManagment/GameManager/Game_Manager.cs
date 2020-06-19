using Game;
using Common;
using UnityEngine;

namespace App
{ 
    public class Game_Manager : MonoBehaviour
    {
        private Levels _levels;
        private PlayerManager _playerManager;
        private InteractablesManager _interactablesManager;
        private IElements _elementsManager;
        private INpcsManager _npcsManager;
        private Level _currentLevel;
        private int _currentLevelNumber = 0;
        //private StateMachine _stateMachine = new StateMachine();

        private void Awake() 
        {            
            ApplicationEvents.DisableCamAndLight();
            _levels = Resources.Load<Levels>("GameLevels/GameLevels");
            
            _playerManager = GetComponent<PlayerManager>();
            _playerManager.Level = _levels.levels[_currentLevelNumber];

            _interactablesManager = GetComponent<InteractablesManager>(); 
            _interactablesManager.Interactables = _levels.levels[_currentLevelNumber].Interactables;

            _elementsManager = GetComponent<IElements>();
            _elementsManager.Elements = _levels.levels[_currentLevelNumber].Elements;

            _npcsManager = GetComponent<INpcsManager>();
            _npcsManager._level = _levels.levels[_currentLevelNumber];                        
        }
        private void Start() // init npc's, env, player
        {            
            ScenesManager.Instance.SetSceneToActiveScene("Game"); //  Instantiate Gameobjects in this scene

            _playerManager.AddPlayer();

            _interactablesManager.AddInteactables();

            _elementsManager.AddElements();
            
            _npcsManager.AddNpcs();           
        }

        private void OnDisable()
        {
            ApplicationEvents.EnableCamAndLight();
        }


    }
}