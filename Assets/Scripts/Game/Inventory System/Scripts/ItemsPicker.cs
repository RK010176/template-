using Common;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Game
{
    public class ItemsPicker : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Item Item;
        [SerializeField] private ItemsHandler Handler;

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log(name);
            Handler = GameObject.Find("ItemsMenager").GetComponent<ItemsHandler>();
            Handler.PickItem(Item);
            Destroy(gameObject);
        }
    }
}