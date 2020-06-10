using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App
{
    public class EditorInput : MonoBehaviour, IPlatformInput
    {
        public bool Backword()
        {
            return Input.GetKey(KeyCode.S);
        }
        public bool Forward()
        {
            return Input.GetKey(KeyCode.W);
        }
        public bool Down()
        {
            return Input.GetKey(KeyCode.DownArrow);
        }
        public bool Up()
        {
            return Input.GetKey(KeyCode.UpArrow);
        }
        public bool FireWeapons()
        {
            return Input.GetButton("Fire1");
        }
        public bool Right()
        {
            return Input.GetKey(KeyCode.RightArrow);
        }
        public bool Left()
        {
            return Input.GetKey(KeyCode.LeftArrow);
        }

        public bool PanLeft()
        {
            return Input.GetKey(KeyCode.Q);
        }
        public bool PanRight()
        {
            return Input.GetKey(KeyCode.E);
        }
        public bool SpaceBar()
        {
            return Input.GetKey(KeyCode.Space);
        }
    }
}