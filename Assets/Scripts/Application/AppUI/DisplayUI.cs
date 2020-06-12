using UnityEngine;
using UnityEngine.UI;

namespace App
{
    public class DisplayUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _group;

        public void DisplayGroup()
        {
            _group.alpha = 1;
            _group.interactable = true;
        }
        public void HideGroup()
        {
            _group.alpha = 0;
            _group.interactable = false;
        }

    }
}
