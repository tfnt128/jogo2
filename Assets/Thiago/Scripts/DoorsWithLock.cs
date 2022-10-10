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
        if (Input.GetKeyDown(KeyCode.P))
        {
            hasKey = true;
        }
        openDoor();

    }
    private void openDoor()
    {
        Transform door = player.hitinfo.collider.gameObject.transform;

        if (locked)
        {
            unlocked = false;
        }
        if (player.canOpenDoor && player.canMove && Input.GetKeyDown(KeyCode.E))
        {
            if (unlocked)
            {
                player.canMove = false;
                StartCoroutine(changeRoom(door));
            }
            else if (hasKey)
            {
                locked = false;
                unlocked = true;
                Debug.Log("Door Unlocked");
            }
            else if(!unlocked)
            {
                Debug.Log("Locked");
            }
        }        

        
    }

    IEnumerator changeRoom(Transform door)
    {
        fadeOut.SetActive(true);
        fadeIn.SetActive(false);
        yield return new WaitForSeconds(2.5f);

        player.transform.position = door.transform.GetChild(0).position;

        fadeOut.SetActive(false);
        fadeIn.SetActive(true);
        player.canMove = true;
    }
}
