using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 targetPos;

    private void Start()
    {
    }

    private void Update()
    {
        Vector3 vector = base.transform.localPosition - targetPos;
        base.transform.position = Vector3.Lerp(base.transform.position, targetPos, 1f - Mathf.Pow(0.0001f, Time.deltaTime));
    }
}