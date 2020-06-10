using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class damage_calculator
    {
        
        [Test]
        public void sets_damage_to_half_with_50_precent()
        {
            int finalDamage = DamageCalculator.CalculateDamage( 10 ,  0.5f);

            Assert.AreEqual(5,finalDamage);
        }


        [Test]
        public void calculates_2_damage_from_10_with_80_precent()
        {
            int finalDamage = DamageCalculator.CalculateDamage(10, 0.8f);

            Assert.AreEqual(2, finalDamage);
        }


    }
}
