using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDisposer : MonoBehaviour
{
    public int ID;    
    private Inventory inventory;    // Reference to the Inventory component.   

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    public void Drop()
    {           
        inventory.RemoveItem(inventory.items[ID]);
    }
}
