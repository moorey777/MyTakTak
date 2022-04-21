using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;

public class CS_Player : MonoBehaviour
{

	public Rigidbody my_Rigidbody;
	private int flag = 0;
	//check if right mouse or left mouse has been clicked. 0 means not click, 1 means left,2 means right
	private int used = 0;
	public float MaxForce = 500.0f;
	private float NowForce = 0;
	private float MinForce = 140.0f;
	private float sc;
	private float scaley;

	private Vector3 player_Direction;
	private Vector3 NextPos;

	private GameObject NowCube = null;
	private GameObject NextCube = null;
	private GameObject NextMace = null;
	private GameObject NextGem = null;
	private Vector3 myCamera = Vector3.zero;
	private GameObject myPlane = null;

	[SerializeField]
	protected AudioClip[] myAudioClip;
	protected AudioSource myAudioSource;

	private CS_GUI myGUI = null;

	private CS_CUBE myCube = null;

	private CS_SwingMace mySwingMace = null;
	private CS_FlamingPlate myFlamingPlate = null;
	private CS_FallingRec myFallingRec = null;
	private CS_RedGem myRedGem = null;

	private Color blackPoint_color;
	// private CS_CUBE_Patrol myCubePat = null;

	public Scene scene;
	public GameObject losePanel, winPanel, shadow, player, barHigh, barLow;
	public Material black, transparent;



	void Start()
	{

		Global.Reset();

		// for Analytics
		// Debug.Log("game is begin");
		Global.gameNum++;
		Global.BigRewardReset();
		myPlane = GameObject.FindGameObjectWithTag("Plane");
		myAudioSource = GetComponent<AudioSource>();
		my_Rigidbody = GetComponent<Rigidbody>();
		myGUI = GetComponent<CS_GUI>();
		myCube = GetComponent<CS_CUBE>();
		mySwingMace = GetComponent<CS_SwingMace>();
		myFlamingPlate = GetComponent<CS_FlamingPlate>();
		myFallingRec = GetComponent<CS_FallingRec>();
		myRedGem = GetComponent<CS_RedGem>();
		// myCubePat = GetComponent<CS_CUBE_Patrol>();
		scaley = transform.localScale.y;
		NextCube = myCube.newBox(NowCube);
		player_Direction = new Vector3(NextCube.GetComponent<Renderer>().bounds.center.x - transform.position.x, NextCube.GetComponent<Renderer>().bounds.center.y, NextCube.GetComponent<Renderer>().bounds.center.z - transform.position.z);


		myGUI.ShowPower(NowForce - MinForce, MaxForce - MinForce);
		myGUI.ShowScore();
		scene = SceneManager.GetActiveScene();

		losePanel = GameObject.Find("LosePanel");
		losePanel.SetActive(false);
		if (scene.name == "Level1" || scene.name == "Level2")
		{
			winPanel = GameObject.Find("WinPanel");
			winPanel.SetActive(false);
		}



		blackPoint_color = shadow.GetComponent<Renderer>().material.color;

		barHigh = GameObject.Find("high_edge_bar");
        barLow = GameObject.Find("low_edge_bar");
		if (scene.name == "Level3") {
        	barHigh.SetActive(false);
        	barLow.SetActive(false);
		}
		PlayerPrefs.SetInt("easy", 0);
	}

