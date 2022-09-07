using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public uint throwCount;

    public uint keepCount;

    public CameraZone titleScreenPoint;

    public CameraZone firstControlPoint;

    public bool skipIntro;

    private GameObject player;

    public GameObject titleScreen;

    public GameObject pauseMenu;

    public GameObject pointer;

    public bool paused;

    private bool cameraMode;

    public GameObject pointyHand;

    public Camera shotCamera;

    public Camera fpsCamera;

    public GameObject secretPickup;

    public bool gotCamera;

    private bool selfie;

    public GameObject FlipIcon;

    public GameObject ZoomIcon;

    public GameObject ApertureIcon;

    public GameObject canvas;

    private static GameController gameController;

    public static GameController Instance()
    {
        if (!gameController)
        {
            gameController = Object.FindObjectOfType(typeof(GameController)) as GameController;
            if (!gameController)
            {
                Debug.LogError("There needs to be one active GameController script on a GameObject in your scene.");
            }
        }
        return gameController;
    }

    private void Start()
    {
        if (skipIntro)
        {

            Object.Destroy(titleScreen);
            return;
        }
        titleScreen.SetActive(true);
        player = GameObject.Find("Player");
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<CCThirdPersonCharacter>().enabled = false;
        titleScreenPoint.PopCamPosition();
        Invoke("ActivatePointer", 0.5f);
    }

    private void ActivatePointer()
    {
        pointyHand.SetActive(true);
    }

    private void Update()
    {

        if (Input.GetButtonDown("Toggle Mode") && gotCamera)
        {
            if (cameraMode)
            {
                GameObject.Find("Player").GetComponent<CCThirdPersonCharacter>().enabled = true;
                fpsCamera.enabled = false;
                fpsCamera.tag = "Untagged";
                shotCamera.tag = "MainCamera";
                shotCamera.enabled = true;
                cameraMode = false;
                ZoomIcon.SetActive(false);
                FlipIcon.SetActive(false);
                canvas.SetActive(true);
            }
            else
            {
                GameObject.Find("Player").GetComponent<CCThirdPersonCharacter>().enabled = false;
                shotCamera.enabled = false;
                shotCamera.tag = "Untagged";
                fpsCamera.tag = "MainCamera";
                fpsCamera.enabled = true;
                cameraMode = true;
                ZoomIcon.SetActive(true);
                FlipIcon.SetActive(true);
            }
        }
        if (!cameraMode)
        {
            return;
        }
        GameObject.Find("Player").transform.Rotate(0f, Input.GetAxis("Horizontal"), 0f);
        Transform transform = GameObject.Find("SelfiePoint").transform;
        transform.Rotate(Input.GetAxis("Vertical"), 0f, 0f);
        float x = transform.localRotation.eulerAngles.x;
        if (x > 180f)
        {
            x -= 360f;
        }
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView + Input.GetAxis("Zoom"), 5f, 100f);
        if (Input.GetButtonDown("Toggle Direction"))
        {
            selfie = !selfie;
            if (selfie)
            {
                fpsCamera.transform.localPosition = new Vector3(0.19f, 1.59f, 0.62f);
                fpsCamera.transform.localRotation = Quaternion.Euler(0f, 212f, 0f);
                fpsCamera.GetComponent<Camera>().nearClipPlane = 0.05f;
            }
            else
            {
                fpsCamera.transform.localPosition = new Vector3(0f, 1.587f, 0f);
                fpsCamera.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                fpsCamera.GetComponent<Camera>().nearClipPlane = 0.5f;
            }
        }
        if (Input.GetButtonDown("Jump"))
        {
            canvas.SetActive(!canvas.activeSelf);
        }
    }

    public void StartGame()
    {

        pointyHand.SetActive(false);

    }





    public void GiveControl()
    {
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<CCThirdPersonCharacter>().enabled = true;
    }

  

    public void CameraGet()
    {
        gotCamera = true;
        ApertureIcon.SetActive(true);
    }

    public void Unpause()
    {
        StartCoroutine(UnpauseRoutine());
    }

    private IEnumerator UnpauseRoutine()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        pointer.SetActive(false);
        yield return null;
        paused = false;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        Application.LoadLevel(Application.loadedLevel);
    }

    public void LoadURLinNewTab(string url)
    {
        Application.OpenURL(url);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
