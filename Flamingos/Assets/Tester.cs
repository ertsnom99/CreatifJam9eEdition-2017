using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour {

    public Canvas shopCanvas;
    private bool shopOpen = false;

    void Awake()
    {
        InventoryManager.GetInstance().ResetInventory();
    }

    void Update()
    {
        if (!shopOpen && Input.GetKeyDown(KeyCode.Z))
        {
            Canvas newShopCanvas = Instantiate(shopCanvas);
            newShopCanvas.GetComponent<ShopCanvas>().SetCallBackMethodOnClose(OnCloseShop);
            shopOpen = true;
        }
    }

    private void OnCloseShop()
    {
        shopOpen = false;
    }
}
