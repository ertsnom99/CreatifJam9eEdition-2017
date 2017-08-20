using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCanvas : MonoBehaviour {

    [SerializeField]
    private Canvas startCanvas;

    private Action callBackMethodResume;
    private Action callBackMethodMenu;

    public void SetCallBackMethodOnClose(Action method)
    {
        callBackMethodResume = method;
    }

    public void SetCallBackMethodOnMenu(Action method)
    {
        callBackMethodMenu = method;
    }

    public void ClosePause()
    {
        Destroy(gameObject);
        if (callBackMethodResume != null) callBackMethodResume();
    }

    public void LoadStart()
    {
        Instantiate(this.startCanvas);
        Destroy(gameObject);
        if (callBackMethodMenu != null) callBackMethodMenu();
    }

}
