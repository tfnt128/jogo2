using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachKey : MonoBehaviour
{
    public FadeInAndOut fadeinAndOut;
    public GameObject go;
    public DoorsWithLock door;
    public bool isClose;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeinAndOut.HasTool)
        {
            go.SetActive(false);
        }
        if (Input.GetKeyUp(KeyCode.E) && fadeinAndOut.HasTool && isClose)
        {
            door.hasKey = true;
            fadeinAndOut.isKey = true;
            fadeinAndOut.act = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            isClose = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isClose = false;
        }
    }
}
