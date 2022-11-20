using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using UnityEngine.ProBuilder.Shapes;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class FadeInAndOut : MonoBehaviour
{
    public GameObject FadeOut;
    public GameObject FadeIn;
    public bool act = false;
    [SerializeField] GameObject key;
    [SerializeField] GameObject item;
    [SerializeField] GameObject item2;
    public bool isKey = false;
    public bool isItem1 = false;
    public bool HasUV;
    public bool HasTool;
    public bool isFandingIn = false;
    public int keyNumber;
    public DialogueManager[] dialoguesKey = new DialogueManager[2];
    public DialogueManager[] dialoguesItem = new DialogueManager[2];
    public int itemNumber;
    private void Update()
    {
        if (act)
        {
            act = false;
            StartCoroutine(FadeInFadeOut());             
        }
        if(Input.GetKeyDown(KeyCode.E) && Time.timeScale == 0)
        {
            if (isKey)
            {
                isKey = false;
                dialoguesKey[keyNumber].textBox.text = "";
                dialoguesKey[keyNumber].textBox.enabled = false;
            }
            else
            {
                dialoguesItem[itemNumber].textBox.text = "";
                dialoguesItem[itemNumber].textBox.enabled = false;
            }
            
            
            Time.timeScale = 1;
        }
        
    }
    IEnumerator FadeInFadeOut()
    {
        isFandingIn = false;
        FadeOut.SetActive(true);
        FadeIn.SetActive(false);
        yield return new WaitForSeconds(3f);
        isFandingIn = true;
        StartCoroutine(isFadingoff());
        //SceneManager.LoadScene("InventoryScene", LoadSceneMode.Additive);
        //yield return new WaitForSeconds(1f);
        if (isKey)
        {
            InventoryManager.Instance.AddItem(key, 1);
            dialoguesKey[keyNumber].textBox.enabled = true;
            dialoguesKey[keyNumber].PlayDialogue1();            
            Time.timeScale = 0;
        }
        else 
        {
            dialoguesItem[itemNumber].textBox.enabled = true;
            dialoguesItem[itemNumber].PlayDialogue1();
            Time.timeScale = 0;
            if (isItem1)
            {
                InventoryManager.Instance.AddItem(item, 1);
                isItem1 = false;
            }
            else
            {
                InventoryManager.Instance.AddItem(item2, 1);
            }
            

        }
        //yield return new WaitForSeconds(1f);
        //SceneManager.UnloadSceneAsync("InventoryScene", UnloadSceneOptions.None);
        
        FadeOut.SetActive(false);
        FadeIn.SetActive(true);
    }

    IEnumerator isFadingoff()
    {
        yield return new WaitForSeconds(1f);
        isFandingIn = false;

    }
}
