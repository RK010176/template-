using NUnit.Framework;
using NSubstitute;

namespace Tests
{
    
    public class inventory 
    {
        [Test]
        public void only_one_chest_euipped_at_a_time()
        {
            //ARRANGE
            ICharacter character = Substitute.For<ICharacter>();
            Inventory_test inventory = new Inventory_test(character);
            InvItem chestOne = new InvItem() { EquipSlot = EquipSlots.Chest };
            InvItem chestTwo = new InvItem() { EquipSlot = EquipSlots.Chest };
            //ACT
            inventory.EquipItem(chestOne);
            inventory.EquipItem(chestTwo);
            //ASSERT
            InvItem equippedItem = inventory.GetItem(EquipSlots.Chest);
            Assert.AreEqual(chestTwo,equippedItem);
        }


        [Test]
        public void tells_characther_when_an_item_equipped_successfully()
        {
            //ARRANGE
            ICharacter character = Substitute.For<ICharacter>();
            Inventory_test inventory = new Inventory_test(character);
            InvItem chestOne = new InvItem() { EquipSlot = EquipSlots.Chest };
           
            //ACT
            inventory.EquipItem(chestOne);

            //ASSERT
            character.Received().OnItemEquipped(chestOne);
        }
    }
}
