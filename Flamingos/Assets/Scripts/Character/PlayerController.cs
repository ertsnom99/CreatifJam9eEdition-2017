using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterMovement))]

public class PlayerController : MonoBehaviour
{
    private CharacterMovement movementScript;

    private void Awake()
    {
        movementScript = GetComponent<CharacterMovement>();
    }

    private void Update()
    {
        // For test purposess
        debugCommand();

        if (Time.deltaTime > 0)
        {
            Hashtable inputs = fetchInputs();

            // Rotate the character
            movementScript.RotateCharacterWithInput(inputs, true);

            // Move the camera
            if (CameraManager.Instance.CurrentCameraType == CameraManager.FPS_CAMERA)
            {
                CameraManager.Instance.CurrentCamera.GetComponent<FPSCameraControl>().RotateCameraWithInput(inputs, true);
            }
        }
    }

    // The Hashtable of inputs value must contain those keys:
    //-xAxis
    //-yAxis
    Hashtable fetchInputs()
    {
        Hashtable inputs = new Hashtable();

        inputs.Add("verticalInput", 0.0f);
        inputs.Add("horizontalInput", 0.0f);

        inputs.Add("xAxis", Input.GetAxis("Mouse X"));
        inputs.Add("yAxis", Input.GetAxis("Mouse Y"));

        return inputs;
    }
    
    static void ClearConsole()
    {
        // This simply does "LogEntries.Clear()" the long way:
        var logEntries = System.Type.GetType("UnityEditorInternal.LogEntries,UnityEditor.dll");
        var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
        clearMethod.Invoke(null, null);
    }

    void debugCommand()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0.2f;
            }
            else if (Time.timeScale == 0.2f)
            {
                Time.timeScale = 0.0f;
            }
            else
            {
                Time.timeScale = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.ClearDeveloperConsole();
            SceneManager.LoadScene(0);
        }
    }
}
