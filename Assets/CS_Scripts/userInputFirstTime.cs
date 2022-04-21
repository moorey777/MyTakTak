using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class userInputFirstTime : MonoBehaviour
{
  public GameObject inputUI;


  void Start()
    {
        // PlayerPrefs.DeleteKey("FirstInput");
        if (!PlayerPrefs.HasKey("FirstInput"))
        {
            inputUI.SetActive(true);
            Debug.Log("First Time Input");
            PlayerPrefs.SetInt("FirstInput", 0);
        }
        else
        {
            inputUI.SetActive(false);
            Debug.Log("NOT First Time Opening");
        }
    }
}
