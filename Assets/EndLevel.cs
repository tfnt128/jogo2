using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    void LastDoor()
    {
        SceneManager.LoadScene("EndScene");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            LastDoor();
        }
    }
}
