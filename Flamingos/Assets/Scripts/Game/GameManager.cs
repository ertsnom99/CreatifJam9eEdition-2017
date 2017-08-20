using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject thief;

    [SerializeField]
    private Canvas HUDCanvas;
    [SerializeField]
    private Canvas pauseCanvas;

    private GameObject currentHUDCanvas;
    private GameObject currentPauseCanvas;

    private Quaternion initCharacterRot;
    private Quaternion initCameraRot; 

    private void Start ()
    {
        // Set the FPS camera as the main camera
        CameraManager.Instance.ChangeCamera(CameraManager.FPS_CAMERA);

        // Diseable control of the character
        player.GetComponent<PlayerController>().enabled = false;

        // Stock the original orientation of the character and camera 
        initCharacterRot = player.transform.rotation;
        initCameraRot = CameraManager.Instance.CurrentCamera.transform.rotation;
    }

    public void StartGame()
    {
        // Display the HUD
        currentHUDCanvas = Instantiate(this.HUDCanvas).gameObject;
        InventoryManager.GetInstance().RegisterObserver(currentHUDCanvas.GetComponent<Player_HUD>());

        // Hide and lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Eneable control of the character
        player.GetComponent<PlayerController>().enabled = true;

        // For ennemis
        //SHOULD START GENERATING THE ENNEMIS
        /*GameObject SpawnLocation1 = GameObject.Find("SpawnLocation1");
        GameObject SpawnedThief = Instantiate(thief, SpawnLocation1.transform.position, SpawnLocation1.transform.rotation);
        SpawnedThief.GetComponent<ThiefMovement>().StartChar();*/
    }

    public void Pause()
    {
        // Diseable control of the character
        player.GetComponent<PlayerController>().enabled = false;

        // Show the pause menu
        currentPauseCanvas = Instantiate(pauseCanvas).gameObject;
        currentPauseCanvas.GetComponent<PauseCanvas>().SetCallBackMethodOnClose(Resume);
        currentPauseCanvas.GetComponent<PauseCanvas>().SetCallBackMethodOnMenu(QuitGame);

        // Unhide and unlock the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        // Hide and lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Remove the pause menu
        Destroy(currentPauseCanvas);

        // Eneable control of the character
        player.GetComponent<PlayerController>().enabled = true;
    }

    public void QuitGame()
    {
        // Return the character and the camera to there original orientation
        player.transform.rotation = initCharacterRot;
        CameraManager.Instance.CurrentCamera.transform.rotation = initCameraRot;

        // Unregister and remove HUD
        InventoryManager.GetInstance().UnregisterObserver(currentHUDCanvas.GetComponent<Player_HUD>());
        Destroy(currentHUDCanvas);

        // Remove the pause menu
        Destroy(currentPauseCanvas);

        // For ennemis
        //SHOULD DELETE ALL ENNEMIS AND STOP GENERATING OTHERS
    }
}
