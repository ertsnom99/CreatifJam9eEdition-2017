using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCanvas : MonoBehaviour {

    [SerializeField]
    private Canvas creditCanvas;

    private Action callBackMethod;
    private GameObject disabledCanvas;

    public void SetCallBackMethodOnClose(Action method)
    {
        callBackMethod = method;
    }

    public void SetDisabledCanvas(GameObject disabledCanvas)
    {
        this.disabledCanvas = disabledCanvas;
        this.disabledCanvas.SetActive(false);
    }

    public void LoadCredit()
    {
        Canvas creditCanvas = Instantiate(this.creditCanvas);
        creditCanvas.GetComponent<CreditsCanvas>().SetDisabledCanvas(gameObject);
    }

    public void StartGame()
    {
        Destroy(gameObject);
        Debug.LogWarning("Start The Game Now!");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
