using System.Collections.Generic;
using UnityEngine;
using System;

namespace Common
{
    [CreateAssetMenu(fileName = "New Level", menuName = "Levels/Level")]

    [Serializable]
    public class Level : ScriptableObject
    {
        public int LevelNumber;
        public List<Elements> Elements = new List<Elements>();
        public List<NpcBehaviors> Npcs = new List<NpcBehaviors>();
        public List<Conditions> Conditions = new List<Conditions>();
        public PlayerSpecs PlayerSpecs = new PlayerSpecs();
    }
    
    [Serializable]
    public class LevelObjects
    {
        public GameObject prefab;
    }

    [Serializable]
    public class NpcBehaviors : LevelObjects
    {        
        public float MovingSpeed;
        public float FastMovingSpeed;
        public float PatrollingRotation;
        public float PatrollingStandingPeriod;
        public float SearchRadius;
        public float AttackRadios;
        public Vector3 SpawnPoint;
        public List<AudioClip> Sounds;        
    }

    [Serializable]
    public class Elements : LevelObjects
    {        
        public Vector3 Position;
        public Vector3 Rotation;
    }

    [Serializable]
    public class Conditions
    {

    }

    [Serializable]
    public class PlayerSpecs
    {
        public float MovingSpeed;                                       
        public Vector3 SpawnPoint;
        public List<AudioClip> Sounds;
    }

}
