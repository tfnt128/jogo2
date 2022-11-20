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

        if (player.canOpenDoor && player.canMove)
        {
            var thisDoor = player.hitinfo.collider.GetComponent<DoorsWithLock>();
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!thisDoor.isMessaging)
                {
                    StartCoroutine(Order());
                }
                else
                {
                    StartCoroutine(Order2());
                }

                if (locked)
                {
                    unlocked = false;
                }
            }

            void interact()
            {
                if (!player.hitinfo.collider.GetComponent<DoorsWithLock>().unlocked)
                {
                    if (!player.hitinfo.collider.GetComponent<DoorsWithLock>().hasKey)
                    {
                        player.hitinfo.collider.GetComponent<DoorsWithLock>().GetComponent<AudioSource>().clip = audioClipsArray[1];
                        player.hitinfo.collider.GetComponent<DoorsWithLock>().GetComponent<AudioSource>().Play();
                        if (!player.hitinfo.collider.GetComponent<DoorsWithLock>().isMessaging)
                        {
                            player.hitinfo.collider.GetComponent<DoorsWithLock>().dialogue.textBox.enabled = true;
                            player.hitinfo.collider.GetComponent<DoorsWithLock>().dialogue.PlayDialogue1();
                            player.hitinfo.collider.GetComponent<DoorsWithLock>().isMessaging = true;
                        }
                    }
                    else if (player.hitinfo.collider.GetComponent<DoorsWithLock>().hasKey)
                    {
                        if (!player.hitinfo.collider.GetComponent<DoorsWithLock>().isMessaging)
                        {
                            player.hitinfo.collider.GetComponent<DoorsWithLock>().dialogue.textBox.enabled = true;
                            player.hitinfo.collider.GetComponent<DoorsWithLock>().dialogue.PlayDialogue2();
                            player.hitinfo.collider.GetComponent<DoorsWithLock>().isMessaging = true;
                            player.hitinfo.collider.GetComponent<DoorsWithLock>().GetComponent<AudioSource>().clip = audioClipsArray[2];
                            player.hitinfo.collider.GetComponent<DoorsWithLock>().GetComponent<AudioSource>().Play();



                            player.hitinfo.collider.GetComponent<DoorsWithLock>().locked = false;
                            player.hitinfo.collider.GetComponent<DoorsWithLock>().unlocked = true;
                            player.hitinfo.collider.GetComponent<DoorsWithLock>().hasKey = false;

                        }
                    }

                }
                else if (player.hitinfo.collider.GetComponent<DoorsWithLock>().unlocked && !player.hitinfo.collider.GetComponent<DoorsWithLock>().isMessaging)
                {

                    player.canMove = false;
                    player.hitinfo.collider.GetComponent<DoorsWithLock>().GetComponent<AudioSource>().clip = audioClipsArray[0];
                    player.hitinfo.collider.GetComponent<DoorsWithLock>().GetComponent<AudioSource>().Play();
                    StartCoroutine(changeRoom());
                }




            }
            void exitInterect()
            {
                if (player.hitinfo.collider.GetComponent<DoorsWithLock>().isMessaging)
                {
                    player.hitinfo.collider.GetComponent<DoorsWithLock>().isMessaging = false;
                    player.hitinfo.collider.GetComponent<DoorsWithLock>().dialogue.textBox.text = "";
                    player.hitinfo.collider.GetComponent<DoorsWithLock>().dialogue.textBox.enabled = false;
                }
            }
            IEnumerator Order()
            {

                yield return new WaitForSeconds(0.1f);
                interact();

            }
            IEnumerator Order2()
            {

                yield return new WaitForSeconds(0.1f);
                exitInterect();

            }

        }

        if (isMessaging && !player.canOpenDoor)
        {
            isMessaging = false;
            dialogue.textBox.text = "";
            dialogue.textBox.enabled = false;
            videoPlayer.Play();
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

}
