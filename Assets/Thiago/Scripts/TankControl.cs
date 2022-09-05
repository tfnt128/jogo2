using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControl : MonoBehaviour
{
    public GameObject player;
    public bool isMoving;
    public bool isRunning = false;
    public float horizontalMove;
    public float verticalMove;
    public float horizontalSpeed = 150;

    private void Start()
    {
        
    }

    private void Update()
    {
        
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
           
            isMoving = true;
            player.GetComponent<Animator>().Play("Walk");
            horizontalMove = Input.GetAxis("Horizontal") * Time.deltaTime * horizontalSpeed;
            verticalMove = Input.GetAxis("Vertical") * Time.deltaTime * 3.9f;
            player.transform.Rotate(0, horizontalMove, 0);
            player.transform.Translate(0,0, verticalMove);
            
        }
        else
        {
            isMoving = false;
            player.GetComponent<Animator>().Play("Idle");
        }
    }
}
