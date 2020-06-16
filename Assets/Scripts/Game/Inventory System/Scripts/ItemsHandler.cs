using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;


public class ItemsHandler : MonoBehaviour
{    
    public Inventory Inventory;
    public void PickItem(Item item)
    {
        Inventory.AddItem(item);
    }

    public void DropItem()
    {
        
    }


}
