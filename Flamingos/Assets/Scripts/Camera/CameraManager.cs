using UnityEngine;

public class CameraManager : KeepOnLoadMonoSingleton<CameraManager>
{
    // At the moment when this script is isntanciated, all stocked camera will be deactivated

    // Two kind of camera can be stored in the manager: specific camera and level camera

    // A specific camera should have a public var to stock the gameObject and a public constante string used to identify it.
    // If a new specific camera is hadded, a new case in the switch statement inside the ChangeCamera method should be hadded.

    // A level camera should be any camera that you don't want to absolutly identify with a string, just like with specifique
    // cameras. It must likely will be a camera inside the level used for a cinematique effect (by xemple, a camera showing a
    // door openning). Those camera will be identified by the index they are associated to insiide the levelCameras array.

    // Use the ChangeCamera() method to switch to an other camera. It takes one parameters, which can either be the string
    // identifying the camera (will always be a public constante of the class) or an int which is the index of the camera
    // inside the levelCameras array.

    public GameObject FPSCamera;
    //public GameObject TPSCamera;
    public GameObject[] levelCameras;

    private GameObject currentCamera;
    // Accessor of the current camera
    public GameObject CurrentCamera { get; private set; }

    private string currentCameraType;
    // Accessor of the current camera type
    public string CurrentCameraType { get; private set; }

    private int currentCameraIndex;
    // Accessor of the current camera index
    public int CurrentCameraIndex { get; private set; }

    public const string FPS_CAMERA = "FPSCamera";
    //public const string TPS_CAMERA = "TPSCamera";

    protected CameraManager() { }

    private void Awake()
    {
        FPSCamera.GetComponent<Camera>().enabled = false;
        //TPSCamera.GetComponent<Camera>().enabled = false;

        foreach (GameObject levelCamera in levelCameras)
        {
            if (levelCamera != null) levelCamera.GetComponent<Camera>().enabled = false;
        }

        CurrentCameraType = "";
        CurrentCameraIndex = -1;
    }

    public void ChangeCamera(string camera)
    {
        switch (camera)
        {
            case FPS_CAMERA:
                ChangeCamera(FPSCamera, camera, -1);
                break;
            /*case TPS_CAMERA:
                ChangeCamera(TPSCamera, camera, -1);
                break;*/
            default:
                Debug.LogError("There is no camera associated to " + camera + ".");
                break;
        }
    }

    public void ChangeCamera(int cameraIndex)
    {
        if (cameraIndex >= levelCameras.Length || levelCameras[cameraIndex] == null)
        {
            Debug.LogError("There is no camera for the index " + cameraIndex + ".");
        }
        else
        {
            ChangeCamera(levelCameras[cameraIndex], "", cameraIndex);
        }
    }

    private void ChangeCamera(GameObject activeCamera, string cameraType, int cameraIndex)
    {
        if (CurrentCamera != null) CurrentCamera.GetComponent<Camera>().enabled = false;
        activeCamera.GetComponent<Camera>().enabled = true;
        CurrentCamera = activeCamera;
        CurrentCameraType = cameraType;
        CurrentCameraIndex = cameraIndex;
    }
}
