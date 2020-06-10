using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App
{
    public class Quit_State : IState
    {
        public void Enter()
        {
            Application.Quit();
        }

        public void Execute()
        {

        }

        public void Exit()
        {

        }


    }
}