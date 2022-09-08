using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ModernController : MonoBehaviour
{
    GameObject gameCamera;
    Rigidbody MyRb;

    float inputHorizontal;
    float inputVerttical;
    public float speed = 20f;
    public float turnSpeed = 150f;

    private void Start()
    {
        MyRb = GetComponent<Rigidbody>();
        gameCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }
    private void Update()
    {
        float inputHorizontal = Input.GetAxisRaw("Horizontal");
        float inputVertical = Input.GetAxisRaw("Vertical");

        float h = inputHorizontal * Time.deltaTime * speed;
        float v = inputVertical * Time.deltaTime * speed;

        Move(h, v);

        //Debug.Log("Vertical = " + inputVerttical + "   " + "Horizontal = " + inputHorizontal);
    }

    void Move(float h, float v)
    {
        Vector3 moveX = gameCamera.transform.right * h;
        Vector3 moveZ = gameCamera.transform.forward * v;
        Vector3 move = moveX + moveZ;

        Vector3 moveTarget = new Vector3(move.x, 0, move.y);

        Transform lastCamPos = gameCamera.transform;

        if (moveTarget != Vector3.zero)
        {
            if (lastCamPos)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(moveTarget), turnSpeed * 2 * Time.deltaTime);
            }
        }

        MyRb.AddForce(move * speed);
    }
}
