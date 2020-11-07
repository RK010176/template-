using UnityEngine;
using Common;
public class ItemsHandler : MonoBehaviour
{    
    private Inventory _inventory;
    public void PickItem(Item item)
    {
        _inventory = GameObject.Find("Inventory").GetComponent<Inventory>();        
        _inventory.AddItem(item);
    }

    public void DropItem()
    {
        
    }


}
