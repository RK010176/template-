using UnityEngine;
using Common;

namespace Game
{

    // addes & remove NPC's from scane
    public class NpcsManager : MonoBehaviour, INpcsManager
    {        
        public Level _level { get; set; }
                
        public void AddNpcs()
        {
            for (int i = 0; i < _level.Npcs.Count ; i++)
            { 
                 GameObject _go = Instantiate(_level.Npcs[i].prefab, _level.Npcs[i].SpawnPoint,Quaternion.identity);
                _go.GetComponent<INpc>().NpcBehaviors = _level.Npcs[i];
            }
        }


        public void RemoveNPCs()
        {
            for (int i = 0; i < _level.Npcs.Count; i++)
            {
                Destroy(_level.Npcs[i].prefab);
            }
        
        }
        
    }
}