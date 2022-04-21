using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using LitJson;
using UnityEngine.Events;
using System;
using System.IO;
using System.Text;

public class Data
{
  public string userName;
  public int score;

  public Data(string userName, int score)
  {
    this.userName = userName;
    this.score = score;
  }
}

public class LeaderBoardManagement : MonoBehaviour
{
    private const string url = "https://www.dreamlo.com/lb/";
    private const string PrivateCode = "HnS1nPZ-3kOEL0Xb8TJNBw2TLBSoo45UasjVBDUQtDdg";
    private const string PublicCode = "6244dbe28f40bc123c452f8e";

    private void Start()
    {

    }

    public static IEnumerator GetHighestScore(UnityAction<List<Data>> callBack)
    {
      UnityWebRequest request = UnityWebRequest.Get(url+PublicCode+"/json");

      yield return request.SendWebRequest();

      if(request.isHttpError || request.isNetworkError)
      {
        Debug.LogError(request.error);
      }else
      {
        // UTF8Encoding m_utf8 = new UTF8Encoding(false);
        // File.WriteAllText(@"C:\Users\liruo\Desktop\myJson.txt", request.downloadHandler.text, m_utf8);
        //
        Debug.Log(request.downloadHandler.text);

        var data = JsonMapper.ToObject(System.Text.Encoding.UTF8.GetString(System.Text.Encoding.UTF8.GetBytes(request.downloadHandler.text)));

        // var data = JsonMapper.ToObject(request.downloadHandler.text);
        // var data = request.downloadHandler.text.Trim();
        Debug.Log(data);
        var userData = data["dreamlo"]["leaderboard"]["entry"];

        List<Data> DataList = new List<Data>();

        if(userData.IsArray)
        {
          foreach(JsonData user in userData)
          {
            DataList.Add(new Data(user["name"].ToString(),Convert.ToInt32(user["score"].ToString())));
            // Debug.Log(user["score"]);
          }
        }else
        {
          DataList.Add(new Data(userData["name"].ToString(),Convert.ToInt32(userData["score"].ToString())));
        }
        callBack(DataList);

      }
    }

    public static IEnumerator CreateNewHighestScore(string user, int score)
    {
      UnityWebRequest request = new UnityWebRequest(url+PrivateCode+"/add/"+UnityWebRequest.EscapeURL(user)+"/"+score);
      yield return request.SendWebRequest();

      if(request.isHttpError || request.isNetworkError)
      {
        Debug.LogError(request.error);
      }else
      {
        Debug.Log("score added");
      }
    }
}
