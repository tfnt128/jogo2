using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerController player;
    public DoorsWithLock door;
    public GameObject Particle;
    public bool canGrab;
    public bool canDestroy;



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
        if (canGrab && Input.GetKeyDown(KeyCode.E))
        {

            door.hasKey = true;
            canDestroy = true ;
            // FadeInFadeOut();

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
    IEnumerator FadeInFadeOut()
    {
        player.canMove = false;
        door.fadeOut.SetActive(true);
        door.fadeIn.SetActive(false);
        yield return new WaitForSeconds(2.5f);



        Destroy(gameObject);
        Destroy(Particle);
        door.fadeOut.SetActive(false);
        door.fadeIn.SetActive(true);
        player.canMove = true;
    }
}
