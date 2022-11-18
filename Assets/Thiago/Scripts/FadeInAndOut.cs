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
    private void Update()
    {
        if (act)
        {
            act = false;
            StartCoroutine(FadeInFadeOut());             
        }
        
    }
    IEnumerator FadeInFadeOut()
    {
        isFandingIn = false;
        FadeOut.SetActive(true);
        FadeIn.SetActive(false);
        yield return new WaitForSeconds(3f);
        isFandingIn = true;
        //SceneManager.LoadScene("InventoryScene", LoadSceneMode.Additive);
        //yield return new WaitForSeconds(1f);
        if (isKey)
        {
            InventoryManager.Instance.AddItem(key, 1);
            isKey = false;
        }
        else 
        {
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
}
