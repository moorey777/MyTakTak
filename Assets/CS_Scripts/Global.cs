using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Global
{
	public static int Score = 0;
	public static int cube_choice = -1;
	public static Vector3 Direction_cube;

	// used for analytics
	public static int isGameOver = 0;
	public static int cubeNum = 0;
	public static int cubePatNum = 0;
	public static int cubeTraspNum = 0;
	public static int totalCube = 0;
	public static int gameNum = 0;
	public static float distanceSum = 0;
	public static float avgDistance = 0;
	public static float heightSum = 0;
	public static float avgHeight = 0;
	public static int fallCenter = 0;
	public static int fallEdge = 0;

	public static System.DateTime beginTime;
	public static System.TimeSpan gameTime;
	public static int big_reward_mul = 1;

	public static string userName;



	public static void AddScore()
	{
		Score++;
		fallEdge++;
		// if (Score > BestScore){
		// 	BestScore = Score;
		// 	PlayerPrefs.SetInt("BestScore",BestScore);
		// }
	}
	public static void BigReward()
	{

		Score += 2 * big_reward_mul;
		if (big_reward_mul < 10)
		{
			big_reward_mul++;
		}
		fallCenter++;
	}
	public static void BigRewardReset()
	{
		big_reward_mul = 1;
	}

	// Reset everything after game restart
	public static void Reset()
	{
		Score = 0;
		isGameOver = 0;
		cubeNum = 0;
		cubePatNum = 0;
		cubeTraspNum = 0;
		distanceSum = 0;
		totalCube = 0;
		avgDistance = 0;
		heightSum = 0;
		avgHeight = 0;
		fallCenter = 0;
		fallEdge = 0;
		big_reward_mul = 0;

		//gameNum = 0;

		beginTime = System.DateTime.Now;
		gameTime = new System.TimeSpan(0, 0, 0);
	}
}
