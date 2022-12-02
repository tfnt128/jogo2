using UnityEngine;
using UnityEngine.Video;

public class ItemDestription : MonoBehaviour
{
    //  public string description = "Description";
   // public VideoPlayer videoPlayer;
    public DialogueManager dialogue;

    public bool inReach;
    public int pressed;
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pressed = 0;
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
        Debug.Log(pressed);

        if (inReach && Input.GetKeyDown(KeyCode.E) && inReach && !playAnotherScript)
        {
            
            if (has2Dialogues)
            {               
                if (pressed == 0)
                {
                    pressed++;
                    dialogue.textBox.enabled = true;
                    dialogue.PlayDialogue1();
                    Time.timeScale = 0;
                   // videoPlayer.Pause();
                    isMessaging = true;
                }
                else if (pressed == 1)
                {
                    if (dialogue.dialogueVertexAnimator.hadEnded)
                    {
                        dialogue.dialogueVertexAnimator.hadEnded = false;                 
                        pressed++;
                        Time.timeScale = 0;
                        dialogue.PlayDialogue2();
                        isMessaging = true;
                    }
                    else
                    {
                        Time.timeScale = 0;
                        dialogue.AcelerateDialogue();
                    }                   
                }
                else if(pressed == 2)
                {
                    if (dialogue.dialogueVertexAnimator.hadEnded)
                    {
                        dialogue.dialogueVertexAnimator.hadEnded = false;
                        pressed = 0;
                        dialogue.textBox.text = "";
                        dialogue.textBox.enabled = false;
                        Time.timeScale = 1;
                      //  videoPlayer.Play();
                        isMessaging = false;
                    }
                    else
                    {                        
                        dialogue.AcelerateDialogue();
                        Time.timeScale = 0;
                    }                 
                }
            }
            else
            {               
                if (pressed == 0 && !endDilogues)
                {
                    dialogue.dialogueVertexAnimator.hadEnded = false;
                    pressed++;
                    dialogue.textBox.enabled = true;
                    dialogue.PlayDialogue1();
                    Time.timeScale = 0;
                    isMessaging = true;
                }
                else if(pressed == 1 && !endDilogues)
                {
                    if (dialogue.dialogueVertexAnimator.hadEnded)
                    {
                        pressed = 0;
                        dialogue.textBox.text = "";
                        dialogue.textBox.enabled = false;
                        Time.timeScale = 1;
                     //   videoPlayer.Play();
                        isMessaging = false;
                    }
                    else
                    {
                        Time.timeScale = 0;
                        dialogue.AcelerateDialogue();
                    }
                }
            }
        }
    }
}
