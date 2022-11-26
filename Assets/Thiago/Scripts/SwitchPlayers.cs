using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlayers : MonoBehaviour
{
    public PlayerController PrisonPlayer;
    public FPSController FPSController;
    public GameObject TVPlayer;
    public bool isSecondPerson;
    public bool isClose;
    public GameObject pauseScreen;
    bool canSwicth;
    public Animator anim;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            switchPlayer();
        }
    }
    void switchPlayer()
    {
        if (isClose)
        {
            isClose = false;
            canSwicth = false;
            if (isSecondPerson)
            {               
                TVPlayer.SetActive(true);
                pauseScreen.SetActive(true);
                PrisonPlayer.canMove = false;
                StartCoroutine(exitTV());
            }
            else
            {
                FPSController.canMove = false;
                StartCoroutine(enterTV());
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (canSwicth)
        {
            if (other.tag == "Player2")
            {
                isClose = true;
            }
        }
        
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player2")
        {
            isClose = false;
        }
    }
    IEnumerator enterTV()
    {
        anim.SetTrigger("OUT");
        yield return new WaitForSeconds(1f);
        isSecondPerson = !isSecondPerson;
        isClose = true;
        canSwicth = true;
        TVPlayer.SetActive(false);
        pauseScreen.SetActive(false);
        PrisonPlayer.canMove = true;
    }
    IEnumerator exitTV()
    {
        yield return new WaitForSeconds(1f);
        isSecondPerson = !isSecondPerson;
        isClose = true;
        canSwicth = true;
        FPSController.canMove = true;
    }
}

