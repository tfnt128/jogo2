using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwitchPlayers : MonoBehaviour
{
    public PlayerController PrisonPlayer;
    public FPSController FPSController;
    public GameObject TVPlayer;
    public bool isSecondPerson;
    public bool isClose;
    public GameObject pauseScreen;
    bool canSwicth;
    public GameObject virtualCamera;

    public Transform player;
    public Transform zoomOUT;
    public Transform zoomIN;


    public float transitionSpeed;
    public bool canZoomOUT;
    public bool canZoomIN;


    void Update()
    {
        //zoomOUT.transform.rotation = virtualCamera.transform.rotation;
        if (canZoomIN)
        {
            virtualCamera.transform.position = Vector3.Lerp(virtualCamera.transform.position, zoomIN.position, Time.deltaTime * transitionSpeed);
            virtualCamera.transform.rotation = Quaternion.Lerp(virtualCamera.transform.rotation, zoomIN.rotation, Time.deltaTime * transitionSpeed);

           // player.transform.rotation = Quaternion.Lerp(player.transform.rotation, zoomOUT.rotation, Time.deltaTime * transitionSpeed);
            // ;

        }
        if(canZoomOUT)
        {
            virtualCamera.transform.position = Vector3.Lerp(virtualCamera.transform.position, zoomOUT.position, Time.deltaTime * transitionSpeed);
            virtualCamera.transform.rotation = Quaternion.Lerp(virtualCamera.transform.rotation, zoomOUT.rotation, Time.deltaTime * transitionSpeed);
          //  virtualCamera.transform.rotation = Quaternion.Euler(0, 0, 0);
           // player.transform.rotation = Quaternion.Euler(0, 0, 0);

            // player.transform.rotation = Quaternion.Lerp(player.transform.rotation, zoomOUT.rotation, Time.deltaTime * transitionSpeed);

        }
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

                canZoomOUT = true; 
                //virtualCamera.transform.position = zoomOUT.position;
               // virtualCamera.transform.rotation = zoomOUT.rotation;
                TVPlayer.SetActive(true);
                pauseScreen.SetActive(true);
                PrisonPlayer.canMove = false;
                StartCoroutine(exitTV());
            }
            else
            {

                canZoomIN = true;
                //virtualCamera.transform.position = zoomIN.position;
                //virtualCamera.transform.rotation = zoomIN.rotation;
                // ZoomCamera(25);
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
        //anim.SetTrigger("OUT");
        yield return new WaitForSeconds(1f);
       // player.transform.rotation = new Quaternion(0, 0, 0, 0);
        isSecondPerson = !isSecondPerson;
        isClose = true;
        canSwicth = true;
        TVPlayer.SetActive(false);
        pauseScreen.SetActive(false);
        PrisonPlayer.canMove = true;
        canZoomIN = false;
        canZoomOUT = false;
    }
    IEnumerator exitTV()
    {
        yield return new WaitForSeconds(1f);
      //  player.transform.rotation = new Quaternion(0, 0, 0, 0);
        isSecondPerson = !isSecondPerson;
        isClose = true;
        canSwicth = true;
        FPSController.canMove = true;
        canZoomIN = false;
        canZoomOUT = false;
    }
}

