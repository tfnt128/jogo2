using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ModernController : MonoBehaviour
{

    //animação
    //Rotation melhor;

    private float speed = 100f;

    GameObject mainCamera;

    float horizontalInput;
    float verticalInput;

    Rigidbody MyRb;

    public Vector3 moveDirection;

    private Vector3 viewForward = Vector3.zero;

    private Vector3 viewRight = Vector3.zero;

    private void Awake()
    {
        MyRb = GetComponent<Rigidbody>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            viewForward = Vector3.Scale(mainCamera.transform.forward, new Vector3(1f, 0f, 1f)).normalized;
            viewRight = Vector3.Scale(mainCamera.transform.right, new Vector3(1f, 0f, 1f)).normalized;
            viewRight = new Vector3(viewForward.z, 0f, viewForward.x * -1f);
        }
        moveDirection = verticalInput * viewForward + horizontalInput * viewRight;
        if (moveDirection.sqrMagnitude > 1f)
        {
            moveDirection.Normalize();
        }
        if (moveDirection.sqrMagnitude != 0f)
        {
            base.transform.forward = moveDirection;
        }

        MyRb.velocity = moveDirection * speed * Time.deltaTime;
    }
}
