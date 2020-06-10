
using System.Collections.Generic;
using System.Linq;

public class Inventory 
{
    Dictionary<EquipSlots, InvItem> _equippedItems = new Dictionary<EquipSlots, InvItem>();
    List<InvItem> _unequippedItems = new List<InvItem>();
    
    public ICharacter _character { get; private set; }
    public Inventory(ICharacter character)
    {
        _character = character;
    }

    

    public void EquipItem(InvItem item)
    {
        if (_equippedItems.ContainsKey(item.EquipSlot))
            _unequippedItems.Add(_equippedItems[item.EquipSlot]);

        _equippedItems[item.EquipSlot] = item;

        _character.OnItemEquipped(item);
    }

    public InvItem GetItem(EquipSlots equipSlot)
    {
        if (_equippedItems.ContainsKey(equipSlot))
            return _equippedItems[equipSlot];
        return null;
    }
    public int GetTotalArmor()
    {
        int totalArmor = _equippedItems.Values.Sum(t => t.Armor);
        return totalArmor;
    }

}