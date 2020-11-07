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

    private GameObject Player;
    
    public void AddItem(Item itemToAdd)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = itemToAdd;
                GameObjects[i] = itemToAdd.GameObject;
                try
                {itemImages[i].sprite = itemToAdd.sprite;}
                catch (System.Exception)
                {print("#drag item image to inventory slot in inspector#");}                 
                itemImages[i].enabled = true;
                return;
            }
        }
    }

    // remove item from Item Ui list and position it in scene
    public void RemoveItem(Item itemToRemove)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == itemToRemove)
            {
                items[i] = null;
                ActivateInteractable(GameObjects[i]);                
                GameObjects[i] = null;
                itemImages[i].sprite = null;
                itemImages[i].enabled = false;
                return;
            }
        }
    }
    private void ActivateInteractable(GameObject Prefab)
    {
        if (_inRoom)
            Debug.Log("Item delivered !!");
        else
        {
            if (Prefab != null)
            {
                Player = GameObject.Find("Player");

                // find inactive Interactable
                Transform[] trans = GameObject.Find("Interactables").GetComponentsInChildren<Transform>(true);
                foreach (Transform tr in trans)
                {
                    if (!tr.gameObject.activeInHierarchy)
                    {
                        if (tr.gameObject.name == Prefab.name + "(Clone)")
                        {
                            var inter = tr.transform.parent.gameObject;
                            // set interactable position in front of player                        
                            inter.transform.position = Player.transform.position + Player.transform.forward;// +new Vector3(1,0,1);                                                
                            inter.SetActive(true);
                            break;
                        }
                    }
                                        
                }                
            }
        }        
    }

}