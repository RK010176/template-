using UnityEngine;
using Common;

namespace Game
{
    public class ElementsManager : MonoBehaviour, IElements
    {
        public Level _level { get; set; }

        public void AddElements()
        {
            for (int i = 0; i < _level.Elements.Count; i++)
            {
                GameObject _go = Instantiate(_level.Elements[i].prefab, 
                                             _level.Elements[i].Position, 
                                             Quaternion.Euler(_level.Elements[i].Rotation));
                _go.GetComponent<IElement>().Elements = _level.Elements[i];
            }
        }

        public void RemoveElements()
        {
            for (int i = 0; i < _level.Elements.Count; i++)
            {
                Destroy(_level.Elements[i].prefab);
            }

        }

    }
}
