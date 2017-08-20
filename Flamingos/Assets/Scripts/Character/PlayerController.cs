using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerRotation))]
[RequireComponent(typeof(BottleThrower))]

public class PlayerController : MonoBehaviour
{
    public LayerMask bottlesLayer;

    private PlayerRotation rotationScript;
    private BottleThrower bottleThrowerScript;

    private float raycastDistance = Mathf.Infinity;

    private void Awake()
    {
        rotationScript = GetComponent<PlayerRotation>();
        bottleThrowerScript = GetComponent<BottleThrower>();
    }
    
    private void Update()
    {
        if (Time.deltaTime > 0)
        {
            Hashtable inputs = fetchInputs();

            // Rotate the character
            rotationScript.RotateCharacterWithInput(inputs, true);

            if (CameraManager.Instance.CurrentCameraType == CameraManager.FPS_CAMERA)
            {
                // Rotate the camera
                CameraManager.Instance.CurrentCamera.GetComponent<FPSCameraControl>().RotateCameraWithInput(inputs, true);

                if (!bottleThrowerScript.IsCharging && (bool) inputs["selectInput"])
                {
                    // Cast a ray to check if a bottle is clicked
                    Transform selectedbottle = TrySelectBottle();

                    if (selectedbottle != null && InventoryManager.GetInstance().GetAmountOfItem(selectedbottle.GetComponent<Item>().ID) != 0)
                    {
                        bottleThrowerScript.SelectBottle(selectedbottle.GetComponent<Item>());
                    }
                }
                else if (bottleThrowerScript.SelectedBottle != null)
                {
                    if ((bool)inputs["chargeInput"]) bottleThrowerScript.StartCharging();
                    if ((bool)inputs["throwInput"]) bottleThrowerScript.Throw();
                }
            }

            if ((bool) inputs["pauseInput"]) GameManager.Instance.Pause();
        }
    }

    private Transform TrySelectBottle()
    {
        // Create a ray based on the camera
        GameObject camera = CameraManager.Instance.CurrentCamera;
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);

        RaycastHit hit;

        Physics.Raycast(ray, out hit, raycastDistance, bottlesLayer);

        return hit.transform;
    }

    // The Hashtable of inputs value must contain those keys:
    //-xAxis
    //-yAxis
    //-selectInput
    //-chargeInput
    //-throwInput
    //-pauseInput
    Hashtable fetchInputs()
    {
        Hashtable inputs = new Hashtable();

        inputs.Add("xAxis", Input.GetAxis("Mouse X"));
        inputs.Add("yAxis", Input.GetAxis("Mouse Y"));

        inputs.Add("selectInput", Input.GetButtonDown("Fire2"));

        inputs.Add("chargeInput", Input.GetButtonDown("Fire1"));
        inputs.Add("throwInput", Input.GetButtonUp("Fire1"));

        inputs.Add("pauseInput", Input.GetButtonDown("Cancel"));

        return inputs;
    }
}
