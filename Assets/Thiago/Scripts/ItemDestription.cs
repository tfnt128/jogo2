using UnityEngine;
using UnityEngine.Video;

public class ItemDestription : MonoBehaviour
{
    //  public string description = "Description";
    public VideoPlayer videoPlayer;
    public DialogueManager dialogue;

    public bool inReach;
    public bool pressed;
    public bool isDoor;
    public bool has2Dialogues;
    public bool endDilogues = false;
    public bool isMessaging;
    public bool playAnotherScript;


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
        if (inReach && Input.GetKeyDown(KeyCode.E) && inReach && !playAnotherScript)
        {
            if (endDilogues)
            {
                dialogue.textBox.text = "";
                dialogue.textBox.enabled = false;
                Time.timeScale = 1;
                videoPlayer.Play();
                isMessaging = false;
            }
            if (has2Dialogues)
            {
                

                if (!pressed)
                {
                    pressed = true;
                    dialogue.textBox.enabled = true;
                    dialogue.PlayDialogue1();
                    Time.timeScale = 0;
                    videoPlayer.Pause();
                    isMessaging = true;
                }
                else
                {
                    dialogue.PlayDialogue2();
                    has2Dialogues = false;
                    pressed = false;
                    has2Dialogues = false;
                    endDilogues = true;
                    isMessaging = true;

                }
            }
            else
            {
                if (!pressed && !endDilogues)
                {

                    pressed = true;
                    dialogue.textBox.enabled = true;
                    dialogue.PlayDialogue1();
                    Time.timeScale = 0;
                    videoPlayer.Pause();
                    isMessaging = true;
                }
                else
                {
                    pressed = false;
                    dialogue.textBox.text = "";
                    dialogue.textBox.enabled = false;
                    Time.timeScale = 1;
                    videoPlayer.Play();
                    isMessaging = false;

                }
            }
        }
    }
}