	void Update()
	{
		GameObject obj = GetHitObject();
		//Debug.Log(obj.tag);
		if (obj != null)
		{
			if (obj.tag == "Cube")
			{

				if (NowCube == null)
				{

					NowCube = obj;
					myCamera = Camera.main.transform.position - NowCube.transform.position;
				}
				else if (NextCube == obj)
				{
					onNewCube();
				}

				if (Global.Score >= 10 && scene.name == "Level1" && Global.isGameOver == 0)
				{
					// Analytics: Ratio of Attemps To Wins
					AnalyticsResult RatioOfAttempsL1 = Analytics.CustomEvent("RatioOfAttempsL1",
					new Dictionary<string, object> {
								 {"RatioOfAttempsL1",  Global.gameNum}
					});

					//Debug.Log("RatioOfAttempsL1: " + RatioOfAttempsL1);
					//Debug.Log("RatioOfAttempsL1: " + Global.gameNum);
					Global.gameNum = 0;
					Global.isGameOver = 1;


					if (scene.name == "Level1" || scene.name == "Level2")
					{
						winPanel.SetActive(true);
					}


					// Time.timeScale = 0f;
				}
				else if (Global.Score >= 20 && scene.name == "Level2" && Global.isGameOver == 0)
				{
					// Analytics: Ratio of Attemps To Wins
					AnalyticsResult RatioOfAttempsL2 = Analytics.CustomEvent("RatioOfAttempsL2",
					new Dictionary<string, object> {
								 {"RatioOfAttempsL2",  Global.gameNum}
					});

					//Debug.Log("RatioOfAttempsL2: " + RatioOfAttempsL2);
					//Debug.Log("RatioOfAttempsL2: " + Global.gameNum);
					Global.gameNum = 0;
					Global.isGameOver = 1;


					winPanel.SetActive(true);
				}

				getInput();

				MoveCamera();


			}
			else if (obj.tag == "Obstacle" && Global.isGameOver == 0)
			{
				my_Rigidbody.AddForce(player_Direction * -5);
				Debug.Log("got hit away by mace");

			}
			else if (obj.tag == "Plane" && Global.isGameOver == 0)
			{
				myAudioSource.PlayOneShot(myAudioClip[2]);
				StartCoroutine("GameOver");
			}

		}

		shadow.transform.position = new Vector3(player.transform.position.x, shadow.transform.position.y, player.transform.position.z);

	}

	void onNewCube()
	{
		//Debug.Log("player:" + transform.position);
		//Debug.Log("center:" + NextCube.GetComponent<Renderer>().bounds.center);
		if (Mathf.Abs(NextCube.GetComponent<Renderer>().bounds.center.z - transform.position.z) <= 0.4f && Mathf.Abs(NextCube.GetComponent<Renderer>().bounds.center.x - transform.position.x) <= 0.4f)
		{
			myAudioSource.PlayOneShot(myAudioClip[3]); // center effect
			Global.BigReward();
			myGUI.Black();
		}
		else
		{
			myAudioSource.PlayOneShot(myAudioClip[1]); // success effect
			Global.BigRewardReset();
			Global.AddScore();
		}


		my_Rigidbody.Sleep();

		myGUI.ShowScore();

		Destroy(NowCube);
		Destroy(NextMace);
		if (Global.cube_choice == 0)
		{
			myCube.DeleteObj();
		}
		else if (Global.cube_choice == 1)
		{
			StopCoroutine("GetTransparent");
		}

		if (scene.name == "Level1" || scene.name == "Level2")
		{
			Global.cube_choice = Random.Range(0, 10);
		}
		else
		{
			if (PlayerPrefs.GetInt("easy") == 1) {
				Global.cube_choice = 99;
			} else {
				Global.cube_choice = Random.Range(0, 8);
			}
		}

		//Debug.Log("a valueL "+ Global.a_v);
		//Global.a_v = 5;
		if (Global.cube_choice == 0)
		{
			NowCube = NextCube;
			NextCube = myCube.newBox(NowCube);

			Global.cubePatNum++;
		}
		else if (Global.cube_choice == 1)
		{
			NowCube = NextCube;
			NextCube = myCube.newBox(NowCube);

			Global.cubeTraspNum++;
			StartCoroutine("GetTransparent");
		}
		else if (Global.cube_choice == 2 || Global.cube_choice == 3)
		{
			NowCube = NextCube;
			Global.cubeNum++;
			NextCube = myCube.newBox(NowCube);
			NextMace = mySwingMace.newBox(NowCube, myCube.getDis(), myCube.getCubeDir());
		}
		else if (Global.cube_choice == 4)
		{
			NowCube = NextCube;
			Global.cubeNum++;
			NextCube = myCube.newBox(NowCube);
			NextMace = myFlamingPlate.newBox(NowCube, myCube.getDis(), myCube.getCubeDir());
		}
		else if (Global.cube_choice == 5)
		{
			NowCube = NextCube;
			Global.cubeNum++;
			NextCube = myCube.newBox(NowCube);
			NextMace = myFallingRec.newBox(NowCube, myCube.getDis(), myCube.getCubeDir());
		}
		else
		{
			NowCube = NextCube;
			Global.cubeNum++;
			NextCube = myCube.newBox(NowCube);
		}

		if (scene.name == "Level3")
		{
			int newGem = Random.Range(0, 5);
			if (newGem == 0)
			{
				NextGem = myRedGem.newBox(NowCube, myCube.getDis(), myCube.getCubeDir());
			}
		}

		// Direction = Global.Direction_cube;
		player_Direction = new Vector3(NextCube.GetComponent<Renderer>().bounds.center.x - transform.position.x, NextCube.GetComponent<Renderer>().bounds.center.y, NextCube.GetComponent<Renderer>().bounds.center.z - transform.position.z);

		my_Rigidbody.WakeUp();
	}



