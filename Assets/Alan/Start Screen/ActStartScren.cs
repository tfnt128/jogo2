using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActStartScren : MonoBehaviour
{
  //Animators
  public Animator Camera;
  public Animator Door;
  public Animator DoorLight;
  public Animator EmissiveMaterial;
  public Animator Txt;
  public Animator Logo;
  public Animator LightScreen;
    public AudioSource Button;

    //===============================

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Button.Play();
            Logo.SetTrigger("Act");
            Txt.SetTrigger("Act");
            Camera.SetTrigger("Act");
            Door.SetTrigger("Act");
            DoorLight.SetTrigger("Act");
            EmissiveMaterial.SetTrigger("Act");
            LightScreen.SetTrigger("Act");
        }
    }

}
