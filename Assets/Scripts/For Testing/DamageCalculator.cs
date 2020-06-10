using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCalculator
{
    // mitigationPrecent is the precent to take off 
    public static int CalculateDamage(int amount, float mitigationPrecent)
    {
        float Multiplier = 1f - mitigationPrecent;
        return Convert.ToInt32((amount * Multiplier));
    }
    public static int CalculateDamage(int amount, ICharacter character)
    {
        int totalArmor = character.Inventory.GetTotalArmor();
        float Multiplier = 100f - totalArmor;
        Multiplier /= 100f;
        return Convert.ToInt32((amount * Multiplier));
    }

}
