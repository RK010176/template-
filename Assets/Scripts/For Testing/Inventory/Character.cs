using UnityEngine;
public class Character :MonoBehaviour, ICharacter
{
    public Inventory_test Inventory { get; set; }
    public int Health { get; set; }
    public int Level { get; set; }

    public void OnItemEquipped(InvItem item)
    {
        Debug.Log($"you equipped the {item} in {item.EquipSlot}");
    }
}


