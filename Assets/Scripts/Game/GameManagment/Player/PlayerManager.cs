using UnityEngine;
using Common;

namespace Game
{
    public class PlayerManager : MonoBehaviour 
    {
        public Level Level { get; set; }
        
        public void AddPlayer()
        {
            GameObject _go = Instantiate(Level.PlayerSpecs.prefab, Level.PlayerSpecs.SpawnPoint, Quaternion.identity);
            _go.GetComponent<PlayerController>().PlayerSpecs = Level.PlayerSpecs;
        }
    }
}