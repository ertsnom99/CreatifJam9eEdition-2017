using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsCanvas : MonoBehaviour {

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

    public void CloseCredit()
    {
        Destroy(gameObject);
        disabledCanvas.SetActive(true);
        if (callBackMethod != null) callBackMethod();
    }
}
