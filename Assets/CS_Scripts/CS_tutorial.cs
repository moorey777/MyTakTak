using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class CS_tutorial : MonoBehaviour
{
    public GameObject tutorialboxUI; 
    // Start is called before the first frame update
    // Update is called once per frame
    public Text PageText;
    public GameObject VideoPanel1;
    public GameObject VideoPanel2;
    public GameObject VideoPanel3;
    public GameObject VideoPanel4;
    private GameObject currentvideo; 
    private int tutorial_page = 1;
    private string page1="Welcome to Taktak!  On the left side we got our charge bar! Press and hold your left click to jump FURTHER! Press and hold your right click to jump HIGHER! Get 10 points to next level! Click on *Next* to see detailed tutorials :D";


    private string page2 =
        "When you jumped onto the center, you get bonus points for that! Good Job! As you can see, the cube is disapearing gradually. Jump before it disappear! :D ";
    private string page3 = "Dont get hit by the swinging mace or you will be bounced off! Make sure your charge can jump on the moving cube or you will lose!@Have Fun! @:D ";

	void Start() 
    {
        // PlayerPrefs.DeleteKey("FirstGaming");
        if (!PlayerPrefs.HasKey("FirstGaming"))
        {
            this.OpenTutorial();
            Debug.Log("First Time Opening");
            PlayerPrefs.SetInt("FirstGaming", 0);
        }
        else
        {
            Debug.Log("NOT First Time Opening");
        }
    }

    public void OpenTutorial()
    {
        tutorialboxUI.SetActive(true);
        page1= PageText.text;
    }

    public void CloseTutorial()
    {
        tutorialboxUI.SetActive(false);
    }
    //exit button 

    public void NextPage()
    {
        if (tutorial_page < 3)
        {
            int nextpage = tutorial_page+1; 
            //Debug.Log(nextpage);
           
            //Debug.Log("VideoPanel"+tutorial_page);
            currentvideo = GameObject.Find("VideoPanel"+tutorial_page);
            currentvideo.SetActive(false);
            if (nextpage == 2)
            {
                PageText.text = page2; 
                VideoPanel2.SetActive(true);
            }
            else if(nextpage ==3)
            {
                page3 = page3.Replace("@", Environment.NewLine);
                PageText.text = page3; 
                VideoPanel3.SetActive(true);
                VideoPanel4.SetActive(true);
            }

            tutorial_page += 1;
        }
    }

    public void PrevPage()
    {
        if (tutorial_page >1)
        { 
            
            int prevpage = tutorial_page-1; 
            currentvideo = GameObject.Find("VideoPanel"+tutorial_page);
            Debug.Log("VideoPanel"+tutorial_page);
            currentvideo.SetActive(false);
            

            if (prevpage == 2)
            {
                GameObject.Find("VideoPanel"+ (tutorial_page+1)).SetActive(false);
                PageText.text = page2; 
                VideoPanel2.SetActive(true);
            }
            else if(prevpage ==1)
            {
                PageText.text = page1; 
                VideoPanel1.SetActive(true);
            }

            tutorial_page -= 1;
            
        }
    }
    
}
