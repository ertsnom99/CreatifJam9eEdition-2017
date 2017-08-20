using UnityEngine;

public class GameManager : KeepOnLoadMonoSingleton<GameManager>
{
    // Gravitie's strength
    public const float GRAVITY = 26.1f;

    public GameObject Thief;

    void Start ()
    {
	    CameraManager.Instance.ChangeCamera(CameraManager.FPS_CAMERA);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // For ennemis
        GameObject SpawnLocation1 = GameObject.Find("SpawnLocation1");
        GameObject SpawnedThief = Instantiate(Thief, SpawnLocation1.transform.position, SpawnLocation1.transform.rotation);
        SpawnedThief.GetComponent<ThiefMovement>().StartChar();
    }
}
