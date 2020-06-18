using UnityEngine;
using Common;
using System.Collections.Generic;

namespace Game
{
    public class ElementsManager : MonoBehaviour, IElements
    {
        public List<LevelGameElement> Elements { get; set; }

        public void AddElements()
        {
            for (int i = 0; i < Elements.Count; i++)
            {
                GameObject _go = Instantiate(Elements[i].prefab,
                                             Elements[i].Position, 
                                             Quaternion.Euler(Elements[i].Rotation));
                _go.GetComponent<IElement>().GameElementSpecs = Elements[i];
            }
        }

        public void RemoveElements()
        {
            for (int i = 0; i < Elements.Count; i++)
            {
                Destroy(Elements[i].prefab);
            }
        }
    }
}
