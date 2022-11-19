using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoorFromOtherSide : MonoBehaviour
{
    DoorsWithLock door;
    // Start is called before the first frame update
    void Start()
    {
        door = GetComponent<DoorsWithLock>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!door.playerIn)
        {
            door.hasKey = true;
        }
        else
        {
            door.hasKey = false;
        }
    }
}
