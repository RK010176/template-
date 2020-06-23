using Common;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class InteractablesManager : MonoBehaviour
    {
        public List<LevelInteractable> Interactables { get; set; }        

        public void AddInteactables()
        {
            foreach (var InteractableItem in Interactables)
            {
                AddInteactable(InteractableItem);
            }  
        }

        private void AddInteactable(LevelInteractable InteractableItem)
        {            
            // add Interactable
            GameObject InteractableGameObject = Instantiate(InteractableItem.InteractableShape,
                                        InteractableItem.Position,
                                        Quaternion.Euler(InteractableItem.Rotation));
            
            //TODO: move to class
            switch (InteractableItem.Action)
            {
                case Actions.InventoryItem:
                    InteractableGameObject.AddComponent<InventoryItem>();
                    break;
                case Actions.TargetItem:
                    InteractableGameObject.AddComponent<TargetItem>();
                    break;
            }

            // add  Game Element to Inteactable
            Instantiate(InteractableItem.Element.prefab, InteractableGameObject.transform);
        }

        public void RemoveInteractables()
        { 
            
        }
    }

}