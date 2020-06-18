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
        public PlayerSpecs PlayerSpecs = new PlayerSpecs();
        public List<LevelInteractable> Interactables = new List<LevelInteractable>();        
        public List<LevelGameElement> Elements = new List<LevelGameElement>();
        public List<NpcBehaviors> Npcs = new List<NpcBehaviors>();
    }
    
    [Serializable]
    public class LevelGameObject
    {
        public GameObject prefab;
    }

    [Serializable]
    public class PlayerSpecs : LevelGameObject
    {
        public float MovingSpeed;
        public Vector3 SpawnPoint;
        public List<AudioClip> Sounds;
        public float Health;
        public float CurrentHealth;
    }

    [Serializable]
    public class LevelInteractable
    {
        public Actions Action;
        public GameObject InteractableShape;
        public Vector3 Position;
        public Vector3 Rotation;
        public Vector3 Scale;
        public LevelGameElement Element;
    }
    
    [Serializable]
    public class LevelGameElement : LevelGameObject
    {
        public Vector3 Position;
        public Vector3 Rotation;              
    }

    [Serializable]
    public class NpcBehaviors : LevelGameObject
    {
        public float MovingSpeed;
        public float FastMovingSpeed;
        public float PatrollingRotation;
        public float PatrollingStandingPeriod;
        public float SearchRadius;
        public float AttackRadios;
        public Vector3 SpawnPoint;
        public List<AudioClip> Sounds;
        public float Health;
    }


    #region enums
    public enum InteractableShape { Box, Cylinder }
    public enum Actions { InventoryItem, TargetItem}
    #endregion

}
