using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGameObject : MonoBehaviour
{
    public void DisableGO()
    {
        gameObject.SetActive(false);
    }
}
