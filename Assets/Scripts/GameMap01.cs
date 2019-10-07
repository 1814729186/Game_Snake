using System;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///用于自动生成地图
///</summary>
public class GameMap01:MonoBehaviour
{
    public static int sideLength = 100;//设置地图的边长
    public int numOfWall = 1000;//障碍墙的数量
    private int curNumOfWall = 0;//记录当前生成障碍墙的个数
    public GameObject wallBox01;
    public GameObject wallBox02;
    public GameObject wallBox03;
    public GameObject wallBox04;
    public GameObject wallBox05;

    private bool isGamePaused = false;//游戏暂停 

    public bool[,] map = new bool [sideLength,sideLength];//设置bool数组记录当前地图的状态
    System.Random random;//产生随机数，生成墙
    /// <summary>
    /// 生成墙，并操作状态数组，传入参数：横坐标，纵坐标，墙的编号(1-5)，墙的方向
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="wallNum"></param>
    /// <param name="direction"></param>
    private void CreatWall(int x,int y,int wallNum,WallDirection direction)
    {
        if (WallIsExist(x, y)||wallNum > 5||wallNum < 1) return;
        if ((x - 50 < 5 &&x-50 > -5) || (y - 50 < 5 && y-50 > -5)) return;//在地图中心留出一块空地，作为snake开始的地方
        if (wallNum == 1&&DistenceToBoder(x,y) >=1)
        {
            //四元数的使用方法
            //欧拉角
            //q.w = cos((a / 2) * pi / 180)
            //q.x = RAix.x * sin((a / 2) * pi / 180)
            //q.y = RAix.y * sin((a / 2) * pi / 180)
            //q.z = RAix.z * sin((a / 2) * pi / 180)
            Instantiate(wallBox01, new Vector3(x, y, 0f),direction == WallDirection.Horizental?new Quaternion(): new Quaternion(0,0,(float)Math.Sin(90/2*Math.PI/180), (float)Math.Cos(90.0/2*Math.PI/180)));
            curNumOfWall++;//成功生成障碍墙，墙的数量+1
            map[x, y] = true;
        }
        else if (wallNum == 2&&DistenceToBoder(x, y) >= 2)
        {
            Instantiate(wallBox02, new Vector3(x, y, 0f), direction == WallDirection.Horizental ? new Quaternion() : new Quaternion(0, 0, (float)Math.Sin(90 / 2 * Math.PI / 180), (float)Math.Cos(90.0 / 2 * Math.PI / 180)));
            curNumOfWall++;//成功生成障碍墙，墙的数量+1
            if (direction == WallDirection.Horizental)
            {
                for(int i = 0;i < 3;i ++)
                    MapChangeTrue(x-1+i,y);
            }else
            {
                for (int i = 0; i < 3; i++)
                    MapChangeTrue(x , y- 1 + i);
            }
        }
        else if (wallNum == 3 && DistenceToBoder(x, y) >= 3)
        {
            Instantiate(wallBox03, new Vector3(x, y, 0f), direction == WallDirection.Horizental ? new Quaternion() : new Quaternion(0, 0, (float)Math.Sin(90 / 2 * Math.PI / 180), (float)Math.Cos(90.0 / 2 * Math.PI / 180)));
            curNumOfWall++;//成功生成障碍墙，墙的数量+1
            if (direction == WallDirection.Horizental)
            {
                for (int i = 0; i < 5; i++)
                    MapChangeTrue(x - 2 + i, y);
            }
            else
            {
                for (int i = 0; i < 5; i++)
                    MapChangeTrue(x, y - 2 + i);
            }
        }
        else if (wallNum == 4 && DistenceToBoder(x, y) >= 4)
        {
            Instantiate(wallBox04, new Vector3(x, y, 0f), direction == WallDirection.Horizental ? new Quaternion() : new Quaternion(0, 0, (float)Math.Sin(90 / 2 * Math.PI / 180), (float)Math.Cos(90.0 / 2 * Math.PI / 180)));
            curNumOfWall++;//成功生成障碍墙，墙的数量+1
            if (direction == WallDirection.Horizental)
            {
                for (int i = 0; i < 7; i++)
                    MapChangeTrue(x - 3+ i, y);
            }
            else
            {
                for (int i = 0; i < 7; i++)
                    MapChangeTrue(x, y - 3 + i);
            }
        }
        else if(DistenceToBoder(x, y) >= 5)
        {
            Instantiate(wallBox01, new Vector3(x, y, 0f), direction == WallDirection.Horizental ? new Quaternion() : new Quaternion(0, 0, (float)Math.Sin(90 / 2 * Math.PI / 180), (float)Math.Cos(90.0 / 2 * Math.PI / 180)));
            curNumOfWall++;//成功生成障碍墙，墙的数量+1
            if (direction == WallDirection.Horizental)
            {
                for (int i = 0; i < 9; i++)
                    MapChangeTrue(x - 4 + i, y);
            }
            else
            {
                for (int i = 0; i < 9; i++)
                    MapChangeTrue(x, y - 4 + i);
            }
        }
        
    }
    /// <summary>
    /// 检测当前位置是否是墙体，如果是，则返回true,参数：横坐标，纵坐标
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool WallIsExist(int x,int y)
    {
        return map[x, y];
    }
    /// <summary>
    /// 设定数组元素的值为true
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void MapChangeTrue(int x,int y)
    {
        if (x < sideLength && x > 0&&y<sideLength&&y > 0) map[x, y] = true;
    }
    /// <summary>
    /// 设定数组元素为false
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void MapChangeFalse(int x,int y)
    {
        if (x < sideLength && x > 0 && y < sideLength && y > 0) map[x, y] = false;
    }
    /// <summary>
    /// 返回到达边界的距离
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private int DistenceToBoder(int x,int y)
    {
        int disX = x  < sideLength - x - 1 ? x  : sideLength - x - 1;
        int disY = y  < sideLength - y - 1 ? y : sideLength - y - 1;
        return disX < disY ? disX : disY;
    }
    private void Start()
    {
        random = new System.Random();
        ///生成墙体边界
        for(int i = 0;i < sideLength; i ++)
        {
            map[0, i] = true;
            Instantiate(wallBox01,new Vector3(0,i,0),new Quaternion());
            map[sideLength - 1, i] = true;
            Instantiate(wallBox01, new Vector3(sideLength -1, i, 0), new Quaternion());
        }
        for(int i = 1;i < sideLength - 1;i ++)
        {
            map[i, 0] = true;
            Instantiate(wallBox01, new Vector3(i, 0, 0), new Quaternion());
            map[i, sideLength - 1] = true;
            Instantiate(wallBox01, new Vector3(i, sideLength - 1, 0), new Quaternion());
        }
        //创建障碍墙体
        while (curNumOfWall <= numOfWall)
        {
            int x = random.Next(1,sideLength - 1);
            int y = random.Next(1,sideLength - 1);
            WallDirection direction = random.Next(0, 2) == 1 ? WallDirection.Horizental : WallDirection.Vertical;
            int wallNum = random.Next(1,6);
            CreatWall(x, y, wallNum, direction);
        }
    }

    //设置游戏暂停和开始
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {

            if (isGamePaused == false)
                PauseGame();
            else
                Continue();
        }
    }
    //控制游戏开始暂停,设置为public方法，使按钮能够调用
    public void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0;
        GameObject.Find("PauseUI").GetComponent<Canvas>().enabled = true;
    }
    public void Continue()
    {
        isGamePaused = false;
        Time.timeScale = 1;
        GameObject.Find("PauseUI").GetComponent<Canvas>().enabled = false;
    }
    public void LoadScene(int sceneNum)
    {
        Application.LoadLevel(sceneNum);
    }
}
