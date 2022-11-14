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
        FadeOut.SetActive(true);
        FadeIn.SetActive(false);
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadSceneAsync("InventoryScene", LoadSceneMode.Additive);
        InventoryManager.Instance.AddItem(key, 1);
        SceneManager.UnloadSceneAsync("InventoryScene");
        FadeOut.SetActive(false);
        FadeIn.SetActive(true);
    }
}
