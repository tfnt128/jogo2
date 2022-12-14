using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachKey : MonoBehaviour
{
    public FadeInAndOut fadeinAndOut;
    public GameObject go;
    public GameObject go2;
    public GameObject particle;
    public DoorsWithLock door;
    public GameObject wire;
    public bool isClose;
    public ItemDestription destription;
    [SerializeField] public AudioSource audioSource;
    bool wireCut = false;
    int count = 0;

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
            go2.SetActive(true);
        }
        if (fadeinAndOut.isFandingIn && isClose)
        {
            go2.SetActive(false);
            Destroy(wire);
            Destroy(this.gameObject);
            Destroy(particle);
        }
        if (Input.GetKeyUp(KeyCode.E) && fadeinAndOut.HasTool && isClose && count == 0)
        {
            count++;
            audioSource.Play();
            door.hasKey = true;
            StartCoroutine(destroyItems()); 
            
            // fadeinAndOut.isKey = true;
            //fadeinAndOut.act = true;
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
    IEnumerator destroyItems()
    {
        yield return new WaitForSeconds(0.1f);
        go2.SetActive(false);
        Destroy(wire);
        Destroy(this.gameObject);
        Destroy(particle);
    }
}
