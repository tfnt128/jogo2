using System.Collections;
using System.Collections.Generic;
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
            if (!noteScreen.active)
            {
                audioSource.clip = audioClipsArray[0];
                audioSource.Play();
                wasOpenedOnce = true;

            }

            StartCoroutine(FadeInFadeOut());
            
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
}
