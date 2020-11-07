using System;
using Common;
using UnityEngine;

namespace Game
{
    public class InventoryItem : MonoBehaviour
    {        
        [SerializeField] private Item _item;
        [SerializeField] private ItemsHandler _handler;
        public GameObject Interactables;

        private void Start()
        {            
            string InteracableOS = "Interacables/" + gameObject.transform.GetChild(0).name.Replace("(Clone)", "");            
            _item =  Resources.Load(InteracableOS) as Item;
            _handler = GameObject.Find("Inventory").GetComponent<ItemsHandler>();
            Interactables = GameObject.Find("Interactables");
            transform.parent = Interactables.transform;
        }

        private void OnTriggerEnter(Collider other)
        {
            // if player
            if (other.tag == "Player")
            {                
                _handler.PickItem(_item);
                // set inactive in scene                
                gameObject.SetActive(false);
            }
        }

    }

}