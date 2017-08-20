using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesterPause : MonoBehaviour {

    public Canvas pauseCanvas;
    private bool pauseOpen = false;

    void Awake()
    {
        InventoryManager.GetInstance().ResetInventory();
    }

    void Update()
    {
        if (!pauseOpen && Input.GetKeyDown(KeyCode.Z))
        {
            Canvas newPauseCanvas = Instantiate(pauseCanvas);
            newPauseCanvas.GetComponent<PauseCanvas>().SetCallBackMethodOnClose(OnClosePause);
            pauseOpen = true;
        }
    }

    private void OnClosePause()
    {
        pauseOpen = false;
    }
}
