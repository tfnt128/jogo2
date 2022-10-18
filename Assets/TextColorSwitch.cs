using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextColorSwitch : MonoBehaviour
{
    public GameObject Image;
    GameObject textMeshPro;
    Color colorDefault;

    private void Start()
    {
        textMeshPro = gameObject;
        colorDefault = textMeshPro.GetComponent<TextMeshProUGUI>().color;
    }

    public void HighLight()
    {
        textMeshPro.GetComponent<TextMeshProUGUI>().color = Color.white;

    }

    public void LowLight()
    {
        textMeshPro.GetComponent<TextMeshProUGUI>().color = colorDefault;
    }

    public void ChangeActive()
    {
        Invoke("GameobjectOFF", .5f);
    }

    void GameobjectOFF()
    {
        Image.SetActive(false);
    }
}
