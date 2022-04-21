using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Timers;



public class CS_GUI : MonoBehaviour
{
	public Text GameOver;
	public Slider Power;
	public Text NowScore;
	public Text Pefect;
	public bool HitCenter;

	private Color colorOrgion = new Color(0, 0, 0, 1);
	private float Alpha = 1.0f;
	private Timer timer = new Timer(2000);
	private GameObject high_edge_bar;
	private GameObject low_edge_bar;

	// private int BestScore = 0;

	// Use this for initialization
	void Start()
	{
		GameOver.enabled = false;
		HitCenter = false;
		Pefect.color = new Color(0, 0, 0, 0);
		high_edge_bar = Power.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject;
		low_edge_bar = Power.transform.GetChild(1).gameObject.transform.GetChild(2).gameObject;
		//this is the way to change pos of bar
		//high_edge_bar.GetComponent<RectTransform>().localPosition += new Vector3(0.2,-74.5,0);
		//Debug.Log(low_edge_bar.GetComponent<RectTransform>().localPosition);
		// BestScore = PlayerPrefs.GetInt("BestScore");


	}

	// Update is called once per frame
	void Update()
	{
		if (HitCenter)
		{
			Alpha = Alpha - (Time.deltaTime * 0.5f);
			colorOrgion.a = Alpha;
			Pefect.color = colorOrgion;
		}
		if (colorOrgion.a <= 0)
		{
			HitCenter = false;
		}


		// if (Input.GetKeyDown("r"))
		// {
		// 	ReStart();
		// }
		if (Input.GetKeyDown("escape"))
		{
			BackMenu();
		}
		GameOver.text = "Game Over !\n" + "Score:" + Global.Score;
	}

	// void StartGameOver(){
	// 	GameOver.enabled = true;
	// }

	public void ShowScore()
	{
		string str = string.Format("Score:" + Global.Score.ToString());
		NowScore.text = str;
	}

	void BackMenu()
	{
		Application.LoadLevel(0);
	}

	void ReStart()
	{
		Application.LoadLevel(1);
	}

	public void ShowPower(float power, float maxpower)
	{
		Power.minValue = 0;
		Power.maxValue = maxpower;
		Power.value = power;
	}



	public void Black()
	{
		HitCenter = true;
		Pefect.color = new Color(0, 0, 0, 1);
		Alpha = 1;
		colorOrgion.a = 1.0f;
		timer.Start();
	}
	public void SetHighEdgeBar(Vector3 vect)
	{
		high_edge_bar = Power.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject;

		//high_edge_bar.GetComponent<RectTransform>().sizeDelta =new Vector2(high_edge_bar.GetComponent<RectTransform>().sizeDelta.x, height);
		high_edge_bar.GetComponent<RectTransform>().localPosition = vect;
	}
	public void SetLowEdgeBar(Vector3 vect)
	{

		low_edge_bar = Power.transform.GetChild(1).gameObject.transform.GetChild(2).gameObject;
		low_edge_bar.GetComponent<RectTransform>().localPosition = vect;
	}
}
