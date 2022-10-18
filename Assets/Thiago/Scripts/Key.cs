using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerController player;
    public DoorsWithLock door;
    public GameObject Particle;



    private void Start()
    {        
        player = GameObject.Find("Player").GetComponent<PlayerController>();
       // door = GameObject.FindGameObjectWithTag("DoorsWithLock").get;
    }
    

    private void Update()
    {
        pickKey();

    }
    private void pickKey()
    {
        if (player.canGrab && Input.GetKeyDown(KeyCode.E))
        {
            
            door.hasKey = true;
            player.canGrab = false;            
            Destroy(gameObject);
            Destroy(Particle);
        }
    }
}
