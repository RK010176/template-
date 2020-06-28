using System;
using Common;
using UnityEngine;

namespace Game
{
    public class InventoryItem : MonoBehaviour
    {        
        [SerializeField] private Item _item;
        [SerializeField] private ItemsHandler _handler;

        private void Start()
        {
            string InteracableOS = "Interacables/" + gameObject.transform.GetChild(0).name.Replace("(Clone)", "");            
             _item =  Resources.Load(InteracableOS) as Item;
            _handler = GameObject.Find("InventoryMamager").GetComponent<ItemsHandler>();
        }

        private void OnTriggerEnter(Collider other)
        {
            // if player
            if (other.tag == "Player")
            {
                //Debug.Log(name);                
                _handler.PickItem(_item);
                Destroy(gameObject);
            }
           
        }

        private void OnTriggerExit(Collider other)
        {
            
        }
    }

}