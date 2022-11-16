using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsThatCanBeGrab : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerController player;
    public GameObject Particle;
    public bool intemGrabbed = false;
    public bool canGrab;
    public bool canDestroy;
    public FadeInAndOut FadeInAndOut;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    private void Update()
    {
        pickItem();
    }
    private void pickItem()
    {
        if (canGrab && Input.GetKeyDown(KeyCode.E))
        {
            if(this.tag == "UV")
            {
                FadeInAndOut.isItem1 = true;
                FadeInAndOut.HasUV = true;
            }
            else
            {
                FadeInAndOut.HasTool = true;
            }
            intemGrabbed = true;
            canDestroy = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            player.canGrab = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player.canGrab = false;
        }
    }
}
