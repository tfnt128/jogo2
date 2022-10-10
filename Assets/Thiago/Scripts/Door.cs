using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine;

public class Door : MonoBehaviour
{

    public GameObject fadeIn;
    public GameObject fadeOut;
    private PlayerController player;



    private void Start()
    {
        
        
        fadeOut.SetActive(false);
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    private void Update()
    {
        openDoor();

    }
    private void openDoor()
    {
        Transform door = player.hitinfo.collider.gameObject.transform;

        if (player.canOpenDoor && player.canMove && Input.GetKeyDown(KeyCode.E))
        {
            player.canMove = false;
            StartCoroutine(changeRoom(door));


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
