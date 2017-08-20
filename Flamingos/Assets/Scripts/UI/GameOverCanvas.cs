using System;
using UnityEngine;

public class GameOverCanvas : MonoBehaviour {

    [SerializeField]
    private Canvas startCanvas;

    private Action callBackMethodRestart;
    private Action callBackMethodQuit;

    public void SetCallBackMethodOnRestart(Action method)
    {
        callBackMethodRestart = method;
    }

    public void SetCallBackMethodOnQuit(Action method)
    {
        callBackMethodQuit = method;
    }

    public void RestartGame()
    {
        Destroy(gameObject);
        if (callBackMethodRestart != null) callBackMethodRestart();
    }

    public void LoadStart()
    {
        Instantiate(this.startCanvas);
        Destroy(gameObject);
        if (callBackMethodQuit != null) callBackMethodQuit();
    }
}
