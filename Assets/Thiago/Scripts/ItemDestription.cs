using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ItemDestription : MonoBehaviour
{
    public string description = "Description";
    public VideoPlayer videoPlayer;
    public DialogueManager dialogue;

    public bool inReach;
    public bool pressed;
    public bool isDoor;


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inReach = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inReach = false;
            
        }
    }

    void Update()
    {
       if (inReach && Input.GetKeyDown(KeyCode.E) && inReach)
        {
            if (!pressed)
            {
                pressed = true;
                dialogue.textBox.enabled = true;
                dialogue.PlayDialogue1();
                Time.timeScale = 0;
                videoPlayer.Pause();
            }
            else
            {
                pressed = false;
                dialogue.textBox.text = "";
                dialogue.textBox.enabled = false;
                Time.timeScale = 1;
                videoPlayer.Play();
            }                        
        }
    }
}
