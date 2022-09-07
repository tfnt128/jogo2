using UnityEngine;

public class CameraZone : MonoBehaviour
{
    public Vector3 target;

    private void Start()
    {
    }

    private void Update()
    {
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying)
        {
            PopCamPosition();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(base.transform.position, target);
    }

    public void PopCamPosition()
    {
        Camera.main.transform.localPosition = base.transform.position;
        Camera.main.transform.LookAt(target);
        base.transform.LookAt(target);
        Invoke("PopListenerPosition", 1f);
    }

    public void PopListenerPosition()
    {
        GameObject.Find("Listener").transform.position = base.transform.position;
    }
}
