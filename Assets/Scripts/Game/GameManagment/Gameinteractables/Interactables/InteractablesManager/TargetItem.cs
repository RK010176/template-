using UnityEngine;

namespace Game
{
    public class TargetItem : MonoBehaviour
    {
        private string _itemTag;
        private void Start()
        {
            _itemTag = gameObject.transform.GetChild(0).tag;            
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == _itemTag)
            {
                // TODO: condition 
                //                
            }
        }
    }

}