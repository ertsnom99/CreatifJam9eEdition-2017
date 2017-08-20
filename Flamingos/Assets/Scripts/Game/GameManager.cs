using System.Collections;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public const string ROOM_TAG = "Room";
    public const string THROWED_BOTTLE_TAG = "ThrowedBottle";
    public const string CLIENT_TAG = "Client";
    public const string DRUNK_TAG = "Drunk";

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject thief;

    [SerializeField]
    private Canvas HUDCanvas;
    [SerializeField]
    private Canvas pauseCanvas;
    [SerializeField]
    private Canvas gameOverCanvas;
    [SerializeField]
    private Canvas shopCanvas;

    [SerializeField]
    private Canvas chargeBarCanvas;

    private GameObject currentHUDCanvas;
    private GameObject currentPauseCanvas;

    private Quaternion initCharacterRot;
    private Quaternion initCameraRot;

    public int Round { get; private set; }

    [SerializeField]
    private float roundDuration = 5f;

    public float RoundDuration
    {
        get { return roundDuration; }
        private set { roundDuration = value; }
    }

    private RoundInfo roundInfo;

    private void Awake()
    {
        RoundDuration = 10f;
    }

    private void Start ()
    {
        // Set the FPS camera as the main camera
        CameraManager.Instance.ChangeCamera(CameraManager.FPS_CAMERA);

        // Hide the stamina bar
        chargeBarCanvas.enabled = false;

        // Diseable control of the character
        player.GetComponent<PlayerController>().enabled = false;

        // Stock the original orientation of the character and camera 
        initCharacterRot = player.transform.rotation;
        initCameraRot = CameraManager.Instance.CurrentCamera.transform.rotation;
    }

    public void StartGame()
    {
        // Display the HUD
        if (currentHUDCanvas == null)
        { 
            currentHUDCanvas = Instantiate(HUDCanvas).gameObject;
            InventoryManager.GetInstance().RegisterObserver(currentHUDCanvas.GetComponent<Player_HUD>());
        }

        // Show the charge bar
        chargeBarCanvas.enabled = true;

        // Reset the inventory to it's base value
        InventoryManager.GetInstance().ResetInventory();

        // Reset the number of round
        Round = 0;

        // Start a new round
        StartRound();
    }

    private void StartRound()
    {
        // Hide and lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Round++;

        roundInfo = RoundInfoFactery.GetInstance().GetRoundInfo(Round);

        currentHUDCanvas.GetComponent<Player_HUD>().NotifyGoalChange(roundInfo.Goal);

        // Eneable control of the character
        player.GetComponent<PlayerController>().enabled = true;

        //*************************************************************//
        // For ennemis
        //SHOULD START GENERATING THE ENNEMIS
        //*************************************************************//
        SpawnManager.Instance.Enable();
        SpawnManager.Instance.StartSpawningDrunks();
        //*************************************************************//
        SpawnManager.Instance.SpawnClient();


        // Start the timer
        StartCoroutine(Timer(RoundDuration));
    }

    IEnumerator Timer(float timeLimit)
    {
        // Cancel the charge and unselect the bottle
        yield return new WaitForSeconds(timeLimit);
        EndRound();
    }

    

    private void EndRound()
    {

        //*************************************************************//
        // For ennemis
        //SHOULD DELETE ALL ENNEMIS AND STOP GENERATING OTHERS
        //*************************************************************//
        SpawnManager.Instance.StopSpawningDrunks();
        SpawnManager.Instance.KillAllDrunks();
        //*************************************************************//

        // Diseable control of the character
        player.GetComponent<PlayerController>().enabled = false;

        // Stop the player from throwing his bottle
        player.GetComponent<BottleThrower>().StopThrowing();

        // Destroy all possible existing bottles
        GameObject[] throwedBottles = GameObject.FindGameObjectsWithTag(THROWED_BOTTLE_TAG);

        foreach (GameObject throwedBottle in throwedBottles)
        {
            Destroy(throwedBottle);
        }

        // Return the character and the camera to there original orientation
        player.transform.rotation = initCharacterRot;
        CameraManager.Instance.CurrentCamera.transform.rotation = initCameraRot;
        
        // Check if the player has enough money to continue playing (Game Over)
        if (InventoryManager.GetInstance().GetTotalMoney() < roundInfo.Goal)
        {
            // Show the game over menu
            GameObject gameOverCanvas = Instantiate(this.gameOverCanvas).gameObject;
            gameOverCanvas.GetComponent<GameOverCanvas>().SetCallBackMethodOnRestart(StartGame);
            gameOverCanvas.GetComponent<GameOverCanvas>().SetCallBackMethodOnQuit(QuitGame);
        }
        else
        {
            // Show the shop
            GameObject shopCanvas = Instantiate(this.shopCanvas).gameObject;
            shopCanvas.GetComponent<ShopCanvas>().SetCallBackMethodOnClose(StartRound);
        }

        // Unhide and unlock the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Pause()
    {
        // Diseable control of the character
        player.GetComponent<PlayerController>().enabled = false;

        // Stop all time base movement... THE WORLD!!! TOKIO TOMARE!!!
        Time.timeScale = 0;

        // Show the pause menu
        currentPauseCanvas = Instantiate(pauseCanvas).gameObject;
        currentPauseCanvas.GetComponent<PauseCanvas>().SetCallBackMethodOnClose(Resume);
        currentPauseCanvas.GetComponent<PauseCanvas>().SetCallBackMethodOnMenu(QuitGame);

        // Unhide and unlock the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SpawnManager.Instance.StopSpawningDrunks();
    }

    public void Resume()
    {
        // Hide and lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Remove the pause menu
        Destroy(currentPauseCanvas);
        currentPauseCanvas = null;

        // Eneable control of the character
        player.GetComponent<PlayerController>().enabled = true;

        // Returne time to normal... THE WORLD!!! TOKIO TOMARE!!!
        Time.timeScale = 1;

        SpawnManager.Instance.StartSpawningDrunks();
    }

    public void QuitGame()
    {
        // Return the character and the camera to there original orientation
        player.transform.rotation = initCharacterRot;
        CameraManager.Instance.CurrentCamera.transform.rotation = initCameraRot;

        // Unregister and remove HUD
        InventoryManager.GetInstance().UnregisterObserver(currentHUDCanvas.GetComponent<Player_HUD>());
        Destroy(currentHUDCanvas);
        currentHUDCanvas = null;

        // Hide the stamina bar
        chargeBarCanvas.enabled = false;

        // Remove the pause menu
        Destroy(currentPauseCanvas);
        currentPauseCanvas = null;

        //*************************************************************//
        // For ennemis
        //SHOULD DELETE ALL ENNEMIS AND STOP GENERATING OTHERS
        SpawnManager.Instance.StopSpawningDrunks();
        SpawnManager.Instance.KillAllDrunks();
        SpawnManager.Instance.Disable();
        //*************************************************************//

    }
}
