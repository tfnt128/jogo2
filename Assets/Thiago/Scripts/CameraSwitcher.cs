using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{

    public GameObject On;
    public GameObject Off;
    private void OnTriggerStay(Collider collision)
    {
        if(collision.tag == "Player")
        {
            On.SetActive(true);
            Off.SetActive(false);
        }
    }
}
