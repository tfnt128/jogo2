using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPad : MonoBehaviour
{

    CinemachineVirtualCamera currentCamera;
    public CinemachineVirtualCamera targetCameraIn;
    public CinemachineVirtualCamera targetCameraOut;
    private PlayerController player;
    bool isClose;
    bool isOpen;
    public KeypadController keypad;
    public GameObject keypadModel;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (keypad.corrertPass)
        {
            isOpen = false;
            player.canMove = true;
            keypadModel.SetActive(false);
            if (GameObject.FindGameObjectWithTag("CurrentCamera") != null)
            {
                if (GameObject.FindGameObjectWithTag("CurrentCamera").GetComponent<CinemachineVirtualCamera>() != null)
                {
                    currentCamera = GameObject.FindGameObjectWithTag("CurrentCamera").GetComponent<CinemachineVirtualCamera>();
                }
            }
            else
            {
                currentCamera = null;
            }

            if (currentCamera != targetCameraOut || currentCamera == null)
            {
                targetCameraOut.tag = "CurrentCamera";
                targetCameraOut.Priority = 100;

                currentCamera.tag = "InactiveCamera";
                currentCamera.Priority = 99;
            }
            keypad.corrertPass = false;
            Destroy(GetComponent<Collider>());
            Destroy(targetCameraIn);
            gameObject.GetComponent<KeyPad>().enabled = false;



        }

        if(Input.GetKeyDown(KeyCode.E) && isClose)
        {
            if (isOpen)
            {
                keypadModel.SetActive(false);
                isOpen = false;
                player.canMove = true;
                if (GameObject.FindGameObjectWithTag("CurrentCamera") != null)
                {
                    if (GameObject.FindGameObjectWithTag("CurrentCamera").GetComponent<CinemachineVirtualCamera>() != null)
                    {
                        currentCamera = GameObject.FindGameObjectWithTag("CurrentCamera").GetComponent<CinemachineVirtualCamera>();
                    }
                }
                else
                {
                    currentCamera = null;
                }

                if (currentCamera != targetCameraOut || currentCamera == null)
                {
                    targetCameraOut.tag = "CurrentCamera";
                    targetCameraOut.Priority = 100;

                    currentCamera.tag = "InactiveCamera";
                    currentCamera.Priority = 99;
                }
            }
            else
            {
                isOpen = true;
                player.canMove = false;
                keypadModel.SetActive(true);
                if (GameObject.FindGameObjectWithTag("CurrentCamera") != null)
                {
                    if (GameObject.FindGameObjectWithTag("CurrentCamera").GetComponent<CinemachineVirtualCamera>() != null)
                    {
                        currentCamera = GameObject.FindGameObjectWithTag("CurrentCamera").GetComponent<CinemachineVirtualCamera>();
                    }
                }
                else
                {
                    currentCamera = null;
                }

                if (currentCamera != targetCameraIn || currentCamera == null)
                {
                    targetCameraIn.tag = "CurrentCamera";
                    targetCameraIn.Priority = 100;

                    currentCamera.tag = "InactiveCamera";
                    currentCamera.Priority = 99;
                }
            }
           
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isClose = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isClose = false;
        }
    }
}