	void getInput()
	{
		if (EventSystem.current.IsPointerOverGameObject())
		{
			return;
		}
		if (Input.GetMouseButtonDown(0) && used == 0)
		{
			myAudioSource.PlayOneShot(myAudioClip[0]);
			NowForce = MinForce;
			used = 1;
		}
		else if (Input.GetMouseButtonDown(1) && used == 0)
		{
			NowForce = MinForce;
			used = 2;
		}
		else if (used == 1 && Input.GetMouseButton(0))
		{
			if (flag == 0)
			{
				NowForce += Time.deltaTime * 80;
				if (NowForce > MaxForce)
				{
					NowForce = MaxForce;
					flag = 1;
				}
				ShowScale();
			}
			else if (flag == 1)
			{
				NowForce -= Time.deltaTime * 80;
				if (NowForce < MinForce)
				{
					NowForce = MinForce;
					flag = 0;
				}
				ShowScale();
			}
		}
		else if (used == 2 && Input.GetMouseButton(1))
		{
			if (flag == 0)
			{
				NowForce += Time.deltaTime * 80;
				if (NowForce > MaxForce)
				{
					NowForce = MaxForce;
					flag = 1;
				}
				ShowScale();
			}
			else if (flag == 1)
			{
				NowForce -= Time.deltaTime * 80;
				if (NowForce < MinForce)
				{
					NowForce = MinForce;
					flag = 0;
				}
				ShowScale();
			}
		}
		else if (Input.GetMouseButtonUp(1))
		{
			JumpHigher();
			BackScale();
			used = 0;

		}
		else if (Input.GetMouseButtonUp(0))
		{
			myAudioSource.Stop(); // stop scaleup
			Jump();
			BackScale();
			used = 0;
		}
		myGUI.ShowPower(NowForce - MinForce, MaxForce - MinForce);
	}

	void Jump()
	{
		my_Rigidbody.AddForce(Vector3.up * NowForce * 1.9f);
		my_Rigidbody.AddForce(player_Direction * NowForce * 0.1f);
		StopCoroutine("GetTransparent");
	}

	void JumpHigher()
	{
		my_Rigidbody.AddForce(Vector3.up * NowForce * 2.5f);
		my_Rigidbody.AddForce(player_Direction * NowForce * 0.04f);
		StopCoroutine("GetTransparent");
	}

	void ShowScale()
	{
		sc = (MaxForce - NowForce) / (MaxForce - MinForce);
		Vector3 scale = transform.localScale;
		scale.y = sc * scaley;
		transform.localScale = scale;
	}

	void BackScale()
	{
		Vector3 scale = transform.localScale;
		scale.y = scaley;
		transform.localScale = scale;
		NowForce = MinForce;
	}

