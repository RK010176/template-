using UnityEngine;
using UnityEngine.Events;

namespace Common
{
    public class Game_EventListener : MonoBehaviour
    {
        [Tooltip("Event to register with.")]
        public Game_Event Event;

        [Tooltip("Response to invoke when Event is raised.")]
        public UnityEvent Response;

        private void OnEnable()
        {
            Event.RegisterListener(this);            
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised()
        {
            Response.Invoke();
        }
    }
}