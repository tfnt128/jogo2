using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnAndOffLight : MonoBehaviour
{
    private bool isClose = false;
    public Light light;
    public Light light2;
    public Light light3;
    public GameObject textIn;
    public GameObject textOut;
    public bool lightOFF;
    [SerializeField] public AudioSource audioSource;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isClose)
        {
            audioSource.Play();
            if (light.enabled)
            {
                lightOFF = true;
                light.enabled = false;
                light2.enabled = false;
                light3.enabled = false;
                textIn.SetActive(false);
                textOut.SetActive(true);
            }
            else
            {
                lightOFF = false;
                light.enabled = true;
                light2.enabled = true;
                light3.enabled = true;
                textIn.SetActive(true);
                textOut.SetActive(false);
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
