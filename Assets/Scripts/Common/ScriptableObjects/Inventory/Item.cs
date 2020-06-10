using UnityEngine;
using UnityEngine.UI;

// tutorial-  https://www.youtube.com/watch?time_continue=829&v=9zXvIEvyZ_c&feature=emb_logo

namespace Common
{
    [CreateAssetMenu(fileName = "New Item", menuName = "core/Item")]
    public class Item : ScriptableObject
    {
        public Sprite sprite;
        public GameObject GameObject;
    }
}
