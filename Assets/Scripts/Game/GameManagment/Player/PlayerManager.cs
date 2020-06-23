using UnityEngine;
using Common;
using System;

namespace Game
{
    public class PlayerManager : MonoBehaviour 
    {
        public Level Level { get; set; }
        
        public void AddPlayer()
        {
            try {
                GameObject _go = Instantiate(Level.PlayerSpecs.prefab, Level.PlayerSpecs.SpawnPoint, Quaternion.identity);
                _go.GetComponent<PlayerController>().PlayerSpecs = Level.PlayerSpecs;
            }
            catch (Exception ex)
            {
                Debug.Log(ex +" - verify player Level setting!  ");
            }
        }
    }
}