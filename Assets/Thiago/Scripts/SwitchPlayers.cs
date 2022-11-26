using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlayers : MonoBehaviour
{
    public PlayerController PrisonPlayer;
    public GameObject TVPlayer;
    public bool isSecoundPerson;
    public bool isClose;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isSecoundPerson = !isSecoundPerson;
            switchPlayer();
        }
    }
    void switchPlayer()
    {
        if (isClose)
        {
            if (isSecoundPerson)
            {
                TVPlayer.SetActive(true);
                PrisonPlayer.canMove = false;
            }
            else
            {
                TVPlayer.SetActive(false);
                isClose = true;
                PrisonPlayer.canMove = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player2")
        {
            isClose = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player2")
        {
            isClose = false;
        }
    }
}
