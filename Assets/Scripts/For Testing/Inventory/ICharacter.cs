using System;

public interface ICharacter
{
    Inventory Inventory { get; }
    int Health { get;  }    
    int Level { get;  }

    void OnItemEquipped(InvItem item);
}