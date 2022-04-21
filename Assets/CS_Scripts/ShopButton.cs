using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : MonoBehaviour
{
    public GameObject shopPanel;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name == "Shop") {
            shopPanel = GameObject.Find("ShopPanel");
            shopPanel.SetActive(false);
        } else {
            shopPanel = GameObject.Find("ShopPanel");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OpenShop() {
        shopPanel.SetActive(true);
    }
    
    public void CloseShop() {
        shopPanel.SetActive(false);
    }

    public void GetRedItem() {
        if (PlayerPrefs.HasKey("coins")) {
            int coins = PlayerPrefs.GetInt("coins");
            if (coins >= 5) {
                PlayerPrefs.SetInt("coins", coins-5);
                int gemR = PlayerPrefs.GetInt("gemR");
                PlayerPrefs.SetInt("gemR", gemR+1);
            }
        }
    }

    public void GetGreenItem() {
        if (PlayerPrefs.HasKey("coins")) {
            int coins = PlayerPrefs.GetInt("coins");
            if (coins >= 8) {
                PlayerPrefs.SetInt("coins", coins-8);
                int gemG = PlayerPrefs.GetInt("gemG");
                PlayerPrefs.SetInt("gemG", gemG+1);
            }
        }
    }

    public void Bug() {
        PlayerPrefs.SetInt("coins", 100);
    }


}
