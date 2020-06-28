using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDisposer : MonoBehaviour
{
    public int ID;    
    private Inventory inventory;    // Reference to the Inventory component.   

    public void Drop()
    {
        //Debug.Log("drop");
        inventory = FindObjectOfType<Inventory>();
        inventory.RemoveItem(inventory.items[ID]);
    }
}
