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

                // Cast a ray to check if the player is looking at a bottle
                GameObject bottleWatched = GetWacthedBottle();
                
                // Apply over glow effect
                GlowBottleEffectManager.Instance.OverBottle(bottleWatched);

                if (!bottleThrowerScript.IsCharging && (bool) inputs["selectInput"])
                {
                    if (bottleWatched != null && InventoryManager.GetInstance().GetAmountOfItem(bottleWatched.GetComponent<Item>().ID) != 0)
                    {
                        bottleThrowerScript.SelectBottle(bottleWatched.GetComponent<Item>());

                        // Apply select glow effect
                        GlowBottleEffectManager.Instance.SelectBottle(bottleWatched);
                    }
                }
                else if (bottleThrowerScript.SelectedBottle != null && InventoryManager.GetInstance().GetAmountOfItem(bottleThrowerScript.SelectedBottle.ID) != 0)
                {
                    if ((bool)inputs["chargeInput"]) bottleThrowerScript.StartCharging();
                    if ((bool)inputs["throwInput"]) bottleThrowerScript.Throw();
                }
            }

            if ((bool) inputs["pauseInput"]) GameManager.Instance.Pause();
        }
    }

    private GameObject GetWacthedBottle()
    {
        // Create a ray based on the camera
        GameObject camera = CameraManager.Instance.CurrentCamera;
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);

        RaycastHit hit;

        Physics.Raycast(ray, out hit, raycastDistance, bottlesLayer);

        GameObject bottle = null;

        if (hit.transform != null) bottle = hit.transform.gameObject;
        
        return bottle;
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
