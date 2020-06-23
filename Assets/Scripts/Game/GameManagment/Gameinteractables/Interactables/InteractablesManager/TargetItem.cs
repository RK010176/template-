using UnityEngine;
using App;

namespace Game
{
    public class TargetItem : MonoBehaviour
    {        
        private Animator _animator;
        private bool _interaction = false;
        
        private void OnTriggerEnter(Collider other)
        {            
            if (other.tag == "Player")
            {                
                _interaction = true;
                _animator = other.gameObject.GetComponent<Animator>();                
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                _interaction = false;
            }
        }

        private void Update()
        {
            if (_interaction)
            {
                if (_animator.GetBool("Eat"))
                {
                    _interaction = false;
                    ApplicationEvents.LevelEnded(true);
                                       
                }
                
            }
        }

    }
}