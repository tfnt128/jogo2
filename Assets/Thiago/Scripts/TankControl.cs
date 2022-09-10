using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TankControl : MonoBehaviour
{

    public GameObject player;
    public bool isMoving;
    public bool isBacking;
    bool isRunning = false;
   public float horizontalMove;
   public float verticalMove;
    bool canStopAnim;
    public float verticalSpeed = 3.8f;
    public float horizontalSpeed = 150f;

    Animator anim;
    float velocityX = 0.0f;
    float velocityY = 0.0f;
    public float acceleration = 0.1f;
    public float deceleration = 0.1f;
    Rigidbody MyRb;
    Vector3 move;
    private void Start()
    {
        MyRb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        MovementAnimation();
        anim.SetFloat("Velocity", velocityY);
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
                verticalSpeed = 150f;
                isBacking = true;                
            }
            else
            {
                isBacking = false;
                if (!isRunning)
                {
                    verticalSpeed = 300f;                   
                }
                else
                {
                    verticalSpeed = 660f;                   
                }
            }
             verticalMove = Input.GetAxis("Vertical") * Time.deltaTime * verticalSpeed;
            //  player.transform.Translate(0, 0, verticalMove);

            if (isBacking)
            {
                move = new Vector3(0, 0, verticalSpeed);
                MyRb.velocity = player.transform.forward * -verticalSpeed * Time.deltaTime;
            }
            else
            {
                move = new Vector3(0, 0, verticalSpeed);
                MyRb.velocity = player.transform.forward * verticalMove;
            }
                
            
           
           
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
    }
    void MovementAnimation()
        {
            bool forwardPressed = Input.GetKey("w");
            bool rightPressed = Input.GetKey("d");
            bool leftpressed = Input.GetKey("a");
            bool runPressed = Input.GetKey("left shift");
            if (forwardPressed && velocityY < 2.0f)
            {
                velocityY += Time.deltaTime * acceleration;
            }
            if (forwardPressed && runPressed && velocityY < 3.0f)
            {
                velocityY += Time.deltaTime * acceleration;
            }
            if (forwardPressed && !isRunning && velocityY > 2.0f)
            {
                velocityY -= Time.deltaTime * deceleration;
            }
            if (!forwardPressed && velocityY > 1.0f)
            {
                velocityY -= Time.deltaTime * deceleration;
            }
            if (!forwardPressed && !isBacking && velocityY < 1.0f)
            {
                velocityY += Time.deltaTime * acceleration;
            }
            if (isBacking && velocityY > 0.0f)
            {
                velocityY -= Time.deltaTime * deceleration;
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
