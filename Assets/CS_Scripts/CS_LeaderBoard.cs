using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_LeaderBoard : MonoBehaviour
{
    public GameObject leaderboard,closeBT;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenLeaderboard()
    {
        leaderboard.SetActive(true);
        closeBT.SetActive(true);
    }

    public void CloseLeaderboard()
    {
        leaderboard.SetActive(false);
        closeBT.SetActive(false);
    }
}