	GameObject GetHitObject()
	{
		RaycastHit hit;
		//adding shadow
		if (Physics.Raycast(transform.position, Vector3.down, out hit, 100f))
		{
			if (hit.collider.gameObject.tag == "Plane")
			{
				shadow.GetComponent<MeshRenderer>().material = transparent;
			}
			else
			{
				shadow.GetComponent<MeshRenderer>().material = black;
			}

		}



		if (Physics.Raycast(transform.position, Vector3.down, out hit, 2f))
		{
			return hit.collider.gameObject;
		}
		else
		{

			var right45 = (Vector3.forward + Vector3.down).normalized;
			// var left45 = (transform.forward - transform.right).normalized;
			Vector3[] vOffests = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right, right45 };
			foreach (Vector3 vof in vOffests)
			{
				if (Physics.Raycast(transform.position + vof * 1f, Vector3.down, out hit, 2f))
				{
					return hit.collider.gameObject;
				}
			}
		}
		return null;
	}

	private void OnParticleCollision(GameObject other)
	{
		if (Global.isGameOver == 0 && other.tag == "Flame")
		{
			my_Rigidbody.velocity = new Vector3(1.0f, 1.0f, 1.0f);
			StartCoroutine("GameOver");
			//Time.timeScale = 0.0f;
		}
	}


	// make cube transparent and disappear
	IEnumerator GetTransparent()
	{
		Color oldColor = NowCube.GetComponent<MeshRenderer>().material.color;

		yield return new WaitForSeconds(2.0f);
		NowCube.GetComponent<MeshRenderer>().material.color = new Color(oldColor.r, oldColor.g, oldColor.b, 170f / 255f);

		yield return new WaitForSeconds(2.0f);
		NowCube.GetComponent<MeshRenderer>().material.color = new Color(oldColor.r, oldColor.g, oldColor.b, 70f / 255f);

		yield return new WaitForSeconds(2.0f);
		NowCube.GetComponent<MeshRenderer>().material.color = new Color(oldColor.r, oldColor.g, oldColor.b, 0f / 255f);

		yield return new WaitForSeconds(2.0f);
		if (NowCube.gameObject != null)
			Destroy(NowCube.gameObject);
	}



	void MoveCamera()
	{
		Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,
													   NowCube.transform.position + myCamera, Time.deltaTime * 2);
		//NowCube.transform.position + myCameraPos;
		//Vector3 pos = new Vector3(0,0,0);
		//Vector3 pos = NowCube.transform.position;
		//Debug.Log(NowCube.transform.position);

		Vector3 pos = new Vector3(NowCube.transform.position.x, 0, NowCube.transform.position.z);

		pos.y = 0;
		myPlane.transform.position = pos;
	}
	//0 means left, 1 means right
	public float GetDistance_Cube_Player(Vector3 vect, int direction)
	{
		if (direction == 0)
		{

			return Mathf.Abs(transform.position.x - vect.x);
		}
		else
		{

			return Mathf.Abs(transform.position.z - vect.z);
		}

	}
	IEnumerator GameOver()
	{
		Global.isGameOver = 1;
		if (Global.isGameOver == 1)
		{
			losePanel.SetActive(true);
			// Analytics: Score distribution
			if (scene.name == "Level1")
			{
				AnalyticsResult ScoreDist = Analytics.CustomEvent("ScoreAvg",
						new Dictionary<string, object> {
									 {"ScoreDist_LV1",  Global.Score}
						});
			}
			else if (scene.name == "Level2")
			{
				AnalyticsResult ScoreDist = Analytics.CustomEvent("ScoreAvg",
						new Dictionary<string, object> {
									 {"ScoreDist_LV2",  Global.Score}
						});
			}
			else
			{
				AnalyticsResult ScoreDist = Analytics.CustomEvent("ScoreAvg",
						new Dictionary<string, object> {
									 {"ScoreDist_LV3",  Global.Score}
						});
			}
			// AnalyticsResult ScoreDist = Analytics.CustomEvent("ScoreDist",
			// 		new Dictionary<string, object> {
			// 					 {"ScoreDist",  Global.Score}
			// 		});
			//
			//       Debug.Log("scoreDistribution: " + ScoreDist);
			//       Debug.Log("scoreDistribution:" + Global.Score);


			// Analytics: Different Cube Occurrence
			AnalyticsResult CubeOccur = Analytics.CustomEvent("CubeOccur",
				new Dictionary<string, object> {
					{ "CubeNum", Global.cubeNum},
					{ "CubePatNum", Global.cubePatNum},
					{"CubeTrasNum", Global.cubeTraspNum}
				});
			//Debug.Log("CubeOccur:" + CubeOccur);
			//Debug.Log("Cube:" + Global.cubeNum);
			//Debug.Log("CubePat:" + Global.cubePatNum);
			//Debug.Log("CubeTran:" + Global.cubeTraspNum);


			// Analytics: falling on the center or edge
			AnalyticsResult CenterEdge = Analytics.CustomEvent("centerEdge",
				new Dictionary<string, object> {
					{ "centerNum", Global.fallCenter},
					{ "edgeNum", Global.fallEdge}
				});
			//Debug.Log("centerEdge: " + CenterEdge);
			//Debug.Log("center time: " + Global.fallCenter);
			//Debug.Log("edge time: " + Global.fallEdge);


			//Analytics: Time used in different level
			Global.gameTime = System.DateTime.Now - Global.beginTime;
			if (scene.name == "Level1")
			{
				AnalyticsResult TimeL1 = Analytics.CustomEvent("TimeDiffLevel",
					new Dictionary<string, object> {
								 {"TimeL1",  Global.gameTime.TotalSeconds}
					});
				//Debug.Log("Time used in different level: " + TimeL1);
			}
			else if (scene.name == "Level2")
			{
				AnalyticsResult TimeL2 = Analytics.CustomEvent("TimeDiffLevel",
					new Dictionary<string, object> {
								 {"TimeL2",  Global.gameTime.TotalSeconds}
					});
				//Debug.Log("Time used in different level: " + TimeL2);
			}
			else
			{
				AnalyticsResult TimeL3 = Analytics.CustomEvent("TimeDiffLevel",
					new Dictionary<string, object> {
								 {"TimeL3",  Global.gameTime.TotalSeconds}
					});
				//Debug.Log("Time used in different level: " + TimeL3);
			}
			//Debug.Log(scene.name + ": " + Global.gameTime.TotalSeconds.ToString());


			// Analytics: avg distance on different level
			Global.totalCube = Global.cubeNum + Global.cubePatNum + Global.cubeTraspNum;
			//Debug.Log("total cube: " + Global.totalCube);
			if (Global.totalCube != 0)
			{
				Global.avgDistance = Global.distanceSum / Global.totalCube;

				if (scene.name == "Level1")
				{
					AnalyticsResult avgDisL1Ana = Analytics.CustomEvent("avgDisL1",
					new Dictionary<string, object> {
					{ "avgDisL1", Global.avgDistance}
					});
					//Debug.Log("avgDisL1Ana: " + avgDisL1Ana);
				}
				else if (scene.name == "Level2")
				{
					AnalyticsResult avgDisL2Ana = Analytics.CustomEvent("avgDisL2",
					new Dictionary<string, object> {
					{ "avgDisL2", Global.avgDistance}
					});
					//Debug.Log("avgDisL2Ana: " + avgDisL2Ana);
				}
				else
				{
					AnalyticsResult avgDisL3Ana = Analytics.CustomEvent("avgDisL3",
					new Dictionary<string, object> {
					{ "avgDisL3", Global.avgDistance}
					});
					//Debug.Log("avgDisL3Ana: " + avgDisL3Ana);

				}

				//Debug.Log("AvgDistance:" + Global.avgDistance);
			}


			// Analytics: avg height on different level
			//if (Global.totalCube != 0)
			//{
			//	Global.avgHeight = Global.heightSum / Global.totalCube;



		}

		if (scene.name == "Level3")
		{
			Debug.Log(Global.userName);
			GameObject.Find("GemRUI").SetActive(false);
			GameObject.Find("GemGUI").SetActive(false);
			StartCoroutine(LeaderBoardManagement.CreateNewHighestScore(Global.userName, Global.Score));
		}
		myGUI.GameOver.enabled = true;
		yield return new WaitForSeconds(2.0f);

		Time.timeScale = 0f;

	}




}
