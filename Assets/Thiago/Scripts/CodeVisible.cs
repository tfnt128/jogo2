using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CodeVisible : MonoBehaviour
{
    CinemachineVirtualCamera currentCamera;
    public CinemachineVirtualCamera targetCameraIn;
    public CinemachineVirtualCamera targetCameraOut;
    private PlayerController player;
    public bool isClose;
    bool isOpen;
    public FadeInAndOut fadeinAndOut;
    public TurnOnAndOffLight turnOnAndOffLight;
    public GameObject normalWindow;
    public GameObject codedWindow;
    public bool canExit;
    public TurnOnAndOffLight light;

    public GameObject go;
    public GameObject go2;
    public GameObject go3;
    public GameObject go4;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    
    // Update is called once per frame
    void Update()
    {
        
        if (light.lightOFF && fadeinAndOut.HasUV)
        {
            go.SetActive(false);
            go2.SetActive(false);
            go3.SetActive(false);
            go4.SetActive(true);
        }
        if(!light.lightOFF && fadeinAndOut.HasUV)
        {
            go.SetActive(false);
            go2.SetActive(false);
            go3.SetActive(true);
        }
            if (fadeinAndOut.HasUV && turnOnAndOffLight.lightOFF && Input.GetKeyDown(KeyCode.E) && isClose)
        {
            
                if (isOpen && canExit)
                {
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
                    normalWindow.SetActive(true);
                    codedWindow.SetActive(false);
                light.enabled = false;
                canExit = false;
                }
                else
                {
                    isOpen = true;
                    player.canMove = false;
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
                light.enabled = true;
                    StartCoroutine(PlaceCodeWindow());
                }

            
        }
        

        
    }
    IEnumerator PlaceCodeWindow()
    {
        yield return new WaitForSeconds(1.5f);
        normalWindow.SetActive(false);
        codedWindow.SetActive(true);
        canExit = true;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            isClose = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isClose = false;
        }
    }
}
