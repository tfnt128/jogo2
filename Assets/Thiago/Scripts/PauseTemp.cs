using ClipperLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PauseTemp : MonoBehaviour
{
    public bool isPaused;
    public AudioSource audio;
    public GameObject pauseScreen;
    public VideoPlayer videoPlayer;
    [SerializeField] GameObject inventory;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isPaused)
            {
                Time.timeScale = 0;
                isPaused = true;
                audio.Pause();
                pauseScreen.SetActive(true);
                inventory.SetActive(true);
                //videoPlayer.Pause();               
            }
            else
            {
                inventory.SetActive(false);
                pauseScreen.SetActive(false);
                audio.UnPause();
                isPaused = false;
                Time.timeScale = 1;
                //videoPlayer.Play();
            }
        }        
    }
}
