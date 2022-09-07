using UnityEngine;

public class CameraZone2 : MonoBehaviour
{
    public CameraZone targetShot;

    private void Start()
    {
    }

    private void Update()
    {
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            targetShot.PopCamPosition();
        }
    }
}
