using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.Video;

public class ClosetPuzzle : MonoBehaviour
{
    public bool isClose;
    public GameObject go;
    public GameObject go2;
    public NoteSystem noteSystem;
    bool endPuzzle = false;
    [SerializeField] public AudioSource audioSource;
    public PlayerController playerController;
    public ItemDestription destription;
    bool canClose =false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && isClose && !endPuzzle && noteSystem.wasOpenedOnce)
        {
            go.SetActive(false);
            go2.SetActive(true);
            audioSource.Play();
            endPuzzle = true;
            StartCoroutine(message());
        }
        if (Input.GetKeyDown(KeyCode.E) && isClose && canClose)
        {
            destription.pressed = false;
            destription.dialogue.textBox.text = "";
            destription.dialogue.textBox.enabled = false;
            Time.timeScale = 1;
            destription.videoPlayer.Play();
            destription.isMessaging = false;
            destription.gameObject.SetActive(false);
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
    IEnumerator message()
    {
        playerController.canMove = false;
        yield return new WaitForSeconds(2f);
        playerController.canMove = true; ;
        destription.dialogue.PlayDialogue1();
        destription.pressed = true;
        destription.dialogue.textBox.enabled = true;
        destription.dialogue.PlayDialogue1();
        Time.timeScale = 0;
        destription.videoPlayer.Pause();
        destription.isMessaging = true;
        canClose = true;
    }
}
