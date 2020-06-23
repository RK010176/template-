using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{

    [CreateAssetMenu(fileName = "New FloatVal", menuName = "core/IntVal")]
    public class IntVal : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public int Value;

        public void SetValue(int value)
        {
            Value = value;
        }

        public void SetValue(IntVal value)
        {
            Value = value.Value;
        }

        public void ApplyChange(int amount)
        {
            Value += amount;
        }

        public void ApplyChange(IntVal amount)
        {
            Value += amount.Value;
        }


    }
}