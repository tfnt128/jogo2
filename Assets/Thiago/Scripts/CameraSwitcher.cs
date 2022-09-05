using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    
    public GameObject targetCamera;
    private void OnTriggerStay(Collider collision)
    {
        if(collision.tag == "Player")
        {
            targetCamera.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Player")
        {
            targetCamera.SetActive(false);
        }
    }
}
