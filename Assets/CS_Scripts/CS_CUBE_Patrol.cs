// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class CS_CUBE_Patrol : CS_CUBE
// {
//
// 	private int flag = 0;
// 	private Vector3 patDirection;
// 	private Vector3 leftPos;
// 	private Vector3 rightPos;
// 	private Vector3 forwardPos;
// 	private Vector3 backPos;
// 	private int choice;
// 	private Vector3 dir1;
// 	private	Vector3 dir2;
// 	private Vector3 dir3;
// 	private Vector3 dir4;
// 	System.Random myRand = new System.Random();
//
// 	// private GameObject newobj_Patrol = null;
//
//     // Start is called before the first frame update
//     void Start()
//     {
// 	}
//
//     // Update is called once per frame
//     void Update()
//     {
//         if (newobj != null) {
//             patroller();
//         }
//
//     }
//
// 	// you need to delete the object in case it's inherited to the next cube
// 	public void DeleteObj() {
// 		newobj = null;
// 	}
//
// 	public void patroller() {
//
// 		// newobj.transform.position = leftPos; ||newobj.transform.position.z > leftPos.z  ||newobj.transform.position.z < rightPos.z
// 		if (choice == 1)
//         {
// 			if (newobj.transform.position.x > leftPos.x)
// 			{
// 				flag = 1;
// 				// Debug.Log(1);
// 			}
// 			else if (newobj.transform.position.x < rightPos.x)
// 			{
// 				flag = 0;
// 				// Debug.Log(2);
// 			}
// 		}
// 		else if (choice == 0)
//         {
// 			if (newobj.transform.position.z > forwardPos.z)
// 			{
// 				flag = 3;
// 				// Debug.Log(3);
// 			}
// 			else if (newobj.transform.position.z < backPos.z)
// 			{
// 				flag = 2;
// 				// Debug.Log(4);
// 			}
// 		}
//
// 		if (flag == 0) {
// 			newobj.transform.Translate(dir2 * 1 * Time.deltaTime);
// 		} else if(flag ==1){
// 			newobj.transform.Translate(dir1 * 1 * Time.deltaTime);
// 		}
// 		else if (flag == 2)
//         {
// 			newobj.transform.Translate(dir4 * 1 * Time.deltaTime);
// 		}
// 		else if (flag == 3)
//         {
// 			newobj.transform.Translate(dir3 * 1 * Time.deltaTime);
// 		}
// 	}
//
//     public GameObject newBox(GameObject NowCube){
// 		newobj = GameObject.Instantiate (box) as GameObject;
//     float width = Random.Range(3.5f, 5.5f);
// 		my_Dis = Random.Range (width+1f, MaxDis);
// 		my_Height = 0.8f;
// 		// Random.Range (MinHeight, MaxHeight);
//
// 		// for Analytics
// 		Global.distanceSum += my_Dis;
//
// 		choice = myRand.Next(0,2);
// 		patDirection =  choice == 0?new Vector3(-1,0,0): new Vector3(0,0,1);
//
// 		// Global.Direction_cube = Direction;
//
// 		Vector3 pos = Vector3.zero;
// 		if (NowCube == null)
// 		{
// 			pos = patDirection * my_Dis + transform.position;
// 		}
// 		else {
// 			pos = patDirection * my_Dis + NowCube.transform.position;
// 		}
// 		pos.y = my_Height;
// 		newobj.transform.position = pos;
//
// 		//newobj.transform.localScale = new Vector3 (1, my_Height, 1);
// 		// Radom cube size // y (height) must be fixed
// 		newobj.transform.localScale = new Vector3(width, 5, width);
// 		newobj.GetComponent<MeshRenderer> ().material.color = new Color (Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f));
//
// 		if(choice == 1){
// 			leftPos = new Vector3(newobj.transform.position.x+4, newobj.transform.position.y, newobj.transform.position.z);
// 			rightPos = new Vector3(newobj.transform.position.x-4, newobj.transform.position.y, newobj.transform.position.z);
// 			flag = 0;
// 			dir1 = new Vector3(0,0,0);
// 			dir2 = new Vector3(0,0,0);
// 		}
// 		else if (choice == 0)
//         {
// 			forwardPos = new Vector3(newobj.transform.position.x , newobj.transform.position.y, newobj.transform.position.z+4);
// 			backPos = new Vector3(newobj.transform.position.x , newobj.transform.position.y, newobj.transform.position.z-4);
// 			flag = 2;
// 			dir3 = new Vector3(0,0,0);
// 			dir4 = new Vector3(0,0,0);
// 		}
//
// 		dir1 = (rightPos - leftPos).normalized;
// 		dir2 = (leftPos - rightPos).normalized;
//
// 		dir3 = (backPos - forwardPos).normalized;
// 		dir4 = (forwardPos - backPos).normalized;
//
// 		return newobj;
// 	}
//
// }
