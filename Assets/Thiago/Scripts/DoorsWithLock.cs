using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class DoorsWithLock : MonoBehaviour
{
    public GameObject fadeIn;
    public GameObject fadeOut;
    private PlayerController player;
    public bool hasKey = false;
    public bool locked = true;
    public bool unlocked = false;
    public bool playerIn;
    public DialogueManager dialogue;
    public bool isMessaging = false;
    public bool canSpawnMsg = false;
    public bool canUnlocked = false;
    public VideoPlayer videoPlayer;

    AudioSource audioSource;

    [SerializeField] AudioClip[] audioClipsArray = new AudioClip[3];

    private void Start()
    {
        playerIn = true;
        fadeOut.SetActive(false);
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClipsArray[0];
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && player.hitinfo.collider.GetComponent<DoorsWithLock>().isMessaging)
        {
            player.hitinfo.collider.GetComponent<DoorsWithLock>().isMessaging = false;
            dialogue.textBox.text = "";
            dialogue.textBox.enabled = false;
            videoPlayer.Play();

            if (player.hitinfo.collider.GetComponent<DoorsWithLock>().canUnlocked)
            {
                player.hitinfo.collider.GetComponent<DoorsWithLock>().canUnlocked = false;
                player.hitinfo.collider.GetComponent<DoorsWithLock>().locked = false;
                player.hitinfo.collider.GetComponent<DoorsWithLock>().unlocked = true;
                player.hitinfo.collider.GetComponent<DoorsWithLock>().hasKey = false;

            }
        }
        if (player.canOpenDoor)
        {
            if (player.hitinfo.collider.GetComponent<DoorsWithLock>().isMessaging)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }


        if (locked)
        {
            unlocked = false;
        }
        openDoor();

    }
    private void openDoor()
    {

        if (player.canOpenDoor && player.canMove && Input.GetKeyDown(KeyCode.E))
        {
            if (!player.hitinfo.collider.GetComponent<DoorsWithLock>().unlocked)
            {

                if (!player.hitinfo.collider.GetComponent<DoorsWithLock>().hasKey)
                {
                    player.hitinfo.collider.GetComponent<DoorsWithLock>().canSpawnMsg = !player.hitinfo.collider.GetComponent<DoorsWithLock>().canSpawnMsg;
                    player.hitinfo.collider.GetComponent<DoorsWithLock>().GetComponent<AudioSource>().clip = audioClipsArray[1];
                    player.hitinfo.collider.GetComponent<DoorsWithLock>().GetComponent<AudioSource>().Play();
                    if (!player.hitinfo.collider.GetComponent<DoorsWithLock>().isMessaging && player.hitinfo.collider.GetComponent<DoorsWithLock>().canSpawnMsg)
                    {
                        dialogue.textBox.enabled = true;
                        dialogue.PlayDialogue1();
                        player.hitinfo.collider.GetComponent<DoorsWithLock>().isMessaging = true;
                        videoPlayer.Pause();
                    }


                }
                else if (player.hitinfo.collider.GetComponent<DoorsWithLock>().hasKey)
                {


                    player.hitinfo.collider.GetComponent<DoorsWithLock>().canSpawnMsg = !player.hitinfo.collider.GetComponent<DoorsWithLock>().canSpawnMsg;
                    if (!player.hitinfo.collider.GetComponent<DoorsWithLock>().isMessaging && player.hitinfo.collider.GetComponent<DoorsWithLock>().canSpawnMsg)
                    {
                        dialogue.textBox.enabled = true;
                        dialogue.PlayDialogue2();
                        player.hitinfo.collider.GetComponent<DoorsWithLock>().isMessaging = true;
                        player.hitinfo.collider.GetComponent<DoorsWithLock>().canUnlocked = true;
                        player.hitinfo.collider.GetComponent<DoorsWithLock>().GetComponent<AudioSource>().clip = audioClipsArray[2];
                        player.hitinfo.collider.GetComponent<DoorsWithLock>().GetComponent<AudioSource>().Play();
                        videoPlayer.Pause();
                    }

                }



            }
            else
            {
                player.hitinfo.collider.GetComponent<DoorsWithLock>().canUnlocked = false;
                player.canMove = false;
                player.hitinfo.collider.GetComponent<DoorsWithLock>().GetComponent<AudioSource>().clip = audioClipsArray[0];
                player.hitinfo.collider.GetComponent<DoorsWithLock>().GetComponent<AudioSource>().Play();
                StartCoroutine(changeRoom());
            }
        }


    }

    IEnumerator changeRoom()
    {

        fadeOut.SetActive(true);
        fadeIn.SetActive(false);
        yield return new WaitForSeconds(2.5f);

        if (player.hitinfo.collider.GetComponent<DoorsWithLock>().playerIn)
        {
            player.transform.position = player.hitinfo.collider.GetComponent<DoorsWithLock>().gameObject.transform.GetChild(1).position;
            player.transform.rotation = player.hitinfo.collider.GetComponent<DoorsWithLock>().gameObject.transform.GetChild(1).rotation;
        }
        else
        {
            player.transform.position = player.hitinfo.collider.GetComponent<DoorsWithLock>().gameObject.transform.GetChild(0).position;
            player.transform.rotation = player.hitinfo.collider.GetComponent<DoorsWithLock>().gameObject.transform.GetChild(0).rotation;
        }


        fadeOut.SetActive(false);
        fadeIn.SetActive(true);
        player.canMove = true;
    }
}
