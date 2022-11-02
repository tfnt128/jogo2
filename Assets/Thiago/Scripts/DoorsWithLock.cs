using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsWithLock : MonoBehaviour
{
    public GameObject fadeIn;
    public GameObject fadeOut;
    private PlayerController player;
    public bool hasKey = false;
    public bool locked = true;
    public bool unlocked = false;



    private void Start()
    {
        fadeOut.SetActive(false);
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    private void Update()
    {
        if (locked)
        {
            unlocked = false;
        }
        openDoor();

    }
    private void openDoor()
    {

        if (player.canOpenDoor && player.canMove && Input.GetKeyDown(KeyCode.E))
        {
            if (!player.hitinfo.collider.GetComponent<DoorsWithLock>().unlocked)
            {
                Debug.Log("Locked");
            }
            else
            {
                player.canMove = false;
                StartCoroutine(changeRoom());
            }                      
        }

        if (player.canOpenDoor && player.canMove && Input.GetKeyDown(KeyCode.Z) && player.hitinfo.collider.GetComponent<DoorsWithLock>().locked)
        {
            if (player.hitinfo.collider.GetComponent<DoorsWithLock>().hasKey)
            {
                player.hitinfo.collider.GetComponent<DoorsWithLock>().locked = false;
                player.hitinfo.collider.GetComponent<DoorsWithLock>().unlocked = true;
                player.hitinfo.collider.GetComponent<DoorsWithLock>().hasKey = false;
                Debug.Log("Door Unlocked");
            }           
        }       
    }

    IEnumerator changeRoom()
    {
        
        fadeOut.SetActive(true);
        fadeIn.SetActive(false);
        yield return new WaitForSeconds(2.5f);

        player.transform.position = player.hitinfo.collider.gameObject.transform.GetChild(0).position;
        player.transform.rotation = player.hitinfo.collider.gameObject.transform.rotation;
        fadeOut.SetActive(false);
        fadeIn.SetActive(true);
        player.canMove = true;
    }
}
