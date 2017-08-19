using UnityEngine;

public class GameManager : KeepOnLoadMonoSingleton<GameManager>
{
    // Gravitie's strength
    public const float GRAVITY = 26.1f;

    void Start ()
    {
	    CameraManager.Instance.ChangeCamera(CameraManager.FPS_CAMERA);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
