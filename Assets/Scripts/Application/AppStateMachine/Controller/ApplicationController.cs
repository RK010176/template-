using Common;
using UnityEngine;


namespace App
{
    public class ApplicationController : MonoBehaviour
    {
        
        private StateMachine _stateMachine;
        private Loading_State _loading_State;
        private InMenu_State _inMenu_State;
        private Playing_State _playing_State;
        private Quit_State _quit_State;
        private UIGameBurger_State _burger_State;
        private UILevelEnd_State _levelEnd_State;
        private ScreenFader _screenFader;

        [SerializeField] private Camera _camera;
        [SerializeField] private Light _light;
        [SerializeField] private IntVal _currentLevel;


        private void Awake()
        {
            _stateMachine = new StateMachine();
            _loading_State = new Loading_State();
            _inMenu_State = new InMenu_State();
            _playing_State = new Playing_State();
            _quit_State = new Quit_State();
            _burger_State = new UIGameBurger_State();
            _levelEnd_State = new UILevelEnd_State();
            _screenFader = FindObjectOfType<ScreenFader>();
        }
        private void OnEnable()
        {
            ApplicationEvents.OnLoadingFinish += LoadingFinish;
            ApplicationEvents.OnUnLoadingFinish += UnLoadingFinish;
            ApplicationEvents.OnDisableCamAndLight += DisableCamAndLight;
            ApplicationEvents.OnEnableCamAndLight += EnableCamAndLight;
            ApplicationEvents.OnLevelEnded += LevelEnded;
        }       
        private void Start()
        {
            _stateMachine.SetState(_loading_State);
            _stateMachine.Execute();
        }

        private void LoadingFinish(string scene)
        {
            switch (scene)
            {
                case "Loading":
                    _stateMachine.SetState(_inMenu_State);
                    break;

            }
        }
        private void DisableCamAndLight()
        {
            _camera.gameObject.SetActive(false);
            _light.gameObject.SetActive(false);

            _screenFader.FadeToFullOpacity();
        }
        private void EnableCamAndLight()
        {
            if (_camera != null)
                _camera.gameObject.SetActive(true);
            if(_light != null)
                _light.gameObject.SetActive(true);
        }
        private void UnLoadingFinish(string scene)
        {
            //Debug.Log(scene);        
        }


        #region Menu Buttons conneted to Events
        public void Play()
        {
            _stateMachine.SetState(_playing_State);
        }
        public void ShowBurgerMenu()
        {
            _stateMachine.SetState(_burger_State);
        }
        public void CloseBurgerMenu()
        {
            _stateMachine.Exit();
        }
        public void ShowMainMenu()
        {
            _stateMachine.SetState(_inMenu_State);
        }
        public void Quit()
        {
            _stateMachine.SetState(_quit_State);
        }
        #endregion

        private void LevelEnded(bool Win)
        {                        
            _stateMachine.SetState(_levelEnd_State);
            if (Win) _currentLevel.Value++;
            print(_currentLevel.Value +" val");
            
            // black backgroud
            _screenFader.FadeToBlack();
        }

    }
}