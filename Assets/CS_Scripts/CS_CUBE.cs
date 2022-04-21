using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_CUBE : MonoBehaviour
{
    List<GameObject> prefabList = new List<GameObject>();
    public GameObject box1;
    public GameObject box2;
    public float MinDis = 5f;
    public float MaxDis = 12f;
    // public float MinHeight = 0.8f;
    // public float MaxHeight = 1.2f;
    protected float my_Dis;
    protected float my_Height;
    public Vector3 cubeDirection;
    System.Random myRand = new System.Random();
    public bool back = false;

    /////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////
    private int flag = 0;
    private Vector3 patDirection;
    private Vector3 leftPos;
    private Vector3 rightPos;
    private Vector3 forwardPos;
    private Vector3 backPos;
    private int choice;
    private Vector3 dir1;
    private Vector3 dir2;
    private Vector3 dir3;
    private Vector3 dir4;
    private float player_to_cube;
    private CS_Player myPlayer = null;
    private CS_GUI myGUI = null;
    ///////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////


    protected GameObject newobj;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (newobj != null && Global.cube_choice == 0)
        {
           // Debug.Log("bbbbbbbbbbbbbbbbbbbbbbbbbb");
           patroller();
        }

    }

    public void DeleteObj()
    {
        newobj = null;
    }


    public GameObject newBox(GameObject NowCube)
    {
        prefabList.Add(box1);
        prefabList.Add(box2);
        myGUI = GetComponent<CS_GUI>();
        myPlayer = GetComponent<CS_Player>();
        int prefabIndex = Random.Range(0, 2);

        newobj = GameObject.Instantiate(prefabList[prefabIndex]) as GameObject;
        float width = Random.Range(3f, 5f);
        my_Dis = Random.Range(width + 3f, MaxDis);
        // my_Height = Random.Range (MinHeight, MaxHeight);
        my_Height = 0.8f;

        Global.distanceSum += my_Dis;

        // int DirChoose = Random.Range(0, 3);
        choice = myRand.Next(0, 2);
        cubeDirection = choice == 0 ? new Vector3(-1, 0, 0) : new Vector3(0, 0, 1);

        // Global.Direction_cube = Direction;

        Vector3 pos = Vector3.zero;
        if (NowCube == null)
        {
            pos = cubeDirection * my_Dis + transform.position;
        }
        else
        {
            pos = cubeDirection * my_Dis + NowCube.transform.position;
        }


        player_to_cube = myPlayer.GetDistance_Cube_Player(pos, choice);

        myGUI.SetHighEdgeBar(new Vector3(0.2f, -8.5f + player_to_cube * 0.2f, 0));
        myGUI.SetLowEdgeBar(new Vector3(0.2f, -8.5f + player_to_cube * 0.2f - width * 5, 0));
        // Debug.Log(-8.0f + player_to_cube * 0.4f);
        // Debug.Log(my_Dis);


        pos.y = my_Height;
        newobj.transform.position = pos;

        //newobj.transform.localScale = new Vector3 (1, my_Height, 1);
        // Radom cube size // y (height) must be fixed

        newobj.transform.localScale = new Vector3(width, 5, width);
        newobj.GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

        ////////////////////////////////////////////////////////////
        ////////////   test         ////////////////////////////////
        /////////////////////////////////////////////////////////////
        if (choice == 1)
        {
            leftPos = new Vector3(newobj.transform.position.x + 4, newobj.transform.position.y, newobj.transform.position.z);
            rightPos = new Vector3(newobj.transform.position.x - 4, newobj.transform.position.y, newobj.transform.position.z);
            flag = 0;
            dir1 = new Vector3(0, 0, 0);
            dir2 = new Vector3(0, 0, 0);
        }
        else if (choice == 0)
        {
            forwardPos = new Vector3(newobj.transform.position.x, newobj.transform.position.y, newobj.transform.position.z + 4);
            backPos = new Vector3(newobj.transform.position.x, newobj.transform.position.y, newobj.transform.position.z - 4);
            flag = 2;
            dir3 = new Vector3(0, 0, 0);
            dir4 = new Vector3(0, 0, 0);
        }

        dir1 = (rightPos - leftPos).normalized;
        dir2 = (leftPos - rightPos).normalized;

        dir3 = (backPos - forwardPos).normalized;
        dir4 = (forwardPos - backPos).normalized;
        //////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////


        return newobj;
    }


    public void patroller()
    {

        // newobj.transform.position = leftPos; ||newobj.transform.position.z > leftPos.z  ||newobj.transform.position.z < rightPos.z
        if (choice == 1)
        {
            if (newobj.transform.position.x > leftPos.x)
            {
                flag = 1;
                // Debug.Log(1);
            }
            else if (newobj.transform.position.x < rightPos.x)
            {
                flag = 0;
                // Debug.Log(2);
            }
        }
        else if (choice == 0)
        {
            if (newobj.transform.position.z > forwardPos.z)
            {
                flag = 3;
                // Debug.Log(3);
            }
            else if (newobj.transform.position.z < backPos.z)
            {
                flag = 2;
                // Debug.Log(4);
            }
        }

        if (flag == 0)
        {
            newobj.transform.Translate(dir2 * 1 * Time.deltaTime);
        }
        else if (flag == 1)
        {
            newobj.transform.Translate(dir1 * 1 * Time.deltaTime);
        }
        else if (flag == 2)
        {
            newobj.transform.Translate(dir4 * 1 * Time.deltaTime);
        }
        else if (flag == 3)
        {
            newobj.transform.Translate(dir3 * 1 * Time.deltaTime);
        }
    }

    public float getDis()
    {
        return my_Dis;
    }

    public Vector3 getCubeDir()
    {
        return cubeDirection;
    }

}
