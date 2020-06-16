using UnityEngine;
using UnityEngine.UI;
using Common;
public class Inventory : MonoBehaviour
{
    public GameObject[] GameObjects = new GameObject[numItemSlots];
    public Image[] itemImages = new Image[numItemSlots];
    public Item[] items = new Item[numItemSlots];    
    public const int numItemSlots = 4;
    public bool _inRoom = false;
    public void AddItem(Item itemToAdd)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = itemToAdd;
                GameObjects[i] = itemToAdd.GameObject;
                itemImages[i].sprite = itemToAdd.sprite;
                itemImages[i].enabled = true;
                return;
            }
        }
    }
    public void RemoveItem(Item itemToRemove)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == itemToRemove)
            {
                items[i] = null;
                PrefabHandler(GameObjects[i]);                
                GameObjects[i] = null;
                itemImages[i].sprite = null;
                itemImages[i].enabled = false;
                return;
            }
        }
    }


    private void PrefabHandler(GameObject Prefab)
    {
        if (_inRoom)
            Debug.Log("Item delivered !!");
        else
        {
            if (Prefab != null) Instantiate(Prefab);                        
        }
        
    }

}