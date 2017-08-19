using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerRotation))]
[RequireComponent(typeof(BottleThrower))]

public class PlayerController : MonoBehaviour
{
    private PlayerRotation rotationScript;
    private BottleThrower trowingBottleScript;

    private void Awake()
    {
        rotationScript = GetComponent<PlayerRotation>();
        trowingBottleScript = GetComponent<BottleThrower>();
    }

    private void Update()
    {
        // For test purposess
        debugCommand();

        if (Time.deltaTime > 0)
        {
            Hashtable inputs = fetchInputs();

            // Rotate the character
            rotationScript.RotateCharacterWithInput(inputs, true);

            // Move the camera
            if (CameraManager.Instance.CurrentCameraType == CameraManager.FPS_CAMERA)
            {
                CameraManager.Instance.CurrentCamera.GetComponent<FPSCameraControl>().RotateCameraWithInput(inputs, true);
            }

            if ((bool)inputs["chargeInput"]) trowingBottleScript.StartCharging();
            if ((bool)inputs["throwInput"]) trowingBottleScript.Throw();
        }
    }

    // The Hashtable of inputs value must contain those keys:
    //-xAxis
    //-yAxis
    //-chargeInput
    //-throwInput
    Hashtable fetchInputs()
    {
        Hashtable inputs = new Hashtable();

        inputs.Add("xAxis", Input.GetAxis("Mouse X"));
        inputs.Add("yAxis", Input.GetAxis("Mouse Y"));

        inputs.Add("chargeInput", Input.GetButtonDown("Fire1"));
        inputs.Add("throwInput", Input.GetButtonUp("Fire1"));

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
