using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Great By mysz
Date20191004
*/
///<summary>
///管理游戏中道具的生成
///</summary>
public class GameItemManager : MonoBehaviour
{
    //得到游戏道具的预制体
    public GameObject item;//得到游戏物体
    private int numOfItem = 0;
    public int maxNumOfItem = 20;
    public GameObject map;//获得游戏中得地图，便于直接调用方法查看地图状态
    //游戏改进：使游戏中的除Food以及地雷之外的物体始终处于运动状态之中
    public bool isMoving = true;//物体是否能够运动
    System.Random random = new System.Random();
    private GameObject[] objArray;
    private void Start()
    {
        objArray = new GameObject[maxNumOfItem];
            while(numOfItem<maxNumOfItem)
            {
                int x = random.Next(1,map.GetComponent<GameMap01>().map.GetLength(0) - 2);
                int y = random.Next(1,map.GetComponent<GameMap01>().map.GetLength(0)-2);
                if(map.GetComponent<GameMap01>().WallIsExist(x,y)==false)
                {
                    
                    objArray[numOfItem] = Instantiate(item,new Vector3(x,y,0),new Quaternion(0,0,0,0));//在当前位置生成物体
                    if(isMoving==false)//设置当前位置的状态为true
                    {
                        map.GetComponent<GameMap01>().MapChangeTrue(x,y);
                    }
                    else//给物体一个初速度
                    {
                        objArray[numOfItem].GetComponent<Rigidbody2D>().velocity = new Vector2(random.Next(0, 5), random.Next(0, 5));
                    }
                    numOfItem++;
                }
            }
        InvokeRepeating("CheckArray", 0, 2);//用InvokeRepeating实现重复调用
    }
    /// <summary>
    /// 检查数组中是否有元素被销毁，如果被销毁，将再次生成
    /// </summary>
    private void CheckArray()
    {
        for(int i = 0;i < maxNumOfItem;i++)
        {
            if(objArray[i] == null)
            {
                int x = random.Next(1, map.GetComponent<GameMap01>().map.GetLength(0) - 2);
                int y = random.Next(1, map.GetComponent<GameMap01>().map.GetLength(0) - 2);
                objArray[i] = Instantiate(item, new Vector3(x, y, 0), new Quaternion(0, 0, 0, 0));//在当前位置生成物体
                map.GetComponent<GameMap01>().MapChangeTrue(x, y);
            }
        }
    }
}
