using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControl : MonoBehaviour
{
    public GameObject player;
    bool isMoving;
    bool pressedD;
    bool isTurning = false;
    bool isRunning = false;
    float horizontalMove;
    public float verticalMove = 3.8f;
    public float horizontalSpeed = 150f;


    private void Update()
    {
        if(Input.GetKey(KeyCode.D) && !isTurning)
        {
            pressedD = true;            
        }      
        if (Input.GetKey(KeyCode.A) && !isTurning)
        {
            pressedD = false;
        }


        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }


        if (Input.GetButton("Horizontal"))
        {
            isTurning = true;
            player.transform.Rotate(0, horizontalMove, 0);
            horizontalMove = Input.GetAxis("Horizontal") * Time.deltaTime * horizontalSpeed;

            if (!isMoving)
            {
                if (pressedD)
                {
                    player.GetComponent<Animator>().Play("Right");
                }
                else
                {
                    player.GetComponent<Animator>().Play("Left");
                }               
            }            
        }
        else
        {
            isTurning = false;
        }


        if (Input.GetButton("Vertical"))
        {
           
            isMoving = true;

            if (Input.GetButton("SKey"))
            {
                player.GetComponent<Animator>().Play("WalkBack");
            }
            else
            {
                if(!isRunning)
                {
                    verticalMove = 3f;
                    player.GetComponent<Animator>().Play("Walk");
                }
                else
                {
                    verticalMove = 8f;
                    player.GetComponent<Animator>().Play("Run");
                }
            }         

            verticalMove = Input.GetAxis("Vertical") * Time.deltaTime * verticalMove;  
            player.transform.Translate(0,0, verticalMove);
        }
        else
        {
            isMoving = false;
        }


        if(!isMoving && !isTurning)
        {
            
            player.GetComponent<Animator>().Play("Idle");
        }
        
    }
}
