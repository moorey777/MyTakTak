using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetUserInput : MonoBehaviour
{
    public InputField input;
    public GameObject inputPanel;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void readStringInput()
    {
      // input = s;
      Global.userName = input.text;
      Debug.Log(input);
      inputPanel.SetActive(false);

    }
}
