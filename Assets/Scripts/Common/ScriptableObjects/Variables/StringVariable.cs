
using UnityEngine;

namespace RoboRyanTron.Unite2017.Variables
{
    [CreateAssetMenu(fileName = "New StringVariable", menuName = "core/StringVariable")]
    public class StringVariable : ScriptableObject
    {
        [SerializeField]
        private string value = "";

        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }
    }
}