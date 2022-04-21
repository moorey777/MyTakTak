using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scorecell : MonoBehaviour
{
    public Text nameText;
    public Text scoreText;

    public void SetModel(Data data)
    {
      nameText.text = data.userName;
      scoreText.text = "Score: "+data.score;
    }
}
