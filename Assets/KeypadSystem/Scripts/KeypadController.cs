using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeypadController : MonoBehaviour
{
   // public DoorController door;
    public string password;
    public int passwordLimit;
    public Text passwordText;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip correctSound;
    public AudioClip wrongSound;
    public AudioClip digit;

    public DoorsWithLock door;
    public bool corrertPass = false;
    private void Start()
    {
        passwordText.text = "";
    }

    public void PasswordEntry(string number)
    {
        if (number == "Clear")
        {
            Clear();
            return;
        }
        else if(number == "Enter")
        {
            Enter();
            return;
        }
        if (number != "Clear" && number != "Enter")
        {
            if (audioSource != null)
                audioSource.PlayOneShot(digit);
        }


        int length = passwordText.text.ToString().Length;
        if(length<passwordLimit)
        {
            passwordText.text = passwordText.text + number;
        }
    }

    public void Clear()
    {
        passwordText.text = "";
        passwordText.color = Color.white;
    }

    private void Enter()
    {
        if (passwordText.text == password)
        {
            door.locked = false;
            door.unlocked = true;

            if (audioSource != null)
                audioSource.PlayOneShot(correctSound);

            passwordText.color = Color.green;
            StartCoroutine(waitAndClear());
        }
        else
        {
            if (audioSource != null)
                audioSource.PlayOneShot(wrongSound);

            passwordText.color = Color.red;
            StartCoroutine(waitAndClear());
        }
    }

    IEnumerator waitAndClear()
    {
        yield return new WaitForSeconds(0.75f);
        if (passwordText.text == password)
        {
            StartCoroutine(waitAndClear2());
        }
        Clear();
    }
    IEnumerator waitAndClear2()
    {
        yield return new WaitForSeconds(0.35f);
        corrertPass = true;
        
    }
}
