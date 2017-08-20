using System;
using UnityEngine;

public class StartCanvas : MonoBehaviour {

    [SerializeField]
    private Canvas creditCanvas;
    
    private GameObject disabledCanvas;

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
        GameManager.Instance.StartGame();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
