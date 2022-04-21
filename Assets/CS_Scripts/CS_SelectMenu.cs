using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CS_SelectMenu : MonoBehaviour
{
    public GameObject Level1BT,Level2BT,Level3BT,backBT,Next,replayBT,homeBt;

    public void PlayLevel1()
    {
        // Debug.Log("clicked level1");
        SceneManager.LoadScene("Level1");
    }
    public void PlayLevel2()
    {
      SceneManager.LoadScene("Level2");
    }
    public void PlayLevel3()
    {
      SceneManager.LoadScene("Level3");
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("MainMenu");

    }

    public void replay(){
      string sceneName = SceneManager.GetActiveScene().name;
      SceneManager.LoadScene(sceneName);
    }

    public void GoShopping(){
      SceneManager.LoadScene("Shop");
    }

    public void BackFromShopping(){
      string sceneName = SceneManager.GetActiveScene().name;
      SceneManager.LoadScene(sceneName);
    }
}
