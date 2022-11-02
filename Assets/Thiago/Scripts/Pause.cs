using ClipperLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Pause : MonoBehaviour
{
    public bool isPaused;
    public AudioSource audio;
    public GameObject pauseScreen;
    public VideoPlayer videoPlayer;

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
                videoPlayer.Pause();               
            }
            else
            {
                pauseScreen.SetActive(false);
                audio.UnPause();
                isPaused = false;
                Time.timeScale = 1;
                videoPlayer.Play();
            }
        }        
    }
}
