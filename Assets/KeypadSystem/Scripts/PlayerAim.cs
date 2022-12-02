//using UnityEditor.PackageManager;
//using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public Transform headPos;
    public Camera camera;
    public Camera cameraZoom;
    //  public LayerMask layerMask;

    private void Update()
    {

        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(ray.origin, ray.direction * 20, Color.green);
            Vector2 localPoint = hit.textureCoord;
            Ray _ray = cameraZoom.ViewportPointToRay(localPoint);
            RaycastHit _hit;
            Debug.DrawRay(_ray.origin, _ray.direction * 20, Color.green);
            if (Input.GetMouseButtonDown(0))
            {               
                if (Physics.Raycast(_ray, out _hit))
                {
                    if (_hit.transform.GetComponent<KeypadKey>() != null)
                    {
                        Debug.Log("dafeewf");
                        _hit.transform.GetComponent<KeypadKey>().SendKey();
                    }
                }
            }


        }

    }
}
