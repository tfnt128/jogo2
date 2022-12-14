using UnityEngine;
using UnityEngine.SceneManagement;

public class Key : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerController player;
    public DoorsWithLock door;
    public GameObject Particle;
    public bool canGrab;
    public bool canDestroy;
    public int count = 0;
    public int keyNumber;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    private void Update()
    {
        pickKey();
    }
    private void pickKey()
    {
        if (canGrab && Input.GetKeyDown(KeyCode.E))
        {
            door.hasKey = true;
            canDestroy = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            player.canGrab = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player.canGrab = false;
        }
    }
}