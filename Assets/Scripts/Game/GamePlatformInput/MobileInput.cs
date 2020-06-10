using UnityEngine;
using UnityEngine.UI;
using Game;
using App;

public class MobileInput : MonoBehaviour, IPlatformInput
    {
        private Joystick Joystick;
        private TouchField TouchField;
        private GameObject Button;
        private GameObject UpButton;
        private GameObject DownButton;

        void Awake()
        {
            Joystick = FindObjectOfType<Joystick>();
            TouchField = FindObjectOfType<TouchField>();
            Button = GameObject.Find("FireButton"); //FindObjectOfType<Button>();
                                                    //UpButton = GameObject.Find("UpButton");
                                                    //DownButton = GameObject.Find("DownButton");

        }


        public bool Backword()
        {
            return false; // DownButton.GetComponent<_Button>().Pressed;
        }
        public bool Forward()
        {
            return false;// UpButton.GetComponent<_Button>().Pressed;
        }
        public bool Left()
        {
            return Joystick.AxisNormalized.x < -0.5f ? true : false;
        }
        public bool Right()
        {
            return Joystick.AxisNormalized.x > 0.5f ? true : false;
        }

        public bool Up()
        {
            return Joystick.AxisNormalized.y > 0.5f ? true : false;
        }

        public bool Down()
        {
            return Joystick.AxisNormalized.y < -0.5f ? true : false;
        }

        public bool PanLeft()
        {
            return Input.acceleration.x < -0.05 ? true : false;
        }

        public bool PanRight()
        {
            return Input.acceleration.x > 0.05 ? true : false; throw new System.NotImplementedException();
        }

        public bool FireWeapons()
        {
            return Button.GetComponent<_Button>().Pressed;
        }

        public bool SpaceBar()
        {
            return false;// set button..
        }

    }

