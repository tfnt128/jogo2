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
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("InventoryScene", LoadSceneMode.Additive);
        yield return new WaitForSeconds(1f);
        InventoryManager.Instance.AddItem(key, 1);
        yield return new WaitForSeconds(1f);
        SceneManager.UnloadSceneAsync("InventoryScene", UnloadSceneOptions.None);
        FadeOut.SetActive(false);
        FadeIn.SetActive(true);
    }
}
