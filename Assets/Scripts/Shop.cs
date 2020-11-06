using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

    public Transform shopPanel;

    public void ShopButton()
    {
        if (shopPanel.gameObject.activeInHierarchy == false)
        {
            shopPanel.gameObject.SetActive(true);
        }
        else
        {
            shopPanel.gameObject.SetActive(false);
        }
    }
}
