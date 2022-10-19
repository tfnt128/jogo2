using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackScreen : MonoBehaviour
{
    public Animator animator;

    private void Start()
    {
        if (animator == null)
        {
            animator = null;
        }
    }

    public void SetTriggerAnimator( string triggerName)
    {
        animator.SetTrigger(triggerName);
    }

    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

}
