using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class ClosetPuzzle : MonoBehaviour
{
    public bool isClose;
    public GameObject go;
    public GameObject go2;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && isClose)
        {
            go.SetActive(false);
            go2.SetActive(true);
        }
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
