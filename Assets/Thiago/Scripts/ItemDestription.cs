using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDestription : MonoBehaviour
{
    public TextMeshProUGUI textOB;
    public string description = "Description";

    public bool inReach;
    public bool pressed;
    public bool isDoor;


    void Start()
    {
        textOB.enabled = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inReach = true;
            textOB.enabled = true;
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
        if (isDoor)
        {
            
            pressed = true;
            textOB.enabled = true;
            if (!GetComponent<DoorsWithLock>().showUnlockedMsg)
            {
                textOB.text = description.ToString();
            }
            else
            {
                textOB.text = "UNLOCKED";
            }
                
            Time.timeScale = 0;
            if (Input.GetKeyDown(KeyCode.E))
            {
                pressed = false;
                textOB.enabled = false;
                textOB.text = "";
                Time.timeScale = 1;
                isDoor = false;
                this.GetComponent<ItemDestription>().enabled = false;
            }
        }
        else if (inReach && Input.GetKeyDown(KeyCode.E) && inReach)
        {
            if (!pressed)
            {
                pressed = true;                
                textOB.text = description.ToString();
                Time.timeScale = 0;
            }
            else
            {
                pressed = false;
                textOB.enabled = false;
                textOB.text = "";
                Time.timeScale = 1;
            }                        
        }
    }
}
