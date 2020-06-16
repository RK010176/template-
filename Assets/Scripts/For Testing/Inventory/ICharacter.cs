using System;

public interface ICharacter
{
    Inventory_test Inventory { get; }
    int Health { get;  }    
    int Level { get;  }

    void OnItemEquipped(InvItem item);
}