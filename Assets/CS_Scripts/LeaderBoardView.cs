using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardView : MonoBehaviour
{
    public GameObject scoreCellPrefeb;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LeaderBoardManagement.GetHighestScore(GetHighestScoreCallBack));
    }

    public void GetHighestScoreCallBack(List<Data> datas)
    {
      foreach(var data in datas)
      {
        var cell = Instantiate(scoreCellPrefeb,transform);
        cell.GetComponent<scorecell>().SetModel(data);
      }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
