using Cinemachine;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAim : MonoBehaviour
{
    public Transform headPos;
    public Camera camera;
    public Camera cameraZoom;
    public LayerMask layerMask;

    private void Update()
    {

            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, layerMask))
            {
                Debug.DrawRay(ray.origin, ray.direction * 20, Color.green);


            
                Vector2 localPoint = hit.textureCoord;
            if (Input.GetMouseButtonDown(0))
            {

                Ray _ray = cameraZoom.ViewportPointToRay(localPoint);
                RaycastHit _hit;
                Debug.DrawRay(_ray.origin, _ray.direction * 20, Color.green);
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
