using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutDoor : MonoBehaviour
{
    public DoorsWithLock door;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            door.playerIn = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            door.playerIn = true;
        }
        
    }
}
