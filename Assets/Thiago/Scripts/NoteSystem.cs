using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class NoteSystem : MonoBehaviour
{
    public GameObject noteScreen;
    public GameObject FadeOut;
    public GameObject FadeIn;
    private PlayerController player;
    private bool isClose = false;
    [SerializeField] public AudioSource audioSource;
    [SerializeField] AudioClip[] audioClipsArray = new AudioClip[2];
    public bool wasOpenedOnce;
    public bool isOpen;
    int count = 0;
    int count2 = 0;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    void Update()
    {
        if (noteScreen.active)
        {
            player.canMove = false;
        }
       
        if (Input.GetKeyDown(KeyCode.E) && isClose)
        {
            if (!isOpen && count == 0)
            {
                
                count++;
                audioSource.clip = audioClipsArray[0];
                audioSource.Play();
                wasOpenedOnce = true;
                StartCoroutine(FadeInFadeOut());
                StartCoroutine(canCloseAgain());

            }
            else if(isOpen && count2 == 0)
            {
                count2++;
                StartCoroutine(FadeInFadeOut());
                StartCoroutine(canOpenAgain());
            }

            
            
        }
        IEnumerator FadeInFadeOut()
        {
            FadeOut.SetActive(true);
            FadeIn.SetActive(false);
            
            yield return new WaitForSeconds(1.2f);
            isClose = true;
            if (noteScreen.active)
            {
                noteScreen.SetActive(false);
            }
            else
            {
                noteScreen.SetActive(true);
            }
            
            FadeOut.SetActive(false);
            FadeIn.SetActive(true);
            player.canMove = true;
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
    IEnumerator canOpenAgain()
    {
        yield return new WaitForSeconds(1.8f);
        count2 = 0;
        isOpen = false;
    }
    IEnumerator canCloseAgain()
    {
        yield return new WaitForSeconds(1.8f);
        count = 0;
        isOpen = true;
    }
}

