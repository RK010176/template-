using NUnit.Framework;
using NSubstitute;

namespace Tests
{
    public class character_with_inventory
    {
        [Test]
        public void with_90_armor_takes_10_percent_damage()
        {
            //ARRANGE
            ICharacter character = Substitute.For<ICharacter>(); //new ICharacter();
            Inventory_test inventory = new Inventory_test(character);
            InvItem pants = new InvItem() {EquipSlot = EquipSlots.Legs,Armor = 40 };
            InvItem shild = new InvItem () { EquipSlot = EquipSlots.RightHand, Armor = 50 };

            inventory.EquipItem(pants);
            inventory.EquipItem(shild);
            character.Inventory.Returns(inventory);
            
            //ACT
            int claculatedDamage = DamageCalculator.CalculateDamage(1000, character);

            //ASSERT
            Assert.AreEqual(100, claculatedDamage);
        }



    }
}
