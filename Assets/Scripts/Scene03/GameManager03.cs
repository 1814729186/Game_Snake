using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Great By mysz
Date:
*/
///<summary>
///
///</summary>
public class GameManager03 : MonoBehaviour
{
    public GameObject wall;
    public GameObject food;
    public GameObject snake;
    public GameObject wallUp;
    public GameObject wallDown;

    public GameObject[] diamonds = new GameObject[4];


    public Vector3 wallUpTra;
    public Vector3 wallDownTra;
    public Vector3 foodTra;

    private Vector2 lastPosition;

    private bool isPause = false;
    private System.Random random = new System.Random();
    private void Start()
    {
        foodTra = this.transform.position + new Vector3(18, 0, 0);
        lastPosition = this.transform.position;
        wallUpTra = wallUp.GetComponent<Transform>().position;
        wallDownTra = wallDown.GetComponent<Transform>().position;//得到最近的两堵墙的位置
        InvokeRepeating("CreatWall", 0f, 0.4f);
        InvokeRepeating("CreatFood", 0f, 0.8f);
    }

    private void CreatWall()
    {
        Instantiate(wall, wallUpTra += new Vector3(9, 0, 0), new Quaternion(0, 0, 0, 0));
        Instantiate(wall, wallDownTra += new Vector3(9, 0, 0), new Quaternion(0, 0, 0, 0));
    }
    private void CreatFood()
    {
        
        Instantiate(food, foodTra, new Quaternion(0, 0, 0, 0));
        foodTra += new Vector3(9, 0, 0);
        //生成4个方块
        Instantiate(diamonds[random.Next(0,4)],foodTra + new Vector3(0,3,0),new Quaternion(0,0,0,0));
        Instantiate(diamonds[random.Next(0,4)],foodTra + new Vector3(0,1,0),new Quaternion(0,0,0,0));
        Instantiate(diamonds[random.Next(0,4)],foodTra + new Vector3(0,-1,0),new Quaternion(0,0,0,0));
        Instantiate(diamonds[random.Next(0,4)],foodTra + new Vector3(0,-3,0),new Quaternion(0,0,0,0));
        foodTra += new Vector3(9,0,0);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if(isPause == false)
            {
                Pause();
                isPause = true;
            }
            else if(isPause == true)
            {
                UnPause();
                isPause = false;
            }
        }
            
    }
    public void Pause()
    {
        GameObject.Find("PauseUI").GetComponent<Canvas>().enabled = true;
        Time.timeScale = 0;
    }
    public void UnPause()
    {
        GameObject.Find("PauseUI").GetComponent<Canvas>().enabled = false;
        Time.timeScale = 1;
    }
}
