using Common;
using UnityEngine;


namespace App
{
    public class ApplicationController : MonoBehaviour
    {

        private StateMachine _machine;
        private Loading_State _loading_State;
        private InMenu_State _inMenu_State;
        private Playing_State _playing_State;
        private Quit_State _Quit_State;
        private UIGameBurger_State _burger_State;

        [SerializeField] private Camera _camera;
        [SerializeField] private Light _light;

        private void OnEnable()
        {
            ApplicationEvents.OnLoadingFinish += LoadingFinish;
            ApplicationEvents.OnUnLoadingFinish += UnLoadingFinish;
            ApplicationEvents.OnDisableCamAndLight += DisableCamAndLight;
            ApplicationEvents.OnEnableCamAndLight += EnableCamAndLight;
        }

        private void Awake()
        {
            _machine = new StateMachine();
            _loading_State = new Loading_State();
            _inMenu_State = new InMenu_State();
            _playing_State = new Playing_State();
            _Quit_State = new Quit_State();
            _burger_State = new UIGameBurger_State();
        }

        private void Start()
        {
            _machine.SetState(_loading_State);
            _machine.Execute();
        }

        private void LoadingFinish(string scene)
        {
            switch (scene)
            {
                case "Loading":
                    _machine.SetState(_inMenu_State);
                    break;

            }
        }

        private void DisableCamAndLight()
        {
            _camera.gameObject.SetActive(false);
            _light.gameObject.SetActive(false);
        }
        private void EnableCamAndLight()
        {
            _camera.gameObject.SetActive(true);
            if(_light != null)
                _light.gameObject.SetActive(true);
        }

        private void UnLoadingFinish(string scene)
        {
            //Debug.Log(scene);        
        }


        // Menu Buttons conneted to Events
        public void Play()
        {
            _machine.SetState(_playing_State);
        }
        public void ShowBurgerMenu()
        {
            _machine.SetState(_burger_State);
        }
        public void CloseBurgerMenu()
        {
            _machine.Exit();
        }
        public void ShowMainMenu()
        {
            _machine.SetState(_inMenu_State);
        }

        public void Quit()
        {
            Debug.Log("Quit");
            _machine.SetState(_Quit_State);
        }

    }
}