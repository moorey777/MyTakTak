using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_SwingMace : MonoBehaviour
{
	public GameObject mace;
	protected float my_Dis;
	protected float my_Height;
	public Vector3 cubeDirection;
	System.Random myRand = new System.Random();
	public bool back = false;
	private int flag = 0;
	private int choice;


	protected GameObject newobj;

    // Start is called before the first frame update
  void Start(){
    
  }

    // Update is called once per frame
  void Update(){

  }

  public void DeleteObj() {
  		newobj = null;
  }
  


  public GameObject newBox(GameObject NowCube, float dis, Vector3 cubeDirPassed){

	  newobj = GameObject.Instantiate (mace) as GameObject;

	  my_Dis = dis / 2;
	  my_Height = 7.6f;

	  cubeDirection = cubeDirPassed;
	  Vector3 pos = Vector3.zero;
	  if (NowCube == null)
	  {
		  pos = cubeDirection * my_Dis + transform.position;
	  }
	  else {
		  pos = cubeDirection * my_Dis + NowCube.transform.position;
	  }

	  pos.y = my_Height;
	  newobj.transform.position = pos;


	  if (cubeDirection.z != 0)
	  {
		  Vector3 rotationVector = transform.rotation.eulerAngles;
		  rotationVector.y = 0;
		  newobj.transform.rotation = Quaternion.Euler(rotationVector);
	  }

		return newobj;
	}
  

}
