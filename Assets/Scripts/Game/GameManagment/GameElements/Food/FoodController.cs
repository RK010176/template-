﻿using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    public class FoodController : MonoBehaviour, IElement
    {
        public Elements Elements { get; set; }

        void Start()
        {
            GetLavelData();            
        }

        public void GetLavelData()
        {
            
        }

    }
}