using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{

    public class ElementController : MonoBehaviour, IElement
    {
        public LevelGameElement GameElementSpecs { get; set; }

        void Start()
        {
            ProcessSpecs();
        }
        public void ProcessSpecs()
        {

        }

    }
}