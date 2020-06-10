using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    /// <summary>
    /// Put it on any Image UI Element
    /// </summary>
    public class _Button : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        [HideInInspector]
        public bool Pressed;

        
        public void OnPointerDown(PointerEventData eventData)
        {
            Pressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Pressed = false;
        }
    }

}