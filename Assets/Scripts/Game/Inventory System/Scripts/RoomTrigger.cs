using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;

    private void Start()
    {
        _inventory =  FindObjectOfType<Inventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("H in");
            _inventory._inRoom = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("H exit");
            _inventory._inRoom = false;
        }
    }
    

}
