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
            //Vector2 localPoint = hit.textureCoord;
            //  Ray _ray = cameraZoom.ViewportPointToRay(localPoint);
           // RaycastHit _hit;

            if (Input.GetMouseButtonDown(0))
            {
                if (hit.transform.GetComponent<KeypadKey>() != null)
                {
                    Debug.Log("dafeewf");
                    hit.transform.GetComponent<KeypadKey>().SendKey();
                }
            }
            
        }

    }
}
