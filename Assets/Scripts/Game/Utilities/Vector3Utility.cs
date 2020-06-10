using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Game
{
    public class Vector3Utility
    {
        private static List<Vector3> _vector3sFood = new List<Vector3>();
        public static Vector3 GetClosest(Vector3 from, Vector3 target)
        {
            _vector3sFood.Add(target);
            if (_vector3sFood.Count > 0)
            {
                var orderedByProximity = _vector3sFood.OrderBy(c => Vector3.Distance(from, target)).ToArray();
                return orderedByProximity[0];
            }
            return target;
        }

        public static void Clear()
        {
            _vector3sFood.Clear();
        }
    }

}


