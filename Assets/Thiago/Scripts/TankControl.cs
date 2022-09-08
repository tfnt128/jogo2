using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControl : MonoBehaviour
{
    public GameObject player;
    public bool isMoving;
    public bool isBacking;
    bool isRunning = false;
    float horizontalMove;
    float verticalMove;
    bool canStopAnim;
    public float verticalSpeed = 3.8f;
    public float horizontalSpeed = 150f;

    Animator anim;
    float velocityX = 0.0f;
    float velocity = 0.0f;
    public float acceleration = 0.1f;
    public float deceleration = 0.1f;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        MovementAnimation();
        anim.SetFloat("Velocity", velocity);
        anim.SetFloat("VelocityX", velocityX);


        if (Input.GetButton("Run"))
        {

            if (Input.GetButton("Run") && isBacking)
            {
                StartCoroutine(stopMove());
                 player.GetComponent<Animator>().Play("QuickTurn");
            }
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }


        if (Input.GetButton("Horizontal") && !canStopAnim)
        {         
            horizontalMove = Input.GetAxis("Horizontal") * Time.deltaTime * horizontalSpeed;
            player.transform.Rotate(0, horizontalMove, 0);
        }
     


        if (Input.GetButton("Vertical") && !canStopAnim)
        {

            isMoving = true;

            if (Input.GetButton("SKey"))
            {
                verticalSpeed = 1.5f;
                isBacking = true;                
            }
            else
            {
                isBacking = false;
                if (!isRunning)
                {
                    verticalSpeed = 3f;                   
                }
                else
                {
                    verticalSpeed = 8f;                   
                }
            }
            verticalMove = Input.GetAxis("Vertical") * Time.deltaTime * verticalSpeed;
            player.transform.Translate(0, 0, verticalMove);
        }
        else
        {
            isBacking = false;
            isMoving = false;
        }


        IEnumerator stopMove()
        {
            canStopAnim = true;
            yield return new WaitForSeconds(1.18f);
            canStopAnim = false;
        }

        void MovementAnimation()
        {
            bool forwardPressed = Input.GetKey("w");
            bool rightPressed = Input.GetKey("d");
            bool leftpressed = Input.GetKey("a");
            bool runPressed = Input.GetKey("left shift");
            if (forwardPressed && velocity < 2.0f)
            {
                velocity += Time.deltaTime * acceleration;
            }
            if (forwardPressed && runPressed && velocity < 3.0f)
            {
                velocity += Time.deltaTime * acceleration;
            }
            if (forwardPressed && !isRunning && velocity > 2.0f)
            {
                velocity -= Time.deltaTime * deceleration;
            }
            if (!forwardPressed && velocity > 1.0f)
            {
                velocity -= Time.deltaTime * deceleration;
            }
            if (!forwardPressed && !isBacking && velocity < 1.0f)
            {
                velocity += Time.deltaTime * acceleration;
            }
            if (isBacking && velocity > 0.0f)
            {
                velocity -= Time.deltaTime * deceleration;
            }






            if (rightPressed && !leftpressed && !forwardPressed && !isBacking && velocityX < 1.0f)
            {
                velocityX += Time.deltaTime * acceleration;
            }
            if (!rightPressed && leftpressed && !forwardPressed && velocityX > -1.0f)
            {
                velocityX -= Time.deltaTime * deceleration;
            }
            if (!rightPressed && !leftpressed && velocityX < 0.0f)
            {
                velocityX += Time.deltaTime * acceleration;
            }
            if (!rightPressed && !leftpressed && velocityX > 0.0f)
            {
                velocityX -= Time.deltaTime * deceleration;
            }
        }

    }
}
