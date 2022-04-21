using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsUI : MonoBehaviour
{
    private TextMeshProUGUI gemText;
    // Start is called before the first frame update
    void Start()
    {
        gemText = GetComponent<TextMeshProUGUI>();
        if (gameObject.name == "Gem") {
            if (!PlayerPrefs.HasKey("coins")) {
                gemText.text = "0";
                PlayerPrefs.SetInt("coins", 0);
            } else {
                gemText.text = PlayerPrefs.GetInt("coins").ToString();
            }
        } else if (gameObject.name == "GemR") {
            if (!PlayerPrefs.HasKey("gemR")) {
                gemText.text = "0";
                PlayerPrefs.SetInt("gemR", 0);
            } else {
                gemText.text = PlayerPrefs.GetInt("gemR").ToString();
            }
        } else if (gameObject.name == "GemG") {
            if (!PlayerPrefs.HasKey("gemG")) {  
                gemText.text = "0";
                PlayerPrefs.SetInt("gemG", 0);
            } else {
                gemText.text = PlayerPrefs.GetInt("gemG").ToString();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "Gem") {
            gemText.text = PlayerPrefs.GetInt("coins").ToString();
        } else if (gameObject.name == "GemR") {
            gemText.text = PlayerPrefs.GetInt("gemR").ToString();
        } else if (gameObject.name == "GemG") {
            gemText.text = PlayerPrefs.GetInt("gemG").ToString();
        }
    }
}
