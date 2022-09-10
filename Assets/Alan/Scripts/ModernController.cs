using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ModernController : MonoBehaviour
{

    public float speed;

    [SerializeField] GameObject currentCamera;

    float horizontalInput;
    float verticalInput;

    Rigidbody MyRb;

    public Vector3 moveDirection;

    private Vector3 viewForward = Vector3.zero;

    private Vector3 viewRight = Vector3.zero;

    bool isRunning;
    bool isWalking;


    Animator anim;
    float velocity = 0.0f;
    public float acceleration = 0.1f;
    public float deceleration = 0.1f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        MyRb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        currentCamera = GameObject.FindGameObjectWithTag("CurrentCamera");
    }

    private void Update()
    {
       
        if (Input.GetButton("Run") && isWalking)
        {
            isRunning = true;
            speed = 400f;
        }
        else
        {
            isRunning = false;
            speed = 200f;
        }

        MovementAnimation();
        anim.SetFloat("Velocity", velocity);
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
        {           
            currentCamera = GameObject.FindGameObjectWithTag("CurrentCamera");
            viewForward = Vector3.Scale(currentCamera.transform.forward, new Vector3(1f, 0f, 1f)).normalized;
            viewRight = Vector3.Scale(currentCamera.transform.right, new Vector3(1f, 0f, 1f)).normalized;
            viewRight = new Vector3(viewForward.z, 0f, viewForward.x * -1f);
        }

        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            isWalking = true;
        }        
        else
        {
            isWalking = false;
        }
    }

    private void FixedUpdate()
    {
        moveDirection = verticalInput * viewForward + horizontalInput * viewRight;
        if (moveDirection.sqrMagnitude > 1f)
        {
            moveDirection.Normalize();
        }
        if (moveDirection.sqrMagnitude != 0f)
        {
            base.transform.forward = moveDirection;
        }

        Move();
    }
    void MovementAnimation()
    {
        bool forwardPressed = Input.GetKey("w");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool downPressed = Input.GetKey("s");
        bool runPressed = Input.GetKey("left shift");
        if ((forwardPressed || leftPressed || rightPressed || downPressed) && velocity < 2.0f)
        {
            velocity += Time.deltaTime * acceleration;
        }
        if ((forwardPressed || leftPressed || rightPressed || downPressed) && runPressed && velocity < 3.0f)
        {
            velocity += Time.deltaTime * acceleration;
        }

        if ((!forwardPressed && !leftPressed && !rightPressed && !downPressed) && velocity > 1.0f)
        {
            velocity -= Time.deltaTime * deceleration;
        }
        if ((forwardPressed || leftPressed || rightPressed || downPressed) && !isRunning && velocity > 2.0f)
        {
            velocity -= Time.deltaTime * deceleration;
        }
        if (velocity < 1.0f)
        {
            velocity = 1.0f;
        }

    }
    void Move()
    {
        MyRb.velocity = moveDirection * speed * Time.deltaTime;
    }
}
