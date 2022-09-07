using System.Collections;
using UnityEngine;

public class CameraPickup : MonoBehaviour
{
    private void Start()
    {
    }

    private void Update()
    {
    }

    private void Interact()
    {        
        StartCoroutine(ReturnControlPickUp());
    }

    private IEnumerator ReturnControlPickUp()
    {
        yield return new WaitForSeconds(1f);
        base.transform.parent = GameObject.Find("r_hand").transform;
        base.transform.localPosition = Vector3.zero;
        yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(1f);
        GameObject.Find("Player").GetComponent<CCThirdPersonCharacter>().enabled = true;
        GameController.Instance().CameraGet();
        Object.Destroy(base.gameObject);
    }
}
