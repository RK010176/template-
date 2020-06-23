using Common;
using System.Collections.Generic;
using UnityEngine;

namespace App
{
    [CreateAssetMenu(fileName = "New Levels", menuName = "Levels/Levels")]
    public class Levels : ScriptableObject
    {        
        public List<Level> levels = new List<Level>();
    }
}